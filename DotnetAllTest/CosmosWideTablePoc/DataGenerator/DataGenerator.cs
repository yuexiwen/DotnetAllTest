namespace CosmosWideTablePoc
{
    internal interface DataGenerator
    {
        public abstract List<string> PrimaryKeyList { get; set; }

        public abstract List<string> OperatorList { get; set; }

        public abstract List<string> DocmentList { get; set; }

        public abstract void RandomGenerateField();

        public abstract void OperationGenerate();

        public abstract void DocumentGenerate();

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
