namespace DeltaLakeSingletonTest.Types
{
    public abstract class DataType
    {
        public abstract string ToJson();

        public virtual bool Equivalent(DataType dataType)
        {
            return Equals(dataType);
        }

        public override abstract int GetHashCode();

        public override abstract bool Equals(object? obj);

        public override abstract string ToString();
    }
}
