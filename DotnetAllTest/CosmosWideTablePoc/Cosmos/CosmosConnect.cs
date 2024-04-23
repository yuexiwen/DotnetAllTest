namespace CosmosWideTablePoc
{
    using Azure.Core;
    using Azure.Identity;
    using Microsoft.Azure.Cosmos;

    internal class CosmosConnect
    {
        private string? URL { get; set; }

        private string? DataBaseName { get; set; }

        private string? ContainerName { get; set; }

        private CosmosClient client { get; set;}

        private Database database { get; set; }

        private Container container { get; set; }

        public CosmosConnect(string url, string databaseName, string containerName) 
        {
            this.URL = url;
            this.DataBaseName = databaseName;
            this.ContainerName = containerName;
            TokenCredential credential = new DefaultAzureCredential();
            this.client = new(this.URL, tokenCredential: credential);
            this.database = client.CreateDatabaseIfNotExistsAsync(this.DataBaseName).Result;
            this.container = database.CreateContainerIfNotExistsAsync(this.ContainerName, "/id").Result;
        }

        public async Task CreateOrUpdateItem<T>(List<string> docid, List<T> docments, List<List<PatchOperation>> operations)
        {
            if (docid.Count != docments.Count || docid.Count != operations.Count)
            {
                Console.WriteLine("input error!");
                return;
            }

            for (int i = 0; i <  docid.Count; i++)
            {
                try
                {
                    ItemResponse<T> responseMessage = await container.ReadItemAsync<T>(docid[i], new PartitionKey(docid[i]));
                    await container.PatchItemAsync<T>(docid[i], new PartitionKey(docid[i]), operations[i]);
                }
                catch (Exception)
                {
                    try
                    {
                        await container.CreateItemAsync<T>(docments[i], new PartitionKey(docid[i]));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
