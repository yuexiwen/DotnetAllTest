namespace AzureMLSDK.ResourceGroup
{
    using Azure.ResourceManager.Resources;
    using Azure.ResourceManager;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ResourceGroupOperations
    {
        /// <summary>
        /// Creates a new resource group with the specified name
        /// If one already exists then it gets updated
        /// </summary>
        /// <param name="armClient"></param>
        /// <param name="resourceGroupName"></param>
        /// <returns></returns>
        /*
        public static async Task<ResourceGroupResource> CreateResourceGroup(ArmClient armClient, string resourceGroupName)
        {
            ResourceGroupResource resourceGroup;
            Console.WriteLine("Creating a resource group...");

            SubscriptionResource subscription = await armClient.GetDefaultSubscriptionAsync();
            ResourceGroupCollection resourceGroups = subscription.GetResourceGroups();

            return resourceGroup;
        }
        */
    }
}
