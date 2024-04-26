namespace KustoTest.ADXConnection
{
    using System.Data;

    internal interface IADXConnection
    {
        string KustoClusterName { get; }

        string KustoQueryUrl { get; }

        string KustoQueryDatabase { get; }

        string GetPrintString { get; }

        Task<IDataReader> ExecuteQuery(string query, bool isCommand, string clientRequestId, bool isToRetry, int maxRetires, int retryWaitTimeMs, int? totalTimeOutInMs);
    }
}
