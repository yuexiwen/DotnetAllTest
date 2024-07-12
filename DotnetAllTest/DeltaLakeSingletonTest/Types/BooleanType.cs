namespace DeltaLakeSingletonTest.Types
{
    public class BooleanType : BasePrimitiveType
    {
        public static readonly BooleanType BOOLEAN = new();

        private BooleanType() : base("boolean")
        {
        }
    }
}
