using System;
using System.Linq;
using System.Net;
using Xunit;

namespace AWS.ECS.Metadata.UnitTests
{
    public class EcsMetadataTests
    {
        public EcsMetadataTests()
        {
            Environment.SetEnvironmentVariable("ECS_CONTAINER_METADATA_FILE", "ecs-container-metadata.json");
        }

        [Fact]
        public void GetHostPort_should_find_host_port_using_container_port()
        {
            var hostPort = EcsMetadata.GetHostPort(8080);

            Assert.Equal(80, hostPort);
        }

        [Fact]
        public void GetHostPort_should_not_find_host_port_if_container_port_cannot_be_found()
        {
            var hostPort = EcsMetadata.GetHostPort(80);

            Assert.Null(hostPort);
        }

        [Fact]
        public void GetContainerPort_should_find_container_port_using_host_port()
        {
            var containerPort = EcsMetadata.GetContainerPort(80);

            Assert.Equal(8080, containerPort);
        }

        [Fact]
        public void GetContainerPort_should_not_find_container_port_if_host_port_cannot_be_found()
        {
            var containerPort = EcsMetadata.GetContainerPort(8080);

            Assert.Null(containerPort);
        }

        [Fact]
        public void GetHostPrivateIPv4Address_should_find_private_ip_v4_address()
        {
            var ipAddress = EcsMetadata.GetHostPrivateIPv4Address();

            Assert.Equal(IPAddress.Parse("192.0.2.0"), ipAddress);
        }

        [Fact]
        public void GetHostPublicIPv4Address_should_find_public_ip_v4_address()
        {
            var ipAddress = EcsMetadata.GetHostPublicIPv4Address();

            Assert.Equal(IPAddress.Parse("203.0.113.0"), ipAddress);
        }

        [Fact]
        public void GetContainerMetadata_should_find_container_metadata()
        {
            var containerMetadata = EcsMetadata.GetContainerMetadata();

            Assert.Equal("default", containerMetadata.Cluster);
            Assert.Equal("arn:aws:ecs:us-west-2:012345678910:container-instance/default/1f73d099-b914-411c-a9ff-81633b7741dd", containerMetadata.ContainerInstanceARN);
            Assert.Equal("arn:aws:ecs:us-west-2:012345678910:task/default/2b88376d-aba3-4950-9ddf-bcb0f388a40c", containerMetadata.TaskARN);
            Assert.Equal("console-sample-app-static", containerMetadata.TaskDefinitionFamily);
            Assert.Equal("1", containerMetadata.TaskDefinitionRevision);
            Assert.Equal("aec2557997f4eed9b280c2efd7afccdcedfda4ac399f7480cae870cfc7e163fd", containerMetadata.ContainerID);
            Assert.Equal("simple-app", containerMetadata.ContainerName);
            Assert.Equal("/ecs-console-sample-app-static-1-simple-app-e4e8e495e8baa5de1a00", containerMetadata.DockerContainerName);
            Assert.Equal("sha256:2ae34abc2ed0a22e280d17e13f9c01aaf725688b09b7a1525d1a2750e2c0d1de", containerMetadata.ImageID);
            Assert.Equal("httpd:2.4", containerMetadata.ImageName);
            Assert.Single(containerMetadata.PortMappings);
            Assert.Equal(8080, containerMetadata.PortMappings.First().ContainerPort);
            Assert.Equal(80, containerMetadata.PortMappings.First().HostPort);
            Assert.Equal("0.0.0.0", containerMetadata.PortMappings.First().BindIp);
            Assert.Equal("tcp", containerMetadata.PortMappings.First().Protocol);
            Assert.Single(containerMetadata.Networks);
            Assert.Equal("bridge", containerMetadata.Networks.First().NetworkMode);
            Assert.Single(containerMetadata.Networks.First().IPv4Addresses);
            Assert.Equal("192.0.2.0", containerMetadata.Networks.First().IPv4Addresses.First());
            Assert.Equal("READY", containerMetadata.MetadataFileStatus);
            Assert.Equal("us-east-1b", containerMetadata.AvailabilityZone);
            Assert.Equal("192.0.2.0", containerMetadata.HostPrivateIPv4Address);
            Assert.Equal("203.0.113.0", containerMetadata.HostPublicIPv4Address);
        }
    }
}
