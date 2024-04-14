
namespace AzureMLSDK
{
    using Azure;
    using Azure.Core;
    using Azure.Identity;
    using Azure.ResourceManager;
    using Azure.ResourceManager.MachineLearning;
    using Azure.ResourceManager.MachineLearning.Models;
    using Azure.ResourceManager.Resources;
    using System.Threading.Tasks;

    public class AMLJobOperator
    {
        public string subscriptionId = "0c530e83-2bf4-40f0-902f-f897f0e2cd7e";
        public string resourceGroupName = "aml-dev-dev01";
        public string amlWorkspaceName = "aml-ml-poc-dev01";
        public string environmentName = "envpoc";
        public string environmentVersion = "1";
        public string codeUri = "https://amlsamlstodev01.blob.core.windows.net/mltest-container/";
        public string codeVersion = "2";
        public string codeAssetName = "python_code";
        public string computeName = "computepoc";
        public string command = "python Training2.py";

        private ArmClient? armclient = null;
        private SubscriptionResource? subscriptionResource = null;
        private ResourceGroupResource? resourceGroup = null;
        private MachineLearningWorkspaceResource? amlWorkspace = null;
        private MachineLearningComputeResource? computeResource = null;
        private MachineLearningEnvironmentVersionResource? environmentVersionResource = null;
        private MachineLearningCodeVersionResource? codeVersionResource = null;
        private ArmClient? ArmClient {
            get
            {
                if (this.armclient == null)
                {
                    return armclient = this.GetArmClient();
                }
                else
                {
                    return armclient;
                }
            }
        }

        private SubscriptionResource Subscription
        {
            get
            {
                if (this.subscriptionResource == null)
                {
                    return this.subscriptionResource = this.GetSubscriptionResource();
                }
                else
                {
                    return this.subscriptionResource;
                }
            }
        }

        private ResourceGroupResource ResourceGroup
        {
            get
            {
                if (this.resourceGroup == null)
                {
                    return this.resourceGroup = this.GetResourceGroupResource();
                }
                else
                {
                    return this.resourceGroup;
                }
            }
        }
        
        private MachineLearningWorkspaceResource AMLWorkspace
        {
            get
            {
                if (this.amlWorkspace == null)
                {
                    return amlWorkspace = this.GetAMLWorkspace();
                }
                else
                {
                    return amlWorkspace;
                }
            }
        }

        private MachineLearningComputeResource ComputeResource
        {
            get
            {
                if (this.computeResource is null)
                {
                    return this.computeResource = this.GetComputeResource();
                }
                else
                {
                    return this.computeResource;
                }
            }
        }

        private MachineLearningEnvironmentVersionResource EnvironmentVersionResource
        {
            get
            {
                if (this.environmentVersionResource is null)
                {
                    return this.environmentVersionResource = this.GetEnvironemntVersionResource();
                }
                else
                {
                    return this.environmentVersionResource;
                }
            }
        }

        private MachineLearningCodeVersionResource CodeVersionResource
        {
            get
            {
                if (this.codeVersionResource is null)
                {
                    return this.codeVersionResource = this.GetCodeVersionResourceAsync();
                }
                else
                {
                    return this.codeVersionResource;
                }
            }
        }

        public AMLJobOperator()
        {
        }

        public MachineLearningJobStatus? CheckAMLJobStatus(string amlJobName)
        {
            if (this.AMLWorkspace is null)
            {
                throw new Exception("cannot check aml job status when aml workspace is null");
            }

            var amlJob = this.AMLWorkspace.GetMachineLearningJob(amlJobName)
                ?? throw new Exception($"Cannot find aml job through name: {amlJobName}");

            return amlJob.Value.Data.Properties.Status;
        }

        public async Task TriggerAMLJobByNameAsync(string amlJobName)
        {
            if (this.command is null)
            {
                throw new Exception("command is null");
            }

            if (this.EnvironmentVersionResource is null)
            {
                throw new Exception("environment version resource is null when triggering job");
            }

            var commandjob = new MachineLearningCommandJob(this.command, this.EnvironmentVersionResource.Id)
            {
                CodeId = this.CodeVersionResource.Id,
                ExperimentName = "triggerByDotnetSDK",
                DisplayName = "test job",
                ComputeId = this.ComputeResource.Id,
                Tags = new Dictionary<string, string>
                {
                    { "owner", "xiwyue" },
                },
                Description = "This is a description of test Command Job",
            };

            MachineLearningJobData commandJobData = new MachineLearningJobData(commandjob);
            ArmOperation<MachineLearningJobResource> jobOperation = await this.AMLWorkspace.GetMachineLearningJobs().CreateOrUpdateAsync(WaitUntil.Completed, amlJobName, commandJobData);
            MachineLearningJobResource jobResource = jobOperation.Value; 
            Console.WriteLine($"JobCreateOrUpdateOperation {jobResource.Data.Id} created.");
        }

        private ArmClient GetArmClient()
        {
            return new ArmClient(new DefaultAzureCredential());
        }

        private SubscriptionResource GetSubscriptionResource()
        {
            if (this.subscriptionId == null)
            {
                throw new Exception("subscription id is null");
            }

            if (this.ArmClient == null)
            {
                throw new Exception("Arm Client is null");
            }

            return this.ArmClient.GetSubscriptionResource(SubscriptionResource.CreateResourceIdentifier(this.subscriptionId));
        }

        private ResourceGroupResource GetResourceGroupResource()
        {
            if (this.Subscription == null)
            {
                throw new Exception("Subscription is null");
            }

            if (this.resourceGroupName == null)
            {
                throw new Exception("ResourceGroupName is null");
            }

            ResourceGroupResource resourceGroup = this.Subscription.GetResourceGroup(this.resourceGroupName) 
                ?? throw new Exception($"can not find resource group through resource name :{this.resourceGroupName}");

            return resourceGroup;
        }

        private MachineLearningWorkspaceResource GetAMLWorkspace()
        {
            if (amlWorkspaceName is null)
            {
                throw new Exception("aml workspace name is null");
            }

            if (this.ResourceGroup is null)
            {
                throw new Exception("Resource Group is null");
            }

            MachineLearningWorkspaceResource? amlworkspace = resourceGroup.GetMachineLearningWorkspace(this.amlWorkspaceName)
                ?? throw new Exception($"Can not find aml workspac through {this.amlWorkspaceName}");

            return amlworkspace;
        }

        private MachineLearningComputeResource GetComputeResource()
        {
            if (this.AMLWorkspace is null)
            {
                throw new Exception("aml workspace is null");
            }

            if (this.computeName is null)
            {
                throw new Exception("compute name is null");
            }

            MachineLearningComputeResource machineLearningComputeResource = this.AMLWorkspace.GetMachineLearningCompute(this.computeName)
                ?? throw new Exception($"can not find compute resource through compute name : {this.computeName}");

            return machineLearningComputeResource;
        }

        private MachineLearningEnvironmentVersionResource GetEnvironemntVersionResource()
        {
            if (this.environmentName is null)
            {
                throw new Exception("environment name is null");
            }

            if (this.environmentVersion is null)
            {
                throw new Exception("environemnt version is null");
            }

            if (this.AMLWorkspace is null)
            {
                throw new Exception("AMLWorkspace is null when get environemnt");
            }

            if (this.ArmClient is null)
            {
                throw new Exception("ArmClient is null when get environment");
            }

            var envid = $"{this.AMLWorkspace.Id}/environments/{this.environmentName}";
            var envIdentitiferid = new ResourceIdentifier(envid);
            MachineLearningEnvironmentContainerResource environmentContainerResource = this.ArmClient.GetMachineLearningEnvironmentContainerResource(envIdentitiferid);
            if (environmentContainerResource is null)
            {
                throw new Exception($"can not get environemnt through env name {this.environmentName}");
            }

            MachineLearningEnvironmentVersionResource environmentVersionResource = environmentContainerResource.GetMachineLearningEnvironmentVersion(this.environmentVersion)
                ?? throw new Exception($"can not get environemtn version resource through version: {this.environmentVersion}");

            return environmentVersionResource;
        }

        private MachineLearningCodeVersionResource GetCodeVersionResourceAsync()
        {
            if (this.ArmClient is null)
            {
                throw new Exception("Arm client is null when get code version resource");
            }

            if (this.AMLWorkspace is null)
            {
                throw new Exception("aml workspace is null when get code version resource");
            }

            if (this.codeAssetName is null)
            {
                throw new Exception("code asset name is null");
            }

            if (this.codeUri is null)
            {
                throw new Exception("code uri is null");
            }

            string resourceId = $"{this.AMLWorkspace.Id}/codes/{this.codeAssetName}";
            var codeid = new ResourceIdentifier(resourceId);
            MachineLearningCodeContainerResource codeContainer = this.ArmClient.GetMachineLearningCodeContainerResource(codeid)
                ?? throw new Exception($"cannot get code container through code asset name {this.codeAssetName}");

            if (!codeContainer.GetMachineLearningCodeVersions().Exists(this.codeVersion))
            {
                Console.WriteLine("not exist!");
                MachineLearningCodeVersionProperties properties = new MachineLearningCodeVersionProperties { CodeUri = new Uri(this.codeUri) };
                MachineLearningCodeVersionData data = new MachineLearningCodeVersionData(properties);
                ArmOperation<MachineLearningCodeVersionResource> CodeVersionResourceOperation = codeContainer.GetMachineLearningCodeVersions().CreateOrUpdate(WaitUntil.Completed, this.codeVersion, data);
            }
            var codeVersion = codeContainer.GetMachineLearningCodeVersion(this.codeVersion);
            return codeVersion;
        }
    }
}
