using AWSECS.ContainerMetadata.Contracts;
using AWSECS.ContainerMetadata.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AWSECS.ContainerMetadata.UnitTests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddAWSContainerMetadataService_should_add_container_metadata_service()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddAWSContainerMetadataService();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var awsEcsContainerMetadata = serviceProvider.GetService<IAWSContainerMetadata>();

            Assert.NotNull(awsEcsContainerMetadata);
        }
    }
}
