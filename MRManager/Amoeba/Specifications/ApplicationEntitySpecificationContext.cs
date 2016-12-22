using System.Linq;
using LinqSpecs;
using T4Entities;

namespace Specifications
{
    public class ApplicationEntitySpecificationContext
    {
        public static Specification<ApplicationEntity> EntityPropertiesHasDataProperty { get; } = new ExpressionSpecification<ApplicationEntity>(x => x.Entity.EntityProperties.All(z => z.DataProperties.Any()));
        public static Specification<ApplicationEntity> DataPropertyHasModelType { get; } = new ExpressionSpecification<ApplicationEntity>(x => x.Entity.EntityProperties.All(z => z.DataProperties.All(t => t.ModelType != null)));


        public static Specification<ApplicationEntity> WithAppId(int appId)
        {
            return new ExpressionSpecification<ApplicationEntity>(x => x.ApplicationId == appId);
        }

        public static Specification<ApplicationEntity> GetEntitiesSpec(int appId)
        {
            return  WithAppId(appId)
                       & EntityPropertiesHasDataProperty
                       & DataPropertyHasModelType;
        }
    }
}
