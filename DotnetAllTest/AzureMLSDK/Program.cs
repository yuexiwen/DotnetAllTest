using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.MachineLearning;
using Azure.ResourceManager.MachineLearning.Models;
using Azure.ResourceManager.Resources;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Hello, World!");

const string SUBSCRIPTION_ID = "0c530e83-2bf4-40f0-902f-f897f0e2cd7e";
const string RESOURCE_NAME = "aml-dev-dev01";
const string AML_WORKSPACE_NAME = "aml-ml-poc-dev01";
const string ENVIRONMENT_NAME = "envpoc";
const string CODE_URI = "https://amlsamlstodev01.blob.core.windows.net/mltest-container/";
const string CODE_VERSION = "2";
const string CODE_ASSET_NAME = "python_code";
const string COMPUTE_NAME = "computepoc";
const string COMMAND = "python Training2.py";

ArmClient armClient = new ArmClient(new DefaultAzureCredential());
//SubscriptionResource subscription = await armClient.GetDefaultSubscriptionAsync();
//Console.WriteLine($"subscription id = {subscription.Id}");
//Console.WriteLine($"subscription name = {subscription.Data.DisplayName}");
SubscriptionResource subscription1 = armClient.GetSubscriptionResource(SubscriptionResource.CreateResourceIdentifier(SUBSCRIPTION_ID));
ResourceGroupCollection resourceGroups = subscription1.GetResourceGroups();

ResourceGroupResource resourceGroup;
if (await resourceGroups.ExistsAsync(RESOURCE_NAME))
{
    resourceGroup = await resourceGroups.GetAsync(RESOURCE_NAME);
    Console.WriteLine($"{RESOURCE_NAME} exist!");
}
else
{
    throw new Exception("resource group doesn't exist!");
}

MachineLearningWorkspaceResource workspace;
if (await resourceGroup.GetMachineLearningWorkspaces().ExistsAsync(AML_WORKSPACE_NAME))
{
    workspace = await resourceGroup.GetMachineLearningWorkspaces().GetAsync(AML_WORKSPACE_NAME);
    Console.WriteLine($"workspace {AML_WORKSPACE_NAME} already exists.");
}
else
{
    throw new Exception("workspace doesn't exist!");
}

Console.WriteLine($"workspace id = {workspace.Id}");
var job = await workspace.GetMachineLearningJobAsync("xiwentestjob");
Console.WriteLine($"job name = {job.Value.Id}");
Console.WriteLine($"job status = {job.Value.Data.Properties.Status}");


var envid = $"{workspace.Id}/environments/{ENVIRONMENT_NAME}";
var envIdentitiferid = new ResourceIdentifier(envid);
MachineLearningEnvironmentContainerResource environmentContainerResource = armClient.GetMachineLearningEnvironmentContainerResource(envIdentitiferid);
MachineLearningEnvironmentVersionResource environmentVersionResource = environmentContainerResource.GetMachineLearningEnvironmentVersions().First();
Console.WriteLine($"env version id = {environmentVersionResource.Id}");

string resourceId = $"{workspace.Id}/codes/{CODE_ASSET_NAME}";
var codeid = new ResourceIdentifier(resourceId);
MachineLearningCodeContainerResource codeContainer = armClient.GetMachineLearningCodeContainerResource(codeid);
bool exist = await codeContainer.GetMachineLearningCodeVersions().ExistsAsync(CODE_VERSION);
if (!exist)
{
    Console.WriteLine("not exist!");
    MachineLearningCodeVersionProperties properties = new MachineLearningCodeVersionProperties { CodeUri = new Uri(CODE_URI) };
    MachineLearningCodeVersionData data = new MachineLearningCodeVersionData(properties);
    ArmOperation<MachineLearningCodeVersionResource> CodeVersionResourceOperation = await codeContainer.GetMachineLearningCodeVersions().CreateOrUpdateAsync(WaitUntil.Completed, CODE_VERSION, data);
}

var codeVersion = await codeContainer.GetMachineLearningCodeVersionAsync(CODE_VERSION);
Console.WriteLine($"codeversion id = {codeVersion.Value.Id}");
Console.WriteLine($"codeversion name = {codeVersion.Value.Data.Name}");
Console.WriteLine($"codeversion data id  = {codeVersion.Value.Data.Id}");
Console.WriteLine($"codeversion uri  = {codeVersion.Value.Data.Properties.CodeUri}");

MachineLearningComputeResource machineLearningComputeResource = workspace.GetMachineLearningCompute(COMPUTE_NAME);
Console.WriteLine($"Compute id = {machineLearningComputeResource.Id}");
Console.WriteLine($"Compute location = {machineLearningComputeResource.Data.Properties.ComputeLocation}");
Console.WriteLine($"Compute sku = {machineLearningComputeResource.Data.Sku}");

var commandjob = new MachineLearningCommandJob(COMMAND, environmentVersionResource.Id)
{
    CodeId = codeVersion.Value.Id,
    ExperimentName = "triggerByDotnetSDK",
    DisplayName = "test job",
    ComputeId = machineLearningComputeResource.Id,
    Tags = new Dictionary<string, string>
    {
        { "owner", "xiwyue" },
    },
    Description = "This is a description of test Command Job",
};

MachineLearningJobData commandJobData = new MachineLearningJobData(commandjob);
ArmOperation<MachineLearningJobResource> jobOperation = await workspace.GetMachineLearningJobs().CreateOrUpdateAsync(WaitUntil.Completed, "xiwentestjob2", commandJobData);
MachineLearningJobResource jobResource = jobOperation.Value;
Console.WriteLine($"JobCreateOrUpdateOperation {jobResource.Data.Id} created.");
