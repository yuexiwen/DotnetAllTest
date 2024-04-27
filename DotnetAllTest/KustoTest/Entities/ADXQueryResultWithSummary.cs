namespace KustoTest.Entities
{
    using Newtonsoft.Json;
    using System.Data;
    using System.Runtime.ExceptionServices;

    public class ADXQueryResultWithSummary
    {
        public int TotalQueryRowsResult { get; set; }

        public List<KustoResultColumn> Columns { get; private set; }

        public List<List<object>> RowValues { get; private set; }

        public string QueryExecutionStatsJson { get; private set; }

        public QueryExecutionStatsInfo QueryStats { get; private set; }

        private DataBaseCursor DataBaseCursorInfo;

        public ADXQueryResultWithSummary(IDataReader dataReader, bool isTotalQueryIncluded, bool isToGetCursorAndQueryExecutionStats = true) 
        {
            using (dataReader)
            {
                if (isTotalQueryIncluded)
                {
                    dataReader.Read();
                    this.TotalQueryRowsResult = (int)(long)dataReader.GetValue(0);
                    dataReader.NextResult();
                }
                else
                {
                    this.TotalQueryRowsResult = -1;
                }

                this.PopulateColumns(dataReader);
                this.PopulateRows(dataReader);
            }
        }

        private void PopulateRows(IDataReader dataReader)
        {
            this.RowValues = new List<List<object>>();
            while (dataReader.Read())
            {
                List<object> row = new List<object>();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    row.Add(dataReader.GetValue(i));
                }

                this.RowValues.Add(row);
            }
        }

        private void PopulateColumns(IDataReader dataReader)
        {
            this.Columns = new List<KustoResultColumn>();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                this.Columns.Add(new KustoResultColumn(dataReader.GetName(i), dataReader.GetDataTypeName(i)));
            }
        }

        public class DataBaseCursor
        {
            public string Cursor { get; set; }
        }

        public class KustoResultColumn
        {
            public string ColumnName { get; private set; }

            public string ColumnType { get; private set; }

            public KustoResultColumn(string columnName, string columnType)
            {
                this.ColumnName = columnName;
                this.ColumnType = columnType;
            }
        }

        public class Memory
        {
            [JsonProperty(PropertyName = "hits")]
            public long Hits { get; set; }

            [JsonProperty(PropertyName = "misses")]
            public long Misses { get; set; }

            [JsonProperty(PropertyName = "total")]
            public long Total { get; set; }
        }

        public class Disk
        {
            [JsonProperty(PropertyName = "hits")]
            public long Hits { get; set; }

            [JsonProperty(PropertyName = "misses")]
            public long Misses { get; set; }

            [JsonProperty(PropertyName = "total")]
            public long Total { get; set; }
        }

        public class Shards
        {
            [JsonProperty(PropertyName = "hitbytes")]
            public long HitBytes { get; set; }

            [JsonProperty(PropertyName = "missbytes")]
            public long MissBytes { get; set; }

            [JsonProperty(PropertyName = "bypassbytes")]
            public long ByPassBytes { get; set; }
        }

        public class Cache
        {
            [JsonProperty(PropertyName = "memory")]
            public Memory Memory { get; set; }

            [JsonProperty(PropertyName = "disk")]
            public Disk Disk { get; set; }

            [JsonProperty(PropertyName = "shards")]
            public Shards Shards { get; set; }
        }

        public class Cpu
        {
            [JsonProperty(PropertyName = "user")]
            public string User { get; set; }

            [JsonProperty(PropertyName = "kernel")]
            public string Kernel { get; set; }

            [JsonProperty(PropertyName = "total cpu")]
            public string TotalCpu { get; set; }
        }

        public class Memory2
        {
            [JsonProperty(PropertyName = "peak_per_node")]
            public long PeakPerNode { get; set; }
        }

        public class ResourceUsage
        {
            [JsonProperty(PropertyName = "cache")]
            public Cache Cache { get; set; }

            [JsonProperty(PropertyName = "cpu")]
            public Cpu Cpu { get; set; }

            [JsonProperty(PropertyName = "memory")]
            public Memory2 Memory { get; set; }
        }

        public class Extents
        {
            [JsonProperty(PropertyName = "total")]
            public long Total { get; set; }

            [JsonProperty(PropertyName = "scanned")]
            public long Scanned { get; set; }

            [JsonProperty(PropertyName = "scanned_min_datetime")]
            public DateTime ScannedMinDatetime { get; set; }

            [JsonProperty(PropertyName = "scanned_max_datetime")]
            public DateTime ScannedMaxDateTime { get; set; }
        }

        public class Rows
        {
            [JsonProperty(PropertyName = "total")]
            public long Total { get; set; }

            [JsonProperty(PropertyName = "scanned")]
            public long Scanned { get; set; }
        }

        public class Rowstores
        {
            [JsonProperty(PropertyName = "scanned_rows")]
            public long ScannedRows { get; set; }

            [JsonProperty(PropertyName = "scanned_values_size")]
            public long ScannedValuesSize { get; set; }
        }

        public class InputDatasetStatistics
        {
            [JsonProperty(PropertyName = "extents")]
            public Extents Extents { get; set; }

            [JsonProperty(PropertyName = "rows")]
            public Rows Rows { get; set; }

            [JsonProperty(PropertyName = "rowstores")]
            public Rowstores Rowstores { get; set; }
        }

        public class DatasetStatistic
        {
            [JsonProperty(PropertyName = "table_row_count")]
            public long TableRowCount { get; set; }

            [JsonProperty(PropertyName = "table_size")]
            public long TableSize { get; set; }
        }

        public class QueryExecutionStatsInfo
        {
            [JsonProperty(PropertyName = "ExecutionTime")]
            public double ExecutionTime { get; set; }

            [JsonProperty(PropertyName = "resource_usage")]
            public ResourceUsage ResourceUsage { get; set; }

            [JsonProperty(PropertyName = "input_dataset_statistics")]
            public InputDatasetStatistics InputDatasetStatistics { get; set; }

            [JsonProperty(PropertyName = "dataset_statistics")]
            public List<DatasetStatistic> DatasetStatistics { get; set; }
        }
    }
}
