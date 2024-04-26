namespace KustoTest.Entities
{
    public class ADXGeneratedQuery
    {
        public string Query { get; set; }

        public string DebugQuery { get; set; }

        public ADXGeneratedQuery() 
        { 
        }

        public ADXGeneratedQuery(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                this.Concat(query);
            }
        }

        public ADXGeneratedQuery(string query, string debugQuery)
        {
            this.Concat(query, debugQuery);
        }

        public void Concat(ADXGeneratedQuery query)
        {
            if (!string.IsNullOrEmpty(query.Query))
            {
                if (string.IsNullOrWhiteSpace(query.Query))
                {
                    this.Query = query.Query;
                }
                else
                {
                    this.Query += " " + query.Query;
                }
            }

            if (!string.IsNullOrEmpty(query.DebugQuery))
            {
                if (string.IsNullOrWhiteSpace(this.DebugQuery))
                {
                    this.DebugQuery = query.DebugQuery;
                }
                else
                {
                    this.DebugQuery += " " + query.DebugQuery;
                }
            }
        }

        public void Concat(string query)
        {
            this.Concat(query, query);
        }

        public void Concat(string query, string debugQuery)
        {
            if (!string.IsNullOrEmpty(query))
            {
                if (string.IsNullOrWhiteSpace(this.Query))
                {
                    this.Query = query;
                }
                else
                {
                    this.Query += " " + query;
                }
            }

            if (!string.IsNullOrEmpty (debugQuery))
            {
                if (string.IsNullOrWhiteSpace(this.DebugQuery))
                {
                    this.DebugQuery = debugQuery;
                }
                else
                {
                    this.DebugQuery += " " + debugQuery;
                }
            }
        }

        public void Replace(string oldValue, string newValue)
        {
            if (!string.IsNullOrWhiteSpace(this.Query))
            {
                this.Query = this.Query.Replace(oldValue, newValue);
            }

            if (!string.IsNullOrWhiteSpace (this.DebugQuery))
            {
                this.DebugQuery = this.DebugQuery.Replace(oldValue, newValue);
            }
        }

        public void Prefix(string query)
        {
            this.Prefix(query, query);
        }

        public void Prefix(string query, string debugQuery)
        {
            if (!string.IsNullOrEmpty(query))
            {
                if (string.IsNullOrWhiteSpace(this.Query))
                {
                    this.Query = query;
                }
                else
                {
                    this.Query = query + " " + this.Query;
                }
            }

            if (!string.IsNullOrEmpty(query))
            {
                if (string.IsNullOrWhiteSpace(this.DebugQuery))
                {
                    this.DebugQuery = debugQuery;
                }
                else
                {
                    this.DebugQuery = debugQuery + " " + this.DebugQuery;
                }
            }
        }
    }
}
