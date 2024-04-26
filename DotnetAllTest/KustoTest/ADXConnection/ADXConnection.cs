namespace KustoTest.ADXConnection
{
    using Kusto.Data;
    using Kusto.Data.Common;
    using Kusto.Data.Net.Client;
    using System.Data;

    internal class ADXConnection : IADXConnection
    {
        public string KustoClusterName { get; }

        public string KustoQueryUrl { get; }

        public string KustoQueryDatabase { get; }

        public string GetPrintString => $"{this.KustoClusterName}/{this.KustoQueryDatabase}";

        public Semaphore SemaphoreParallelRunningJobs { get; }

        public ICslQueryProvider CslQueryProvider { get; private set; }

        public ICslAdminProvider CslAdminProvider { get; private set; }

        public ADXConnection(string kustoQueryUrlParam, string kustoQueryDatabaseParam, int maxParallelRunningJobs = 30)
        {
            this.KustoQueryUrl = kustoQueryUrlParam;
            this.KustoQueryDatabase = kustoQueryDatabaseParam;
            this.KustoClusterName = new Uri(this.KustoQueryUrl).Host;
            this.SemaphoreParallelRunningJobs = new Semaphore(maxParallelRunningJobs, maxParallelRunningJobs * 2);
            this.Init();
        }

        private void Init()
        {
            KustoConnectionStringBuilder kustoQueryConn = new KustoConnectionStringBuilder(this.KustoQueryUrl)
            {
                InitialCatalog = this.KustoQueryDatabase
            }.WithAadUserPromptAuthentication();

            try
            {
                this.CslQueryProvider = KustoClientFactory.CreateCslQueryProvider(kustoQueryConn);
                this.CslAdminProvider = KustoClientFactory.CreateCslAdminProvider(kustoQueryConn);
            }
            catch (Exception e) 
            {
                Console.WriteLine($"exception when create adx provider : {e.Message}");
                throw;
            }
        }

        public async Task<IDataReader> ExecuteQuery(string query, bool isCommand, string clientRequestId, bool isToRetry, int maxRetires, int retryWaitTimeMs, int? totalTimeOutInMs)
        {
            ClientRequestProperties clientRequestProperties = GetClientRequestProperties(query, isCommand);
            clientRequestProperties.ClientRequestId = clientRequestId;
            
            try
            {
                Task? timeoutWaitTask = null;
                if (totalTimeOutInMs != null)
                {
                    timeoutWaitTask = Task.Delay(totalTimeOutInMs.Value);
                }


                this.SemaphoreParallelRunningJobs.WaitOne();

                int loopCount = 0;

                while (true)
                {
                    try
                    {
                        Task<IDataReader> task = isCommand ? this.CslAdminProvider.ExecuteControlCommandAsync(this.KustoQueryDatabase, query, clientRequestProperties) :
                            this.CslQueryProvider.ExecuteQueryAsync(this.KustoQueryDatabase, query, clientRequestProperties);

                        if (timeoutWaitTask != null)
                        {
                            if (task == await Task.WhenAny(task, timeoutWaitTask))
                            {
                                return await task;
                            }

                            throw new TimeoutException("ExecuteQuery Timed out");
                        }
                        else
                        {
                            return await task;
                        }
                    }
                    catch (TimeoutException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        if (loopCount == maxRetires || !isToRetry)
                        {
                            throw;
                        }
                    }

                    loopCount++;
                    await Task.Delay(retryWaitTimeMs);
                }

                throw new Exception("ExecuteQuery failed.");
            }
            finally
            {
                this.SemaphoreParallelRunningJobs.Release();
            }
        }

        private static ClientRequestProperties GetClientRequestProperties(string query, bool isCommand)
        {
            ClientRequestProperties clientRequestProperties;

            if (isCommand)
            {
                if (query.StartsWith(".set-or-append async", System.StringComparison.OrdinalIgnoreCase))
                {
                    clientRequestProperties = new ClientRequestProperties();
                    clientRequestProperties.SetOption(ClientRequestProperties.OptionServerTimeout, TimeSpan.FromSeconds(10));
                    clientRequestProperties.SetOption(ClientRequestProperties.OptionQueryFanoutThreadsPercent, 25);
                }
                else
                {
                    clientRequestProperties = new ClientRequestProperties();
                    clientRequestProperties.SetOption(ClientRequestProperties.OptionServerTimeout, TimeSpan.FromSeconds(60));
                }
            }
            else
            {
                clientRequestProperties = new ClientRequestProperties();
                clientRequestProperties.SetOption(ClientRequestProperties.OptionQueryConsistency, ClientRequestProperties.OptionQueryConsistency_Weak);
                clientRequestProperties.SetOption(ClientRequestProperties.OptionServerTimeout, TimeSpan.FromSeconds(10));
            }

            return clientRequestProperties;
        }
    }
}
