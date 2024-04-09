using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.MachineLearning;
using Azure.ResourceManager.Resources;

Console.WriteLine("Hello, World!");

const string SUBSCRIPTION_ID = "0c530e83-2bf4-40f0-902f-f897f0e2cd7e";
const string RESOURCE_NAME = "aml-dev-dev01";
const string AML_WORKSPACE_NAME = "aml-ml-poc-dev01";

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
var job = await workspace.GetMachineLearningJobAsync("cmdpoc2-2024-0407-1531");
Console.WriteLine($"job name = {job.Value.Id}");
//MachineLearningWorkspaceResource workspace = await resourceGroup.GetMachineLearningWorkspaces().GetAsync("aml-ml-poc-dev01");
//var resourceGroup = await ResourceGroupOperations.CreateResourceGroup(armClient, Constants.ResourceGroupName);