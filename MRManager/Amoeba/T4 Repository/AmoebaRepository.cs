using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MoreLinq;
using Specifications;
using T4Entities;
using T4Repository.Properties;

namespace T4Repository
{
    public static class AmoebaRepository
    {

        public static Application GetApp(string appName)
        {
            try
            {
                
                using (var ctx = new AmoebaDBEntities(Settings.Default.DBConnectionString))
                {
                    return ctx.Applications
                        .Include(x => x.Settings)
                        .Include("Settings.Layer")
                        .Include("Settings.Project")
                        .First(x => x.Name == appName);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static List<ApplicationEntity> GetEntities(int appId)
        {

            var spec = ApplicationEntitySpecificationContext.GetEntitiesSpec(appId);

            using (var ctx = new AmoebaDBEntities(Settings.Default.DBConnectionString))
            {
                return ctx.ApplicationEntities//.Where(spec)
                                              .Where(ApplicationEntitySpecificationContext.WithAppId(appId))
                                              .Include(x => x.Entity)
                                              .Include(x => x.Entity.EntityProperties)
                                              .Include("Entity.EntityViews.EntityViewProperties.EntityProperty")
                                              .Include("Entity.EntityViews.EntityViewProperties.EntityViewViewProperty")
                                              .Include("Entity.EntityViews.EntityViewProperties.EntityViewPropertyFunctions")
                                              .Include("Entity.EntityViews.EntityViewProperties.EntityViewPropertyFunctions.Function")
                                              .Include("Entity.EntityViews.EntityViewProperties.EntityViewPropertyFunctions.EntityViewPropertyFunctionParameter.FunctionParameter.Parameter.DataType")
                                              .Include("Entity.EntityViews.EntityViewProperties.EntityViewPropertyFunctions.EntityViewPropertyFunctionParameter.FunctionParameter.Function.ReturnDataType.DataType")
                                              .Include("Entity.EntityViews.EntityViewProperties.EntityViewPropertyFunctions.EntityViewPropertyFunctionParameter")
                                              .Include("Entity.EntityProperties.TestValues")
                                              .Include("Entity.EntityProperties.ChildRelationships")
                                              .Include("Entity.EntityProperties.ChildRelationships.RelationshipType")
                                              .Include("Entity.EntityProperties.ChildRelationships.ParentProperty")
                                              .Include("Entity.EntityProperties.ChildRelationships.ParentProperty.Entity")
                                              .Include("Entity.EntityProperties.ChildRelationships.ChildProperty")
                                              .Include("Entity.EntityProperties.ChildRelationships.ChildProperty.Entity")
                                              .Include("Entity.EntityProperties.ChildRelationships.ChildProperty.Entity.EntityProperties.DataProperties")
                                              .Include("Entity.EntityProperties.ChildRelationships.ChildProperty.Entity.EntityProperties.DataProperties.PrimaryKeyOption")
                                              .Include("Entity.EntityProperties.ParentRelationships")
                                              .Include("Entity.EntityProperties.ParentRelationships.RelationshipType")
                                              .Include("Entity.EntityProperties.ParentRelationships.ParentProperty.Entity.EntityProperties.DataProperties.PrimaryKeyOption")
                                              .Include("Entity.EntityProperties.ParentRelationships.ChildProperty")
                                              .Include("Entity.EntityProperties.PresentationProperty")
                                              .Include("Entity.EntityProperties.DataProperties.PrimaryKeyOption")
                                              .Include("Entity.EntityProperties.DataProperties.DataType")
                                              .Include("Entity.EntityProperties.DataProperties.ModelType")
                                              
                                              .DistinctBy(x => x.Id).ToList();
            }
        }

        public static List<EntityView> GetEntityViews(int appId)
        {

           using (var ctx = new AmoebaDBEntities(Settings.Default.DBConnectionString))
            {
                return ctx.EntityViews.Where(x => x.Entity.ApplicationEntities.Any(z => z.ApplicationId == appId))
                    .Include(x => x.Entity)
                    .Include(x => x.Entity.EntityProperties)

                    .Include("Entity.EntityProperties.DataProperties.DataType")
                    .Include("Entity.EntityProperties.DataProperties.ModelType")
                    .Include("Entity.EntityProperties.DataProperties.PrimaryKeyOption")
                    .Include("Entity.EntityProperties.ChildRelationships")
                    .Include("Entity.EntityProperties.ChildRelationships.RelationshipType")
                    .Include("Entity.EntityProperties.ChildRelationships.ParentProperty")
                    .Include("Entity.EntityProperties.ChildRelationships.ParentProperty.Entity")
                    .Include("Entity.EntityProperties.ChildRelationships.ChildProperty")
                    .Include("Entity.EntityProperties.ChildRelationships.ChildProperty.Entity")
                    .Include("Entity.EntityProperties.ChildRelationships.ChildProperty.Entity.EntityProperties.DataProperties")
                    .Include("Entity.EntityProperties.ChildRelationships.ChildProperty.Entity.EntityProperties.DataProperties.PrimaryKeyOption")
                    .Include("Entity.EntityProperties.ParentRelationships")
                    .Include("Entity.EntityProperties.ParentRelationships.RelationshipType")
                    .Include("Entity.EntityProperties.ParentRelationships.ParentProperty.Entity.EntityProperties.DataProperties.PrimaryKeyOption")
                    .Include("Entity.EntityProperties.ParentRelationships.ChildProperty")


                    .Include("EntityViewProperties.EntityProperty.Entity")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.DataProperties.DataType")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.DataProperties.ModelType")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.DataProperties.PrimaryKeyOption")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ChildRelationships")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ChildRelationships.RelationshipType")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ChildRelationships.ParentProperty")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ChildRelationships.ParentProperty.Entity")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ChildRelationships.ChildProperty")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ChildRelationships.ChildProperty.Entity")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ChildRelationships.ChildProperty.Entity.EntityProperties.DataProperties")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ChildRelationships.ChildProperty.Entity.EntityProperties.DataProperties.PrimaryKeyOption")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ParentRelationships")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ParentRelationships.RelationshipType")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ParentRelationships.ParentProperty.Entity.EntityProperties.DataProperties.PrimaryKeyOption")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.ParentRelationships.ChildProperty")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.PresentationProperty")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.DataProperties.PrimaryKeyOption")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.DataProperties.DataType")
                    .Include("EntityViewProperties.EntityProperty.Entity.EntityProperties.DataProperties.ModelType")
                    .Include("EntityViewProperties.EntityViewViewProperty")
                    .Include("EntityViewProperties.EntityViewPropertyFunctions")
                    .Include("EntityViewProperties.EntityViewPropertyFunctions.Function")
                    .Include("EntityViewProperties.EntityViewPropertyFunctions.EntityViewPropertyFunctionParameter.FunctionParameter.Parameter.DataType")
                    .Include("EntityViewProperties.EntityViewPropertyFunctions.EntityViewPropertyFunctionParameter.FunctionParameter.Function.ReturnDataType.DataType")
                    .Include("EntityViewProperties.EntityViewPropertyFunctions.EntityViewPropertyFunctionParameter")
                    .ToList();
            }
        }

        public static List<Function> GetFunctions(int appId)
        {

            using (var ctx = new AmoebaDBEntities(Settings.Default.DBConnectionString))
            {
                return ctx.Functions.Where(x => x.ReturnDataType != null && x.EntityViewPropertyFunctions.Any(y => y.EntityViewProperty.EntityProperty.Entity.ApplicationEntities.Any(z => z.ApplicationId == appId)))
                    .Include(x => x.ReturnDataType)
                    .Include("FunctionBody")
                    .Include("FunctionParameters")
                    .Include("FunctionParameters.Parameter.DataType")
                    .ToList();
            }
        }

        //public static List<ApplicationStateMachine> GetStateMachines(int appId)
        //{
        //    using (var ctx = new AmoebaDBEntities(Properties.Settings.Default.DBConnectionString))
        //    {
        //        return ctx.ApplicationStateMachines.Where(x => x.ApplicationId == appId)
        //                                      .Include(x => x.StateMachine)
        //                                      .Include(x => x.StateMachine.MachineStates)
        //                                      .Include(x => x.StateMachine.MachineTriggers)
        //                                      .Include("StateMachine.MachineStates.State")
        //                                      .Include("StateMachine.MachineStates.Transitions.DestinationMachineState")
        //                                      .Include("StateMachine.MachineStates.Transitions.MachineTrigger")
        //                                      .Include("StateMachine.MachineStates.Transitions.MachineTrigger.Trigger")
        //                                      .Include("StateMachine.MachineStates.Transitions.MachineTrigger.Trigger.TriggerParameters")
        //                                      .Include("StateMachine.MachineStates.Transitions.MachineTrigger.Trigger.TriggerParameters.Parameter")
        //                                      .Include("StateMachine.MachineStates.Transitions.TransitionFunctions")
        //                                      .Include("StateMachine.MachineStates.Transitions.TransitionFunctions.FunctionSet")
        //                                      .Include("StateMachine.MachineTriggers.Trigger")
        //                                      .ToList();
        //    }
        //}

        //public static List<State> GetStates()
        //{
        //    using (var ctx = new AmoebaDBEntities(Properties.Settings.Default.DBConnectionString))
        //    {
        //        return ctx.States.ToList();
        //    }
        //}

        //public static List<Trigger> GetTriggers()
        //{
        //    using (var ctx = new AmoebaDBEntities(Properties.Settings.Default.DBConnectionString))
        //    {
        //        return ctx.Triggers.ToList();
        //    }
        //}

        public static
            List<FunctionSet> GetFunctionSets()
        {
            using (var ctx = new AmoebaDBEntities(Settings.Default.DBConnectionString))
            {
                return ctx.FunctionSets.ToList();
            }
        }

    }
}


