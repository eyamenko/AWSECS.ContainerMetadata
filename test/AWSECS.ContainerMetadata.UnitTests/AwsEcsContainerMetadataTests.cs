using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using AWSECS.ContainerMetadata.Contracts;
using AWSECS.ContainerMetadata.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace AWSECS.ContainerMetadata.UnitTests
{
    public class AwsEcsContainerMetadataTests
    {
        private readonly Mock<IAwsEcsContainerMetadataClient> _awsEcsContainerMetadataClient;
        private readonly AwsEcsContainerMetadata _awsEcsContainerMetadata;

        public AwsEcsContainerMetadataTests()
        {
            _awsEcsContainerMetadataClient = new Mock<IAwsEcsContainerMetadataClient>();
            _awsEcsContainerMetadata = new AwsEcsContainerMetadata(_awsEcsContainerMetadataClient.Object, NullLogger<AwsEcsContainerMetadata>.Instance);
        }

        [Fact]
        public void GetContainerMetadata_should_get_container_metadata()
        {
            _awsEcsContainerMetadataClient.Setup(c => c.GetContainerMetadata()).Returns(File.ReadAllText("container-metadata.json"));

            var containerMetadata = _awsEcsContainerMetadata.GetContainerMetadata();

            Assert.NotNull(containerMetadata);
            Assert.Equal("c7a6b9b237934e9999f319ea3ccc9da4query-metadata", containerMetadata.DockerId);
            Assert.Equal("query-metadata", containerMetadata.Name);
            Assert.Equal("query-metadata", containerMetadata.DockerName);
            Assert.Equal("mreferre/eksutils", containerMetadata.Image);
            Assert.Equal("sha256:1b146e73f801617610dcb00441c6423e7c85a7583dd4a65ed1be03cb0e123311", containerMetadata.ImageID);
            Assert.NotNull(containerMetadata.Labels);
            Assert.Equal(5, containerMetadata.Labels.Count);
            Assert.Equal("arn:aws:ecs:us-west-2:&ExampleAWSAccountNo1;:cluster/default", containerMetadata.Labels["com.amazonaws.ecs.cluster"]);
            Assert.Equal("query-metadata", containerMetadata.Labels["com.amazonaws.ecs.container-name"]);
            Assert.Equal("arn:aws:ecs:us-west-2:&ExampleAWSAccountNo1;:task/default/c7a6b9b237934e9999f319ea3ccc9da4", containerMetadata.Labels["com.amazonaws.ecs.task-arn"]);
            Assert.Equal("query-metadata", containerMetadata.Labels["com.amazonaws.ecs.task-definition-family"]);
            Assert.Equal("3", containerMetadata.Labels["com.amazonaws.ecs.task-definition-version"]);
            Assert.Equal("RUNNING", containerMetadata.DesiredStatus);
            Assert.Equal("RUNNING", containerMetadata.KnownStatus);
            Assert.NotNull(containerMetadata.Limits);
            Assert.Equal(2, containerMetadata.Limits.CPU);
            Assert.Equal(DateTime.Parse("2020-03-26T22:11:23.62831313Z", styles: DateTimeStyles.AdjustToUniversal), containerMetadata.CreatedAt);
            Assert.Equal(DateTime.Parse("2020-03-26T22:11:23.62831313Z", styles: DateTimeStyles.AdjustToUniversal), containerMetadata.StartedAt);
            Assert.Equal("NORMAL", containerMetadata.Type);
            Assert.Single(containerMetadata.Networks);
            Assert.Equal("awsvpc", containerMetadata.Networks.First().NetworkMode);
            Assert.Single(containerMetadata.Networks.First().IPv4Addresses);
            Assert.Equal("10.0.0.61", containerMetadata.Networks.First().IPv4Addresses.First());
        }

        [Fact]
        public void GetContainerMetadata_should_not_throw_if_response_cannot_be_parsed()
        {
            _awsEcsContainerMetadataClient.Setup(c => c.GetContainerMetadata()).Returns(default(string));

            var containerMetadata = _awsEcsContainerMetadata.GetContainerMetadata();

            Assert.Null(containerMetadata);
        }

        [Fact]
        public void GetContainerMetadata_should_not_throw_if_client_throws()
        {
            _awsEcsContainerMetadataClient.Setup(c => c.GetContainerMetadata()).Throws<Exception>();

            var containerMetadata = _awsEcsContainerMetadata.GetContainerMetadata();

            Assert.Null(containerMetadata);
        }

        [Fact]
        public void GetHostPrivateIPv4Address_should_get_host_private_ip_v4_address()
        {
            _awsEcsContainerMetadataClient.Setup(c => c.GetHostPrivateIPv4Address()).Returns("10.0.0.61");

            var ipAddress = _awsEcsContainerMetadata.GetHostPrivateIPv4Address();

            Assert.NotNull(ipAddress);
            Assert.Equal(IPAddress.Parse("10.0.0.61"), ipAddress);
        }

        [Fact]
        public void GetHostPrivateIPv4Address_should_not_throw_if_response_cannot_be_parsed()
        {
            _awsEcsContainerMetadataClient.Setup(c => c.GetHostPrivateIPv4Address()).Returns(default(string));

            var ipAddress = _awsEcsContainerMetadata.GetHostPrivateIPv4Address();

            Assert.Null(ipAddress);
        }

        [Fact]
        public void GetHostPrivateIPv4Address_should_not_throw_if_client_throws()
        {
            _awsEcsContainerMetadataClient.Setup(c => c.GetHostPrivateIPv4Address()).Throws<Exception>();

            var ipAddress = _awsEcsContainerMetadata.GetHostPrivateIPv4Address();

            Assert.Null(ipAddress);
        }

        [Fact]
        public void GetHostPublicIPv4Address_should_get_host_public_ip_v4_address()
        {
            _awsEcsContainerMetadataClient.Setup(c => c.GetHostPublicIPv4Address()).Returns("10.0.0.61");

            var ipAddress = _awsEcsContainerMetadata.GetHostPublicIPv4Address();

            Assert.NotNull(ipAddress);
            Assert.Equal(IPAddress.Parse("10.0.0.61"), ipAddress);
        }

        [Fact]
        public void GetHostPublicIPv4Address_should_not_throw_if_response_cannot_be_parsed()
        {
            _awsEcsContainerMetadataClient.Setup(c => c.GetHostPublicIPv4Address()).Returns(default(string));

            var ipAddress = _awsEcsContainerMetadata.GetHostPublicIPv4Address();

            Assert.Null(ipAddress);
        }

        [Fact]
        public void GetHostPublicIPv4Address_should_not_throw_if_client_throws()
        {
            _awsEcsContainerMetadataClient.Setup(c => c.GetHostPublicIPv4Address()).Throws<Exception>();

            var ipAddress = _awsEcsContainerMetadata.GetHostPublicIPv4Address();

            Assert.Null(ipAddress);
        }
    }
}
