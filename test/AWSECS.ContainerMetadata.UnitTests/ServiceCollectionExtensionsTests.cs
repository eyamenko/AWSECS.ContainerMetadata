using AWSECS.ContainerMetadata.Contracts;
using AWSECS.ContainerMetadata.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AWSECS.ContainerMetadata.UnitTests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddAwsEcsContainerMetadataServices_should_add_container_metadata_services()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddAwsEcsContainerMetadataServices();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var awsEcsContainerMetadata = serviceProvider.GetService<IAwsEcsContainerMetadata>();

            Assert.NotNull(awsEcsContainerMetadata);
        }
    }
}
