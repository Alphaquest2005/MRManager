namespace ExpressionComputations
{
    public class ComputedOutput<TSource, TValue>
    {
        public TSource Source { get; set; }
        public TValue Value { get; set; }
    }
}