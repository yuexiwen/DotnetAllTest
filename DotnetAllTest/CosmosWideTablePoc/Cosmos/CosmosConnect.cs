namespace CosmosWideTablePoc
{
    using Azure.Core;
    using Azure.Identity;
    using Microsoft.Azure.Cosmos;

    internal class CosmosConnect
    {
        public CosmosConnect() 
        {
            TokenCredential credential = new DefaultAzureCredential();
            CosmosClient client = new("", tokenCredential: credential);
        }
    }
}
