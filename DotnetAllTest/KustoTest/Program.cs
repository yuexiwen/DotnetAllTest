// See https://aka.ms/new-console-template for more information
/*
using Kusto.Data;
using Kusto.Data.Net.Client;

Console.WriteLine("Hello, World!");

var clusterUri = "https://cpccradxingestint01.eastus.kusto.windows.net";
var databaseName = "Report";
var query = "UpdatePolicy_autodenormalizationcomwrksdeviceuser_workspaceentity";
var query1 = ".show functions";
var kcsb = new KustoConnectionStringBuilder(clusterUri).WithAadUserPromptAuthentication();
var kustoclient = KustoClientFactory.CreateCslQueryProvider(kcsb);


using (var response = kustoclient.ExecuteQuery(databaseName, query1, null))
{
    while (response.Read())
    {
        //int columnNo = response.GetOrdinal("WorkspaceName");
        //Console.WriteLine(response.GetString(columnNo));
        var functionName = response.GetString(0);
        Console.WriteLine(functionName);
    }
}
*/

using KustoTest.ADXConnection;

ADXConnection adxConnection = new("https://cpccradxingestint01.eastus.kusto.windows.net", "Report", 30);
