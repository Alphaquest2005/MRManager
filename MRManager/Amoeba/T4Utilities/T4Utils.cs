using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using T4Entities;


    public static class T4Utilities
    {
        public static List<ApplicationEntity> GetMainEntities(List<ApplicationEntity> entities)
        {
            return
                entities.Where(
                    x =>
                        !x.Entity.EntityProperties.SelectMany(xx => xx.ParentRelationships).Any() &&
                        x.Entity.EntityProperties.SelectMany(xx => xx.ChildRelationships).Any()
                        &&
                        x.Entity.EntityProperties.SelectMany(xx => xx.DataProperties)
                            .Any(
                                xx =>
                                    xx.ModelType.Name == "EntityId" &&
                                    (xx.PrimaryKeyOption == null || xx.PrimaryKeyOption.IsCalculated == true))).ToList();
        }

        public static List<Entity> GetEntitiesForMainEntity(Entity mainEntity)
        {
            try
            {
                var childres =
                    mainEntity.EntityProperties.SelectMany(x => x.ChildRelationships)
                        .Where(
                            x =>
                                x != null && x.ChildProperty.Entity.Id != mainEntity.Id && x.ChildProperty != null &&
                                x.ChildProperty.Entity != null)
                        .Select(x => x.ChildProperty.Entity)
                        .Distinct()
                        .ToList();
                var res = new List<Entity>();
                foreach (var c in childres)
                {
                    res.AddRange(GetEntitiesForMainEntity(c));
                }
                res.Add(mainEntity);
                return res;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private static string GetPropertyString(bool? isMany, EntityProperty p, bool? prevIsMany, bool isFirstDefault)
        {
            if (isFirstDefault) return p.PropertyName;
            if (prevIsMany == null) return p.PropertyName;
            if (isMany == null) return p.PropertyName;
            if (prevIsMany == true)
            {
                return string.Format("Select(z => z.{0})", p.PropertyName);//Select(z => z.{0})
        }
            else
            {
                if (isMany == true)
                {
                    return string.Format("Select(z => z.{0})", p.PropertyName);//Select(z => z.{0})
            }
                else
                {
                    return p.PropertyName;
                }
            }


        }


        private static void GetAutoEntityView(Entity Entity, Entity prevEntity, Entity entity, string navPath,
            ref Dictionary<string, dynamic> res,
            int level, ref Stack prevIsMany, bool? isMany, bool everIsMany, bool isFirstDefault)
        {
            
            foreach (var p in entity.EntityProperties)
            {
                if (p.DataProperties.Any() && p.DataProperties.All(x => x.ModelType.Name == "EntityId") && level <= 1)
                {
                    prevEntity = entity;

                    var prlst = p.ChildRelationships.Where(x => x.ChildProperty.Entity != entity);
                    if (prlst.Any())
                    {
                        for (int i = 0; i < prlst.Count(); i++)
                        {
                            var crel = prlst.ElementAt(i);

                            var curEntity = crel.ChildProperty.Entity;
                            if (
                                curEntity.EntityProperties.SelectMany(x => x.DataProperties)
                                    .Count(x => x.ModelType.Name == "ForeignKey") > 2)
                            {
                                continue;
                            }
                            if (Entity == curEntity)
                            {
                                continue;
                            }
                            GetIsCurrentEntityMany(prevEntity, curEntity, out isMany);
                            if (isMany == null)
                            {
                                continue;
                            }
                            if (isMany == true) everIsMany = true;
                            if (isMany == true) everIsMany = true;


                        var nav = "";
                        if (!(navPath.Contains("FirstOrDefault")))
                        {
                            nav = NavString(level == 0 ? false : (bool)prevIsMany.Peek(), isMany, curEntity, i,
                                  false, out isFirstDefault); //i == etrail.Value.Count - 1
                        }
                        else
                        {
                            nav = curEntity.EntitySetName + ".";
                        }
                        prevIsMany.Push(isMany);
                            var resNavPath = navPath + nav;

                            GetAutoEntityView(Entity, prevEntity, curEntity, resNavPath, ref res, level + 1,
                                ref prevIsMany,
                                isMany, everIsMany, isFirstDefault);
                            prevIsMany.Pop();
                        }

                    }
                }

                if (p.DataProperties.Any() && p.DataProperties.All(x => x.ModelType.Name == "ForeignKey") && level <= 1)
                {
                    //var etrail = leafEntities.FirstOrDefault().Value.Where(x => x.EntityProperties);
                    //if (etrail.Equals(default(KeyValuePair<Entity, List<Entity>>))) continue;
                    prevEntity = entity;


                    var rlst = p.ParentRelationships.Where(x => x.ParentProperty.Entity != entity);
                    if (rlst.Any())
                    {
                        for (int i = 0; i < rlst.Count(); i++)
                        {
                            var prel = rlst.ElementAt(i);
                            var curEntity = prel.ParentProperty.Entity;
                            if (Entity == curEntity)
                            {
                                continue;
                            }
                            if (
                                curEntity.EntityProperties.SelectMany(x => x.DataProperties)
                                    .Count(x => x.ModelType.Name == "ForeignKey") > 2)
                            {
                                continue;
                            }

                            GetIsCurrentEntityMany(prevEntity, curEntity, out isMany);
                            if (isMany == null)
                            {
                                continue;
                            }
                            if (isMany == true) everIsMany = true;


                            var nav = "";
                            if (!(navPath.Contains("FirstOrDefault")))
                            {
                              nav =  NavString(level == 0 ? false : (bool) prevIsMany.Peek(), isMany, curEntity, i,
                                    false, out isFirstDefault); //i == etrail.Value.Count - 1
                            }
                            else
                            {
                                nav = curEntity.EntitySetName + ".";
                            }
                            prevIsMany.Push(isMany);
                            var resNavPath = navPath + nav;


                            GetAutoEntityView(Entity, prevEntity, curEntity, resNavPath, ref res, level + 1,
                                ref prevIsMany,
                                isMany, everIsMany, isFirstDefault);

                            prevIsMany.Pop();

                        }

                    }

                }

            }

            var entityNameProps = entity.EntityProperties.Where(x => x.DataProperties.Any(z =>
                z.ModelType.Name == "EntityName" ||
                (z.ModelType.Name == "Attribute" && z.EntityProperty.PropertyName == "Value"))).ToList();
            foreach (var prop in entityNameProps)
            {

                if (isMany == null) continue;
                var key = res.Keys.Contains(prop.Entity.Name)
                    ? prevEntity == null ? "" : prevEntity.Name + prop.Entity.Name
                    : prop.Entity.Name;
                dynamic expando = new ExpandoObject();
                expando.DataType = prop.DataProperties.FirstOrDefault() == null?"string": prop.DataProperties.FirstOrDefault().DataType.Name;
                isFirstDefault = navPath.Contains("FirstOrDefault()") ? true : false;
                var ending = GetPropertyString(prevIsMany.Count >= 1 ? (bool?) prevIsMany.Peek() : false, prop,
                    prevIsMany.Contains(true), isFirstDefault);

                var val = String.Format("{0}{1}{2}", navPath, ending, prevIsMany.Contains(true) && !isFirstDefault?".FirstOrDefault()":"");
                expando.ViewPath = val;
                
                res.Add(key, expando);

            }

        }

        public static Dictionary<string, dynamic> GetAutoEntityView(Entity Entity)
        {
            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();
            var prevIsMany = new Stack();
            GetAutoEntityView(Entity, null, Entity, "", ref res, 0, ref prevIsMany, null, false, false);
            return res;
        }


        public static void GetLeafEntities(Entity entity, ref Dictionary<Entity, List<List<Entity>>> leafEntities,
            ref List<Entity> trail)
        {
            if (trail.Contains(entity)) return;
           // if (leafEntities.Keys.Contains(entity)) return;

            trail.Add(entity);

            var cRels = entity.EntityProperties.SelectMany(x => x.ChildRelationships);
            var pRels = entity.EntityProperties.SelectMany(x => x.ParentRelationships);

            var isLeaf = (!cRels.Any() && pRels.Count() == 1) ||
                         entity.EntityProperties.Any(x => x.DataProperties.Any(z => z.ModelType.Name == "EntityName"));
            if (!isLeaf)
            {
                foreach (var pr in pRels)
                {
                    if (!trail.Contains(pr.ParentProperty.Entity))
                        GetLeafEntities(pr.ParentProperty.Entity, ref leafEntities, ref trail);
                }

                foreach (var cr in cRels)
                {
                    if (!trail.Contains(cr.ChildProperty.Entity))
                        GetLeafEntities(cr.ChildProperty.Entity, ref leafEntities, ref trail);
                }
            }
            if (leafEntities.Keys.Contains(entity))
            {
                var lst = new List<List<Entity>>();
                leafEntities.TryGetValue(entity, out lst);
                lst.Add(new List<Entity>(trail));
            }
            else
            {
                leafEntities.Add(entity, new List<List<Entity>>() {new List<Entity>(trail)});
            }

            //trail = new List<Entity>();
            trail.Remove(entity);
        }



        public static void GetIsCurrentEntityMany(Entity prevEntity, Entity CurEntity, out bool? isMany)
        {
            EntityRelationship rel = null;
            EntityProperty relProp = null;
            if (
                prevEntity.EntityProperties.SelectMany(x => x.ChildRelationships)
                    .FirstOrDefault(x => x.ChildProperty.Entity == CurEntity) != null)
            {
                rel = prevEntity.EntityProperties.SelectMany(x => x.ChildRelationships)
                    .FirstOrDefault(x => x.ChildProperty.Entity == CurEntity);
                relProp = rel.ChildProperty;
                isMany = relProp.DataProperties.All(x => x.ModelType.Name == "ForeignKey") &&
                         !(relProp.DataProperties.All(x => x.ModelType.Name == "EntityId"));
                return;
            }
            else if (
                prevEntity.EntityProperties.SelectMany(x => x.ParentRelationships)
                    .FirstOrDefault(x => x.ParentProperty.Entity == CurEntity) != null)
            {
                rel = prevEntity.EntityProperties.SelectMany(x => x.ParentRelationships)
                    .FirstOrDefault(x => x.ParentProperty.Entity == CurEntity);
                relProp = rel.ParentProperty;
                isMany = relProp.DataProperties.All(x => x.ModelType.Name == "ForeignKey") &&
                         !(relProp.DataProperties.All(x => x.ModelType.Name == "EntityId"));
                return;
            }

            isMany = null;

        }

        public static string NavString(bool? prevIsMany, bool? isMany, Entity curEntity, int i, bool isLast,
            out bool isFirstDefault)
        {

            if (prevIsMany == null)
            {
                isFirstDefault = false;
                return curEntity.EntitySetName + ".";
            }

            if (prevIsMany == true)
            {
                //if (isLast)
                //{
                //    if (isMany == true)
                //    {

                //        return string.Format("SelectMany(x{0} => x{0}.{1}).", i, curEntity.EntitySetName);
                //    }
                //    else
                //    {
                //        return string.Format("Select(x{0} => x{0}.{1}).", i, curEntity.EntitySetName);
                //    }
                //}
                //else
                //{
                if (isMany == true)
                {
                    isFirstDefault = false;
                    return string.Format("SelectMany(x{0} => x{0}.{1}).", i, curEntity.EntitySetName);
                }
                else
                {
                    //if (
                    //    !(curEntity.EntityProperties.SelectMany(x => x.DataProperties)
                    //        .All(x => x.ModelType.Name != "ForeignKey")))
                    //{
                    //    isFirstDefault = true;
                    //    return string.Format("FirstOrDefault().{1}.", i, curEntity.EntitySetName);
                    //}
                    //else
                    //{
                        isFirstDefault = false;
                        return string.Format("Select(x{0} => x{0}.{1}).", i, curEntity.EntitySetName);
                    //}

                }
                //}

            }
            else
            {
                
                    isFirstDefault = false;
                    return curEntity.EntitySetName + ".";
                

            }

        }

        public static string GetPropertyString(bool? isMany, EntityViewProperty p, bool? prevIsMany, bool isFirstOrDefault)
        {
            if(isFirstOrDefault) return p.EntityProperty != null
                    ? p.EntityProperty.PropertyName
                    : p.EntityViewViewProperty.EntityProperty.PropertyName;
             if (prevIsMany == null)
                return p.EntityProperty != null
                    ? p.EntityProperty.PropertyName
                    : p.EntityViewViewProperty.EntityProperty.PropertyName;
            if (isMany == null)
                return p.EntityProperty != null
                    ? p.EntityProperty.PropertyName
                    : p.EntityViewViewProperty.EntityProperty.PropertyName;
            if (prevIsMany == true)
            {
                return string.Format("Select(z => z.{0})",
                    p.EntityProperty != null
                        ? p.EntityProperty.PropertyName
                        : p.EntityViewViewProperty.EntityProperty.PropertyName);
            }
            else
            {
                if (isMany == true)
                {
                    return string.Format("Select(z => z.{0})",
                        p.EntityProperty != null
                            ? p.EntityProperty.PropertyName
                            : p.EntityViewViewProperty.EntityProperty.PropertyName);
                }
                else
                {
                    return p.EntityProperty.PropertyName;
                }
            }


        }
    }

