namespace DeltaLakeSingletonTest.Expressions
{
    public interface IExpression
    {
        public List<IExpression> GetChildren();
    }
}
