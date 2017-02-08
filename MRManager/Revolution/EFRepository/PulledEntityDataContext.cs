using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using SystemInterfaces;
using Common;
using EF.DBContexts;
using EF.Entities;
using EventAggregator;
using EventMessages.Events;
using Microsoft.EntityFrameworkCore;
using RevolutionData.Context;
using RevolutionEntities.Process;

namespace EFRepository
{
    public class PulledEntityDataContext : BaseRepository<PulledEntityDataContext>
    {
        private const string DefaultResponseType = "TextBox";
        public const string DefaultType = "string";
        //Bring in all lookups with enums
        

       

        public static void UpdatePulledEntityWithChanges<TEntity>(IUpdatePulledEntityWithChanges<TEntity> msg) where TEntity : IEntity
        {

            using (var ctx = new MRManagerDBContext())
            {
                try
                {
                    var entity = ctx.Patients.FirstOrDefault(x => x.Id == msg.EntityId);

                    foreach (var change in msg.Changes)
                    {
                        var patientVisit =
                            ctx.PatientVisit.Include(x => x.PatientResponses)
                                            .FirstOrDefault(x => x.PatientId == msg.EntityId && x.DateOfVisit.Date == DateTime.Today.Date)
                                ??
                            ctx.PatientVisit.Add(new PatientVisit()
                            {
                                VisitTypeId = ctx.VisitType.First(x => x.Name == "DataEntry").Id,
                                PatientId = msg.EntityId,
                                DoctorId = 0,
                                DateOfVisit = DateTime.Today.Date,
                            }).Entity;

                        var questions =
                            ctx.Questions.Include(x => x.PatientResponses)
                                         .Include(x => x.ResponseOptions)
                                         .Where(x =>
                                                    x.EntityAttributes.Entity == msg.EntityName &&
                                                    x.EntityAttributes.Attribute == change.Key);
                        Questions question;
                        ResponseOptions responseOptions;
                        if (!questions.Any())
                            ///throw new InvalidOperationException($"No Questions Found for {msg.EntityName}-{change.Key}! Please Update Interviews before trying to add information.");
                        {
                            question = new Questions
                            {
                                Description = $"What is {msg.EntityName} {change.Key}?",
                                EntityAttributes = new EntityAttributes()
                                {
                                    Entity = msg.EntityName,
                                    Attribute = change.Key,
                                    Type = DefaultType
                                },
                                ResponseOptions = new List<ResponseOptions>(),
                                PatientResponses = new List<PatientResponses>()

                            };
                            ctx.Questions.Add(question);
                        }
                        else
                        {
                            question = questions.First();
                        }

                        responseOptions = question.ResponseOptions.FirstOrDefault(x => x.Type == DefaultResponseType);
                        if (responseOptions == null)
                        {
                            responseOptions = new ResponseOptions()
                            {
                                Description = change.Key,
                                Type = DefaultResponseType
                            };
                            question.ResponseOptions.Add(responseOptions);
                        }

                        var patientSyntom =
                            ctx.PatientSyntoms.Include(x => x.PatientVisit).FirstOrDefault(x => x.PatientVisit == patientVisit && x.SyntomId == 0) ??
                            ctx.PatientSyntoms.Add(new PatientSyntoms()
                            {
                                PatientVisit = patientVisit,
                                StatusId = 0,
                                SyntomId = 0,
                                PriorityId = 0,
                            }).Entity;

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

                        var response = patientResponses.Response.FirstOrDefault(x => x.ResponseOptions == responseOptions);
                        if (response == null)
                        {
                            response = ctx.Response.Add(new Response()
                            {
                                PatientResponses = patientResponses,
                                ResponseOptions = responseOptions,
                            }).Entity;
                            question.PatientResponses.Add(patientResponses);
                        }
                       
                        response.Value = change.Value.ToString();
                        

                        ctx.SaveChanges(true);
                        
                        EventMessageBus.Current.Publish(new EntityUpdated<TEntity>((TEntity)(object)entity, new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntityUpdated), msg.Process, Source), Source);

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
