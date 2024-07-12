namespace DeltaLakeSingletonTest.Expressions
{
    public sealed class Column : IExpression
    {
        private readonly string[] names;

        public Column(string name) 
        { 
            this.names = [name];
        }

        public Column(string[] names)
        {
            this.names = names;
        }

        public string[] Names { get { return names; } }

        public List<IExpression> GetChildren()
        {
            return Enumerable.Empty<IExpression>().ToList();
        }

        override
        public string ToString()
        {
            return $"column({QuoteColumnPath(this.names)})";
        }

        override
        public bool Equals(object? o)
        {
            if (this == o) return true;
            if (o is null || this.GetType() != o.GetType()) return false;
            Column other = (Column) o;
            return Equals(names, other.names);
        }

        override
        public int GetHashCode()
        {
            int hashcode = 0;

            foreach (string name in names)
            {
                hashcode += name.GetHashCode();
            }

            return hashcode;
        }

        private static string QuoteColumnPath(string[] names)
        {
            return string.Join(".", names.Select(s => string.Format("`{0}`", s.Replace("`", "``"))));
        }
    }
}
