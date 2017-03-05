//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;

//using System.Threading.Tasks;
//using SystemInterfaces;
//using Common;
//using EventAggregator;
//using EventMessages.Events;
//using Expressions;
//using Interfaces;
//using Microsoft.EntityFrameworkCore;
//using MoreLinq;
//using RevolutionData.Context;
//using RevolutionEntities.Process;

//namespace EFRepository
//{
//    public class PulledEntityDataContext : BaseRepository<PulledEntityDataContext>
//    {
//        private const int DefaultResponseType = 1;

//        //Bring in all lookups with enums


//        private static Dictionary<Type, Func<string, KeyValuePair<string, dynamic>, string>> IPulledMatchTypeFunctions = new Dictionary<Type, Func<string, KeyValuePair<string, object>, string>>()
//      {
//          {typeof(IExactMatch), (str, itm) => str + $"ResponseOptions.Questions.EntityAttributes.Attribute == \"{itm.Key}\" && Value == \"{itm.Value}\" &&" },
//          {typeof(IPartialMatch), (str, itm) => str + $"ResponseOptions.Questions.EntityAttributes.Attribute ==\"{itm.Key}\" && Value.Contains(\"{itm.Value}\") &&" }
//      };

//        public static void UpdatePulledEntityWithChanges<TEntity>(IUpdatePatientEntityWithChanges<TEntity> msg) where TEntity : IEntity
//        {

//            using (var ctx = new MRManagerDBContext())
//            {
//                var exp = FindExpressionClass.FindExpression<Patients, PatientInfo>();

//                try
//                {
//                    var entity = ctx.Patients.FirstOrDefault(x => x.Id == msg.EntityId) 
//                                 ?? ctx.Patients.Add(new Patients()).Entity;

//                    var patientVisit = ctx.PatientVisit.Include(x => x.PatientResponses)
//                                                                .FirstOrDefault(x => x.PatientId == entity.Id && x.DateOfVisit.Date == DateTime.Today.Date)
//                                                    ??
//                                                ctx.PatientVisit.Add(new PatientVisit()
//                                                {
//                                                    VisitTypeId = ctx.VisitType.First(x => x.Name == "DataEntry").Id,
//                                                    Patients = entity,
//                                                    DoctorId = 0,
//                                                    DateOfVisit = DateTime.Today.Date,
//                                                }).Entity;

//                    var currentInterview = ctx.Interviews.FirstOrDefault(x => x.Name == msg.InterviewName);
//                    if (currentInterview == null)
//                    {
//                        currentInterview = ctx.Interviews.Add(new Interviews()
//                        {
//                            Name = msg.InterviewName
//                        }).Entity;

//                      var system =  ctx.MedicalSystemInterviews.Add(new MedicalSystemInterviews()
//                        {
//                            Interviews = currentInterview,
//                            MedicalSystems =
//                                ctx.SyntomMedicalSystems.Include(x=> x.MedicalSystems).First(x => x.Syntoms.Name == msg.SyntomName)
//                                    .MedicalSystems
//                        }).Entity;

                        

//                    }

//                    var patientSyntom =
//                          ctx.PatientSyntoms.Include(x => x.PatientVisit).FirstOrDefault(x => x.PatientVisit == patientVisit && x.Syntoms.Name == msg.SyntomName) ??
//                          ctx.PatientSyntoms.Add(new PatientSyntoms()
//                          {
//                              PatientVisit = patientVisit,
//                              StatusId = 0,
//                              SyntomId = ctx.Syntoms.First(x => x.Name == msg.SyntomName).Id,
//                              PriorityId = 0,
//                          }).Entity;

//                    foreach (var change in msg.Changes)
//                    {
//                        var questions =
//                            ctx.Questions.Include(x => x.PatientResponses)
//                                         .Include(x => x.ResponseOptions)
//                                         .Where(x =>
//                                                    x.EntityAttributes.Entity == msg.EntityName &&
//                                                    x.EntityAttributes.Attribute == change.Key);
//                        Questions question;
//                        ResponseOptions responseOptions;
//                        if (!questions.Any())
//                            ///throw new InvalidOperationException($"No Questions Found for {msg.EntityName}-{change.Key}! Please Update Interviews before trying to add information.");
//                        {
                           
//                            question = new Questions
//                            {
//                                Description = $"What is {msg.EntityName} {change.Key}?",
//                                EntityAttributes = new EntityAttributes()
//                                {
//                                    Entity = msg.EntityName,
//                                    Attribute = change.Key,
//                                },
//                                Interviews = currentInterview,
//                                ResponseOptions = new List<ResponseOptions>(),
//                                PatientResponses = new List<PatientResponses>()

//                            };
//                            ctx.Questions.Add(question);
//                        }
//                        else
//                        {
//                            question = questions.First();
//                        }

//                        responseOptions = question.ResponseOptions.FirstOrDefault(x => x.QuestionResponseTypeId == DefaultResponseType);
//                        if (responseOptions == null)
//                        {
//                            responseOptions = new ResponseOptions()
//                            {
//                                Description = change.Key,
//                                QuestionResponseTypeId = DefaultResponseType
//                            };
//                            question.ResponseOptions.Add(responseOptions);
//                        }

                      

//                        var patientResponses = ctx.PatientResponses.Include(x => x.Response).ThenInclude(x => x.ResponseOptions)
//                                                .FirstOrDefault(x => x.PatientVisit == patientVisit
//                                                                     &&
//                                                                     x.PatientSyntoms ==
//                                                                     patientSyntom
//                                                                     && x.Questions == question) 
                                                 
//                                                ?? ctx.PatientResponses.Add(new PatientResponses()
//                                                 {
//                                                     PatientSyntoms = patientSyntom,
//                                                     Questions = question,
//                                                     PatientVisit = patientVisit,
//                                                     Response = new List<Response>()
//                                                 }).Entity;

//                        var response = patientResponses.Response.FirstOrDefault(x => x.ResponseOptions == responseOptions);
//                        if (response == null)
//                        {
//                            response = ctx.Response.Add(new Response()
//                            {
//                                PatientResponses = patientResponses,
//                                ResponseOptions = responseOptions,
//                            }).Entity;
//                            question.PatientResponses.Add(patientResponses);
//                        }
                       
//                        response.Value = change.Value.ToString();
//                    }    



//                        ctx.SaveChanges(true);

//                    var res = ctx.Set<Patients>().Select(exp).DistinctBy(x => x.Id).FirstOrDefault(x => x.Id == entity.Id);//
//                    //Todo: replace this with proper event using Entityfound cuz aint want to create new event for entityviewupdated
//                    EventMessageBus.Current.Publish(new EntityViewWithChangesUpdated<IPatientInfo>((IPatientInfo)(object)res, msg.Changes ,new StateEventInfo(msg.Process.Id, RevolutionData.Context.Entity.Events.EntityUpdated), msg.Process, Source), Source);

                    
//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//            }
//        }

//        public static void GetEntityFromPatientResponse(IGetEntityFromPatientResponse<TView> msg)
//        {
//            try
//            {
//                using (var ctx = new TDbContext())
//                {
//                    var props = typeof(TView).GetProperties().ToList();
//                    var res =
//                        ctx.PatientResponses.OrderByDescending(x3 => x3.PatientVisitId).Where(x => x.PatientVisit.PatientId == msg.PatientId)
//                            .Where(x => x.Questions.EntityAttributes.Entity == msg.EntityName)
//                            .SelectMany(x => x.Response).Where(z => z.Value != null && props.Any(q => q.Name.Replace(" ", "") == z.ResponseOptions.Description.Replace(" ", "")))
//                            .GroupBy(x => x.ResponseOptions.Description)
//                            .Select(g => new KeyValuePair<string, dynamic>(g.Key, g.Any() ? g.First().Value : null))
//                            .ToList();
//                    TDbView p = new TDbView() { Id = msg.PatientId };
//                    res.ForEach(x => p.ApplyChanges(x));
//                    EventMessageBus.Current.Publish(
//                        new EntityFound<TView>((TView)(object)p,
//                            new StateEventInfo(msg.Process.Id, EntityView.Events.EntityViewFound), msg.Process, Source),
//                        Source);
//                }
//            }
//            catch (Exception ex)
//            {
//                PublishProcesError(msg, ex, typeof(IEntityFound<TView>));

//            }
//        }

//        public static void LoadPulledEntityViewSetWithChanges(ILoadPulledEntityViewSetWithChanges<TView, IMatchType> msg)
//        {
//            try
//            {
//                using (var ctx = new TDbContext())
//                {
//                    var props = typeof(TView).GetProperties().ToList();
//                    var matchtype = msg.GetType().GenericTypeArguments[1];
//                    var whereStr = msg.Changes.Aggregate("", IPulledMatchTypeFunctions[matchtype]);
//                    whereStr = whereStr.TrimEnd('&');

//                    var entities = string.IsNullOrEmpty(whereStr)
//                        ? ctx.Set<TDbEntity>().OrderByDescending(x3 => x3.PatientVisitId)
//                            .Where(
//                                x =>
//                                    x.Questions.EntityAttributes.Entity == msg.EntityName &&
//                                    props.Any(z => z.Name == x.Questions.EntityAttributes.Attribute))
//                            .GroupBy(x => new { x.PatientVisit.PatientId })
//                            .Select(g => new
//                            {
//                                Id = g.Key.PatientId,
//                                Changes = g.SelectMany(q => q.Response)
//                                    .GroupBy(w => w.PatientResponses.Questions.EntityAttributes.Attribute)
//                                    .Select(rg => new KeyValuePair<string, dynamic>(
//                                        rg.Key,
//                                        rg.Any() ? rg.First().Value : null)).ToList()
//                            }).ToList()
//                        : ctx.PatientResponses
//                            .Where(x => x.Questions.EntityAttributes.Entity == msg.EntityName && props.Any(z => z.Name == x.Questions.EntityAttributes.Attribute))
//                            .Where($"Response.Where({whereStr}).Any()")
//                            .GroupBy(x => new { x.PatientVisit.PatientId })
//                            .Select(g => new
//                            {
//                                Id = g.Key.PatientId,
//                                Changes = g.SelectMany(q => q.Response)
//                                    .GroupBy(w => w.PatientResponses.Questions.EntityAttributes.Attribute)
//                                    .Select(rg => new KeyValuePair<string, dynamic>(
//                                        rg.Key,
//                                        rg.Any() ? rg.First().Value : null)).ToList()
//                            }).ToList();
//                    var res = entities.Select(x => new TDbView() { Id = x.Id }.ApplyChanges(x.Changes)).ToList();

//                    EventMessageBus.Current.Publish(new EntityViewSetWithChangesLoaded<TView>(res.OrderByDescending(x => x.Id).Select(x => (TView)(object)x).ToList(), msg.Changes, new StateEventInfo(msg.Process.Id, EntityView.Events.EntityViewFound), msg.Process, Source), Source);
//                }
//            }
//            catch (Exception ex)
//            {
//                PublishProcesError(msg, ex, typeof(IEntityViewLoaded<TView>));
//            }

//        }


//    }
//}
