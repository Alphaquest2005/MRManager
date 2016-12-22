using System.Linq;
using LinqSpecs;
using T4Entities;

namespace Specifications
{
    public class EntitySpecificationContext
    {
        public static Specification<Entity> HasEntityProperties { get; } = new ExpressionSpecification<Entity>(x => x.EntityProperties.Any());

        public static Specification<Entity> AllEntityPropertyHasDataProperty { get; } = new ExpressionSpecification<Entity>(x => x.EntityProperties.All(z => z.DataProperties.Any()));

        public static Specification<Entity> HasEntityId { get; } = new ExpressionSpecification<Entity>(x => x.EntityProperties.Any(z => z.DataProperties.Any(t =>  t.ModelType.Name == "EntityId")));
    }
}
