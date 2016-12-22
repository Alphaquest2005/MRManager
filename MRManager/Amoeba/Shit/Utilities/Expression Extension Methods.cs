using System.Linq.Expressions;

namespace Utilities
{
    public static class Expression_Extension_Methods
    {
        public static string Translate(this Expression exp)
        {
            return new QueryTranslator().Translate(exp);

        }
    }
}
