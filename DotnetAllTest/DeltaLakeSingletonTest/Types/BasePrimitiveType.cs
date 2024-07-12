namespace DeltaLakeSingletonTest.Types
{
    public abstract class BasePrimitiveType : DataType
    {
        private static readonly Dictionary<string, DataType> nameToPrimitiveTypeMap = new Dictionary<string, DataType>
        {
            { "boolean", BooleanType.BOOLEAN}
        };

        private readonly string primitiveTypeName;

        protected BasePrimitiveType(string primitiveTypeName)
        {
            this.primitiveTypeName = primitiveTypeName;
        }

        
        public override bool Equals(object? o)
        {
            if (this == o) return true;
            
            if (o == null || GetType() != o.GetType()) return false;

            BasePrimitiveType that = (BasePrimitiveType)o;
            return primitiveTypeName.Equals(that.primitiveTypeName);
        }

        public override int GetHashCode()
        {
            return primitiveTypeName.GetHashCode();
        }

        public override string ToString()
        {
            return primitiveTypeName;
        }

        public override string ToJson()
        {
            return $"\"{primitiveTypeName}\"";
        }
    }
}
