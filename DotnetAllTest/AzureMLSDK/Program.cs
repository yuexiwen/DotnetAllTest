using AzureMLSDK;

Console.WriteLine("Hello, World!");

const string SUBSCRIPTION_ID = "0c530e83-2bf4-40f0-902f-f897f0e2cd7e";
const string RESOURCE_NAME = "aml1-dev-dev01";
const string AML_WORKSPACE_NAME = "aml1-ml-poc-dev01";
const string ENVIRONMENT_NAME = "envpoc";
const string ENVIRONMENT_VERSION = "1";
const string CODE_URI = "https://aml1samlstodev01.blob.core.windows.net/training-code/";
const string CODE_VERSION = "1";
const string CODE_ASSET_NAME = "python_code";
const string COMPUTE_NAME = "computepoc";
const string COMMAND = "python Training2.py";
const string JOB_NAME = "xiwentestjob-2024-0415-1603";

AMLJobOperator aMLJobOperator = new();
aMLJobOperator.subscriptionId = SUBSCRIPTION_ID;
aMLJobOperator.resourceGroupName = RESOURCE_NAME;
aMLJobOperator.amlWorkspaceName = AML_WORKSPACE_NAME;
aMLJobOperator.environmentName = ENVIRONMENT_NAME;
aMLJobOperator.environmentVersion = ENVIRONMENT_VERSION;
aMLJobOperator.codeUri = CODE_URI;
aMLJobOperator.codeVersion = CODE_VERSION;
aMLJobOperator.codeAssetName = CODE_ASSET_NAME;
aMLJobOperator.computeName = COMPUTE_NAME;
aMLJobOperator.command = COMMAND;

//await aMLJobOperator.TriggerAMLJobByNameAsync(JOB_NAME);

var jobStatus = aMLJobOperator.CheckAMLJobStatus(JOB_NAME);
Console.WriteLine($"job xiwentestjob status = {jobStatus}");

