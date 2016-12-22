using System.Linq;
using LinqSpecs;
using T4Entities;

namespace Specifications
{
    public class EntityPropertySpecificationContext
    {
        public static Specification<EntityProperty> HasDataProperty { get; } = new ExpressionSpecification<EntityProperty>(x => x.DataProperties.Any());

    }
}
