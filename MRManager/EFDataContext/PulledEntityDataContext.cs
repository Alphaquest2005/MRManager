﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Windows.Interop;
using SystemInterfaces;
using BootStrapper;
using Common;
using Common.DataEntites;
using DomainMessages;
using EF.DBContexts;
using EF.Entities;
using EventAggregator;
using EventMessages.Events;
using Expressions;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using RevolutionData.Context;
using RevolutionEntities.Process;
using RevolutionLogger;
using Utilities;
using Process = RevolutionEntities.Process.Process;

namespace EF.DBContext
{
    public partial class PulledDBContext
    {
        private const int DefaultResponseType = 1;

        //Bring in all lookups with enums
        public static ISystemSource Source => new Source(Guid.NewGuid(), $"PulledDBContext:<{typeof(PulledDBContext).GetFriendlyName()}>", new SourceType(typeof(PulledDBContext)), new SystemProcess(new Process(1, 0, "Starting System", "Prepare system for Intial Use", "", new Agent("System")), new MachineInfo(Environment.MachineName, Environment.ProcessorCount)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));

        private static Dictionary<Type, Func<string, KeyValuePair<string, dynamic>, string>> IPulledMatchTypeFunctions = new Dictionary<Type, Func<string, KeyValuePair<string, object>, string>>()
      {
          {typeof(IExactMatch), (str, itm) => str + $"ResponseOptions.Questions.EntityAttributes.Attribute == \"{itm.Key}\" && Value == \"{itm.Value}\" &&" },
          {typeof(IPartialMatch), (str, itm) => str + $"ResponseOptions.Questions.EntityAttributes.Attribute ==\"{itm.Key}\" && Value.Contains(\"{itm.Value}\") &&" }
      };

        static PulledDBContext()
        {
            EventMessageBus.Current.GetEvent<IUpdatePatientEntityWithChanges<IPatients>>(Source).Subscribe(x => UpdatePulledEntityWithChanges(x));
            EventMessageBus.Current.GetEvent<IUpdatePatientEntityListWithChanges<IPatients>>(Source).Subscribe(x => UpdatePulledEntityListWithChanges(x));
            EventMessageBus.Current.GetEvent<IGetEntityFromPatientResponse<IEntityView>>(Source).Subscribe(x => GetEntityFromPatientResponse(x));
            EventMessageBus.Current.GetEvent<ILoadPulledEntityViewSetWithChanges<IMatchType>>(Source).Subscribe(x => LoadPulledEntityViewSetWithChanges(x));

        }

        private static void UpdatePulledEntityListWithChanges(IUpdatePatientEntityListWithChanges<IPatients> msg)
        {
            UpdateDatabase(msg.EntityId,msg.ListId, msg.EntityName, msg.Attribute, msg.SyntomName, msg.InterviewName, msg.Changes, msg.Process);
        }

        public static void UpdatePulledEntityWithChanges(IUpdatePatientEntityWithChanges<IPatients> msg)
        {
            UpdateDatabase(msg.EntityId,null, msg.EntityName, null, msg.SyntomName, msg.InterviewName, msg.Changes, msg.Process);
        }

        private static void UpdateDatabase(int entityId, int? listId, string entityName, string attribute, string syntomName, string interviewName, Dictionary<string, dynamic> changes, ISystemProcess process)
        {
            using (var ctx = new MRManagerDBContext())
            {
                var exp = FindExpressionClass.FindExpression<Patients, PatientInfo>();

                try
                {
                    var entity = ctx.Patients.FirstOrDefault(x => x.Id == entityId)
                                 ?? ctx.Patients.Add(new Patients()).Entity;

                    var patientVisit = ctx.PatientVisit.Include(x => x.PatientResponses)
                                           .FirstOrDefault(x => x.PatientId == entity.Id &&
                                                                x.DateOfVisit.Date == DateTime.Today.Date)
                                       ??
                                       ctx.PatientVisit.Add(new PatientVisit()
                                       {
                                           VisitTypeId = ctx.VisitType.First(x => x.Name == "DataEntry").Id,
                                           Patients = entity,
                                           DoctorId = 0,
                                           DateOfVisit = DateTime.Today.Date,
                                       }).Entity;
                    var syntom = ctx.Syntoms.Include(x => x.SyntomMedicalSystems).First(x => x.Name == syntomName);
                    
                    var currentInterview = ctx.Interviews.FirstOrDefault(x => x.Name == interviewName)
                                           ?? ctx.Interviews.Add(new Interviews()
                                           {
                                               Name = interviewName,
                                               RowState = RowState.Added
                                           }).Entity;
                    int medicalSystemId;
                    medicalSystemId = syntom.SyntomMedicalSystems.Any() 
                                        ? syntom.SyntomMedicalSystems.First().MedicalSystemId 
                                        : ctx.MedicalSystems.First(x => x.Name == "Patient").Id;

                    if(currentInterview.RowState == RowState.Added)
                    {
                        currentInterview.MedicalSystemInterviews = new List<MedicalSystemInterviews>(){new MedicalSystemInterviews(){InterviewId = 0, MedicalSystemId = medicalSystemId}};
                    }
                    var patientSyntom =
                        ctx.PatientSyntoms.Include(x => x.PatientVisit)
                            .FirstOrDefault(x => x.PatientVisit == patientVisit && x.Syntoms.Name == syntomName) ??
                        ctx.PatientSyntoms.Add(new PatientSyntoms()
                        {
                            PatientVisit = patientVisit,
                            StatusId = 0,
                            SyntomId = syntom.Id,
                            PriorityId = 0,
                        }).Entity;

                    foreach (var change in changes)
                    {
                        var attribute1 = attribute ?? change.Key;
                        var questions =
                            ctx.Questions.Include(x => x.PatientResponses)
                                .Include(x => x.ResponseOptions)
                                .Where(x =>
                                    x.EntityAttributes.Entity == entityName &&
                                    x.EntityAttributes.Attribute == attribute1);
                        Questions question;
                        ResponseOptions responseOptions;
                        if (!questions.Any())
                        {
                            question = new Questions
                            {
                                Description = $"What is {entityName} {attribute1}?",
                                EntityAttributes = new EntityAttributes()
                                {
                                    Entity = entityName,
                                    Attribute = attribute1,
                                },
                                Interviews = currentInterview,
                                ResponseOptions = new List<ResponseOptions>(),
                                PatientResponses = new List<PatientResponses>()
                            };
                            ctx.Questions.Add(question);
                        }
                        else
                        {
                            question = questions.First();
                        }

                        responseOptions = question.ResponseOptions.FirstOrDefault(x => x.Description == change.Key);
                                           
                        if (responseOptions == null)
                        {
                            responseOptions = new ResponseOptions()
                            {
                                Description = change.Key,
                                QuestionResponseTypeId = DefaultResponseType
                            };
                            question.ResponseOptions.Add(responseOptions);
                        }


                        var patientResponses = ctx.PatientResponses.Include(x => x.Response).ThenInclude(x => x.ResponseOptions)
                                                   .FirstOrDefault(x => x.PatientVisit == patientVisit
                                                                        &&
                                                                        x.PatientSyntoms ==
                                                                        patientSyntom
                                                                        && x.Questions == question)
                                               ?? ctx.PatientResponses.Add(new PatientResponses()
                                               {
                                                   PatientSyntoms = patientSyntom,
                                                   Questions = question,
                                                   PatientVisit = patientVisit,
                                                   Response = new List<Response>()
                                               }).Entity;

                        var response = listId == null
                                        ? patientResponses.Response.FirstOrDefault(x => x.ResponseOptions == responseOptions)
                                        : patientResponses.Response.FirstOrDefault(x => x.Id == listId);
                        if (response == null)
                        {
                            response = ctx.Response.Add(new Response()
                            {
                                PatientResponses = patientResponses,
                                ResponseOptions = responseOptions,
                            }).Entity;
                            question.PatientResponses.Add(patientResponses);
                        }
                        if (response.ResponseOptions.Description != change.Key)
                            response.ResponseOptions.Description = change.Key;
                        response.Value = change.Value.ToString();
                    }


                    ctx.SaveChanges(true);

                    var res = ctx.Set<Patients>().Select(exp).DistinctBy(x => x.Id).FirstOrDefault(x => x.Id == entity.Id); //
                    //Todo: replace this with proper event using Entityfound cuz aint want to create new event for entityviewupdated
                    EventMessageBus.Current.Publish(
                        new EntityViewWithChangesUpdated<IPatientInfo>((IPatientInfo) (object) res, changes,
                            new StateEventInfo(process.Id, RevolutionData.Context.Entity.Events.EntityUpdated), process,
                            Source), Source);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static void GetEntityFromPatientResponse<TView>(IGetEntityFromPatientResponse<TView> msg) where TView : IEntityView
        {
            try
            {
                using (var ctx = new MRManagerDBContext())
                {
                    var props = msg.ViewType.GetProperties().ToList();
                    var res =
                        ctx.PatientResponses.OrderByDescending(x3 => x3.PatientVisitId).Where(x => x.PatientVisit.PatientId == msg.PatientId)
                            .Where(x => x.Questions.EntityAttributes.Entity == msg.EntityName)
                            .SelectMany(x => x.Response).Where(z => z.Value != null && props.Any(q => q.Name.Replace(" ", "") == z.ResponseOptions.Description.Replace(" ", "")))
                            .GroupBy(x => x.ResponseOptions.Description)
                            .Select(g => new KeyValuePair<string, dynamic>(g.Key, g.Any() ? g.First().Value : null))
                            .ToList();
                    var itmType = BootStrapper.BootStrapper.Container.GetExportedTypes(msg.ViewType).FirstOrDefault() ?? BootStrapper.BootStrapper.Container.GetExportedType(msg.ViewType);
                    var itm = Activator.CreateInstance(itmType);
                    itm.Id = msg.PatientId;
                    res.ForEach(x => { ((IEntityId)itm).ApplyChanges(x); });
                    typeof(PulledDBContext).GetMethod("PublishEntityFound").MakeGenericMethod(msg.ViewType).Invoke(null,new object[] {msg, itm});
                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityFound<>));

            }
        }

        public static void PublishEntityFound<TView>(IGetEntityFromPatientResponse<TView> msg, dynamic p) where TView : IEntityView
        {
            EventMessageBus.Current.Publish(
                new EntityFound<TView>((TView)(object) p,
                    new StateEventInfo(msg.Process.Id, EntityView.Events.EntityViewFound), msg.Process, Source), Source);
        }

        private static void PublishProcesError(IProcessSystemMessage msg, Exception ex, Type expectedMessageType)
        {
            var outMsg = new ProcessEventFailure(failedEventType: msg.GetType(),
               failedEventMessage: msg,
               expectedEventType: expectedMessageType,
               exception: ex,
               source: Source, processInfo: new StateEventInfo(msg.Process.Id, RevolutionData.Context.Process.Events.Error));
            Logger.Log(LoggingLevel.Error, $"Error:ProcessId:{msg.ProcessInfo.ProcessId}, ProcessStatus:{msg.ProcessInfo.State.Status}, ExceptionMessage: {ex.Message}|||| {ex.StackTrace}");
            EventMessageBus.Current.Publish(outMsg, Source);
        }

        public static void LoadPulledEntityViewSetWithChanges(ILoadPulledEntityViewSetWithChanges<IMatchType> msg)// where TView : IEntityView
        {
            try
            {
                using (var ctx = new MRManagerDBContext())
                {
                    var props = msg.ViewType.GetProperties().ToList();
                    var matchtype = msg.GetType().GenericTypeArguments[0];
                    var whereStr = msg.Changes.Aggregate("", IPulledMatchTypeFunctions[matchtype]);
                    whereStr = whereStr.TrimEnd('&');

                    var entities = string.IsNullOrEmpty(whereStr)
                        ? ctx.PatientResponses.OrderByDescending(x3 => x3.PatientVisitId)
                            .Where(
                                x =>
                                    x.Questions.EntityAttributes.Entity == msg.EntityName &&
                                    props.Any(z => z.Name == x.Questions.EntityAttributes.Attribute))
                            .GroupBy(x => new { x.PatientVisit.PatientId })
                            .Select(g => new
                            {
                                Id = g.Key.PatientId,
                                Changes = g.SelectMany(q => q.Response)
                                    .GroupBy(w => w.PatientResponses.Questions.EntityAttributes.Attribute)
                                    .Select(rg => new KeyValuePair<string, dynamic>(
                                        rg.Key,
                                        rg.Any() ? rg.First().Value : null)).ToList()
                            }).ToList()
                        : ctx.PatientResponses
                            .Where(x => x.Questions.EntityAttributes.Entity == msg.EntityName && props.Any(z => z.Name == x.Questions.EntityAttributes.Attribute))
                            .Where($"Response.Where({whereStr}).Any()")
                            .GroupBy(x => new { x.PatientVisit.PatientId })
                            .Select(g => new
                            {
                                Id = g.Key.PatientId,
                                Changes = g.SelectMany(q => q.Response)
                                    .GroupBy(w => w.PatientResponses.Questions.EntityAttributes.Attribute)
                                    .Select(rg => new KeyValuePair<string, dynamic>(
                                        rg.Key,
                                        rg.Any() ? rg.First().Value : null)).ToList()
                            }).ToList();
                    var p = BootStrapper.BootStrapper.Container.GetExportedTypes(msg.ViewType).FirstOrDefault() ?? BootStrapper.BootStrapper.Container.GetExportedType(msg.ViewType);
                    var res = new List<IEntityId>();
                    entities.ForEach(x =>
                    {
                        var itm = Activator.CreateInstance(p);
                        itm.Id = x.Id;
                        ((IEntityId)itm).ApplyChanges(x.Changes);
                        res.Add(itm);
                    });
                    

                    typeof(PulledDBContext).GetMethod("PublishEntityViewSetWithChangesLoaded").MakeGenericMethod(msg.ViewType).Invoke(null, new object[] { msg, res });

                }
            }
            catch (Exception ex)
            {
                PublishProcesError(msg, ex, typeof(IEntityViewLoaded<>));
            }

        }

        public static void PublishEntityViewSetWithChangesLoaded<TView>(ILoadPulledEntityViewSetWithChanges<IMatchType> msg, List<IEntityId>res) where TView : IEntityView
        {
            EventMessageBus.Current.Publish(new EntityViewSetWithChangesLoaded<TView>(res.OrderByDescending(x => x.Id).Select(x => (TView)(object)x).ToList(), msg.Changes, new StateEventInfo(msg.Process.Id, EntityView.Events.EntityViewFound), msg.Process, Source), Source);
        }


    }
}
