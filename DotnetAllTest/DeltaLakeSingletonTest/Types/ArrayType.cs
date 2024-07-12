namespace DeltaLakeSingletonTest.Types
{
    public class ArrayType : DataType
    {
        private readonly DataType elementType;

        private readonly bool containsNull;

        public DataType ElementType { get { return elementType; } }

        public bool ContainsNull { get { return containsNull; } }

        public ArrayType(DataType elementType, bool containsNull)
        {
            this.elementType = elementType;
            this.containsNull = containsNull;
        }

        public override bool Equivalent(DataType dataType)
        {
            return dataType.GetType() == GetType() && ((ArrayType)dataType).ElementType.Equivalent(elementType);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ArrayType arrayType = (ArrayType)obj;
            return containsNull == arrayType.ContainsNull && object.Equals(elementType, arrayType.elementType);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(elementType, ContainsNull);
        }

        public override string ToJson()
        {
            return $"\"type\": \"array\"," +
                        $"\"elementType\": {elementType.ToJson()}" +
                        $"\"containsNull\": {containsNull}";
        }

        public override string ToString()
        {
            return $"array[{elementType}]";
        }
    }
}
