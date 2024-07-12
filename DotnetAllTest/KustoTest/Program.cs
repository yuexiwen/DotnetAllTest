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

var clusterUri = "https://cpccradxingestint01.eastus.kusto.windows.net";
var databaseName = "Report";
var query = ".show functions | where Name contains '_ForCroTriggerCheck'";

ADXConnection adxConnection = new(clusterUri, databaseName, 30);

string clientRequestId = "test_request_id";

var data = await adxConnection.ExecuteQuery(query, false, clientRequestId, false, 2, 2000, 100000);

 while (data.Read())
 {
    for (int i = 0; i < data.FieldCount; i++)
    {
        var columnName = data.GetName(i);
        var columnValue = data.GetValue(i);
        if (columnName == "Name" && columnValue.ToString()?.Contains("_ForCroTriggerCheck") == true)
        {
            Console.WriteLine($"index = {i} | name = {data.GetName(i)} | value = {data.GetValue(i)}");
            var result = adxConnection.ExecuteQuery(columnValue.ToString(), false, clientRequestId, false, 2, 2000, 100000).GetAwaiter().GetResult();
            Console.WriteLine("---------------");
        }
    }
}
Console.WriteLine("-----------------------------------------");