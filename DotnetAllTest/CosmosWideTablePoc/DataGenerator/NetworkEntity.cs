
using Microsoft.Azure.Cosmos;

namespace CosmosWideTablePoc
{
    internal class NetworkEntity(UUIDGenerator generator, CosmosConnect connect) : UUIDConsumer(generator), DataGenerator
    {
        public record Schema(
            string id,
            string RoundTripTime,
            string BandWidthMbp
            );

        private List<string> RoundTripTimes { get; set; } = [];

        private List<string> BandWidthMbps { get; set; } = [];

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
            Console.WriteLine("network batch process complete!");
        }

        public void DocumentGenerate()
        {
            this.DocmentList.Clear();
            for (int i = 0; i < this.PrimaryKeyList.Count; i++)
            {
                this.DocmentList.Add(new Schema(
                    id: this.PrimaryKeyList[i],
                    RoundTripTime: this.RoundTripTimes[i],
                    BandWidthMbp: this.BandWidthMbps[i]
                    ));
            }
        }

        public void OperationGenerate()
        {
            this.OperatorList.Clear();
            for (int i = 0; i < this.PrimaryKeyList.Count; i++)
            {
                this.OperatorList.Add(
                    [
                    PatchOperation.Set("/RoundTripTime", this.RoundTripTimes[i]),
                    PatchOperation.Set("/BandWidthMbp", this.RoundTripTimes[i])
                    ]);
            }
        }

        public void RandomGenerateField()
        {
            this.BandWidthMbps.Clear();
            this.RoundTripTimes.Clear();
            foreach (var _ in this.PrimaryKeyList)
            {
                this.RoundTripTimes.Add("RTT_" + DataGenerator.GenerateRandomString(6));
                this.BandWidthMbps.Add("BWM_" + DataGenerator.GenerateRandomString(6));
            }
        }
    }
}
