// See https://aka.ms/new-console-template for more information
using Kusto.Data;
using Kusto.Data.Net.Client;

Console.WriteLine("Hello, World!");

var clusterUri = "https://cpccradxingestint01.eastus.kusto.windows.net";
var databaseName = "Report";
var query = "UpdatePolicy_autodenormalizationcomwrksdeviceuser_workspaceentity";
var kcsb = new KustoConnectionStringBuilder(clusterUri).WithAadUserPromptAuthentication();
var kustoclient = KustoClientFactory.CreateCslQueryProvider(kcsb);

using (var response = kustoclient.ExecuteQuery(databaseName, query, null))
{
    while (response.Read())
    {
        int columnNo = response.GetOrdinal("WorkspaceName");
        Console.WriteLine(response.GetString(columnNo));
    }
}
