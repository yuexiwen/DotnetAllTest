using Microsoft.Azure.Cosmos;

namespace CosmosWideTablePoc
{
    internal class ConnectionStartEntity(UUIDGenerator generator, CosmosConnect connect) : UUIDConsumer(generator), DataGenerator
    {
        public record Schema(
            string id,
            string State,
            string ClientOS,
            string ClientType,
            string PlatformName,
            string PlatformVersion,
            string PredecessorConnectionId,
            string ResourceType,
            string ResourceAlias,
            string SessionHostName
            );

        private List<string> StateLst { get; set; } = [];

        private List<string> ClientOSLst { get; set; } = [];

        private List<string> ClientVersion { get; set; } = [];

        private List<string> ClientType { get; set; } = [];

        private List<string> PlatformName { get; set; } = [];

        private List<string> PlatformVersion { get; set; } = [];

        private List<string> PredecessorConnectionId { get; set; } = [];

        private List<string> ResourceType { get; set; } = [];

        private List<string> ResourceAlias { get; set; } = [];

        private List<string> SessionHostName { get; set; } = [];

        private List<string> SessionHostPoolType { get; set; } = [];

        public List<string> PrimaryKeyList { get; set; } = [];

        public List<List<PatchOperation>> OperatorList { get; set; } = [];

        public List<Schema> DocmentList { get; set; } = [];

        public CosmosConnect Connect { get; set; } = connect;

        public override async void ComsumeAction()
        {
            this.PrimaryKeyList = this.UUIDLst;
            this.RandomGenerateField();
            this.OperationGenerate();
            this.DocumentGenerate();
            await this.Connect.CreateOrUpdateItem<Schema>(this.PrimaryKeyList, this.DocmentList, this.OperatorList);
            Console.WriteLine("connect batch process complete!");
        }

        public void DocumentGenerate()
        {
            this.DocmentList.Clear();
            for (int i = 0; i < this.PrimaryKeyList.Count; i++)
            {
                this.DocmentList.Add(new Schema(
                    id : this.PrimaryKeyList[i],
                    State : this.StateLst[i],
                    ClientOS : this.ClientOSLst[i],
                    ClientType : this.ClientType[i],
                    PlatformName : this.PlatformName[i],
                    PlatformVersion : this.PlatformVersion[i],
                    PredecessorConnectionId : this.PredecessorConnectionId[i],
                    ResourceType : this.ResourceType[i],
                    ResourceAlias : this.ResourceAlias[i],
                    SessionHostName : this.SessionHostName[i]
                    ));
            }
        }

        public void OperationGenerate()
        {
            this.OperatorList.Clear();
            for (int i = 0; i <  this.PrimaryKeyList.Count; i++)
            {
                this.OperatorList.Add(
                [
                    PatchOperation.Set("/Status", this.StateLst[i]),
                    PatchOperation.Set("/ClientOS", this.ClientOSLst[i]),
                    PatchOperation.Set("/ClientVersion", this.ClientVersion[i]),
                    PatchOperation.Set("/ClientType", this.ClientType[i]),
                    PatchOperation.Set("/PlatformName", this.PlatformName[i]),
                    PatchOperation.Set("/PlatformVersion", this.PlatformVersion[i]),
                    PatchOperation.Set("/PredecessorConnectionId", this.PredecessorConnectionId[i]),
                    PatchOperation.Set("/ResourceType", this.ResourceType[i]),
                    PatchOperation.Set("/ResourceAlias", this.ResourceAlias[i]),
                    PatchOperation.Set("/SessionHostName", this.SessionHostName[i])
                ]);
            }
        }

        public void RandomGenerateField()
        {
            this.StateLst.Clear();
            this.ClientOSLst.Clear();
            this.ClientVersion.Clear();
            this.ClientType.Clear();
            this.PlatformName.Clear();
            this.PlatformVersion.Clear();
            this.PredecessorConnectionId.Clear();
            this.ResourceType.Clear();
            this.ResourceAlias.Clear();
            this.SessionHostName.Clear();
            this.SessionHostPoolType.Clear();
            foreach (var _ in this.PrimaryKeyList)
            {
                this.StateLst.Add("Start");
                this.ClientOSLst.Add("OS_" + DataGenerator.GenerateRandomString(6));
                this.ClientVersion.Add("V_" + DataGenerator.GenerateRandomString(7));
                this.ClientType.Add("Type_" + DataGenerator.GenerateRandomString(3));
                this.PlatformName.Add("PN_" + DataGenerator.GenerateRandomString(5));
                this.PlatformVersion.Add("PV_" + DataGenerator.GenerateRandomString(5));
                this.PredecessorConnectionId.Add("CID_" + DataGenerator.GenerateRandomString(5));
                this.ResourceType.Add("RT_" + DataGenerator.GenerateRandomString(5));
                this.ResourceAlias.Add("RA_" + DataGenerator.GenerateRandomString(5));
                this.SessionHostName.Add("SHN_" + DataGenerator.GenerateRandomString(5));
                this.SessionHostPoolType.Add("SHP_" + DataGenerator.GenerateRandomString(5));
            }
        }
    }
}
