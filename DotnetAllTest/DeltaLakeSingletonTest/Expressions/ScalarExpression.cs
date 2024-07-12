namespace DeltaLakeSingletonTest.Expressions
{
    public class ScalarExpression : IExpression
    {
        protected readonly string name;

        protected readonly List<IExpression> children;

        public ScalarExpression(string name, List<IExpression> children)
        {
            this.name = name;
            this.children = children;
        }

        public string Name {  get { return name; } }

        public List<IExpression> GetChildren()
        {
            return children;
        }
        
        override
        public string ToString()
        {
            return $"{name}({string.Join(", ", children.Select(i => i.ToString()))})";
        }
    }
}
