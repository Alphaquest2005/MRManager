using LinqSpecs;
using T4Entities;

namespace Specifications
{
    public class DataPropertySpecificationContext
    {
        public static Specification<DataProperty> IntZeroLength { get; } = new ExpressionSpecification<DataProperty>(x => x.DataType.Name == "int" && x.MaxLength == 0);
        public static Specification<DataProperty> HasModelType { get; } = new ExpressionSpecification<DataProperty>(x => x.ModelType != null);

    }
}
