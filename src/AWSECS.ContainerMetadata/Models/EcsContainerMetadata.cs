using System.Collections.Generic;

namespace AWSECS.ContainerMetadata.Models
{
    /// <summary>
    /// ECS container metadata.
    /// </summary>
    public class EcsContainerMetadata
    {
        /// <summary>
        /// The name of the cluster that the container's task is running on.
        /// </summary>
        public string Cluster { get; set; }

        /// <summary>
        /// The full Amazon Resource Name (ARN) of the host container instance.
        /// </summary>
        public string ContainerInstanceARN { get; set; }

        /// <summary>
        /// The full Amazon Resource Name (ARN) of the task that the container belongs to.
        /// </summary>
        public string TaskARN { get; set; }

        /// <summary>
        /// The name of the task definition family the container is using.
        /// </summary>
        public string TaskDefinitionFamily { get; set; }

        /// <summary>
        /// The task definition revision the container is using.
        /// </summary>
        public string TaskDefinitionRevision { get; set; }

        /// <summary>
        /// The Docker container ID (and not the Amazon ECS container ID) for the container.
        /// </summary>
        public string ContainerID { get; set; }

        /// <summary>
        /// The container name from the Amazon ECS task definition for the container.
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// The container name that the Docker daemon uses for the container (for example, the name that shows up in docker ps command output).
        /// </summary>
        public string DockerContainerName { get; set; }

        /// <summary>
        /// The SHA digest for the Docker image used to start the container.
        /// </summary>
        public string ImageID { get; set; }

        /// <summary>
        /// The image name and tag for the Docker image used to start the container.
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Any port mappings associated with the container.
        /// </summary>
        public List<PortMapping> PortMappings { get; set; }

        /// <summary>
        /// The network mode and IP address for the container.
        /// </summary>
        public List<Network> Networks { get; set; }

        /// <summary>
        /// The status of the metadata file. When the status is READY, the metadata file is current and complete.
        /// If the file is not ready yet (for example, the moment the task is started), a truncated version of the file format is available.
        /// To avoid a likely race condition where the container has started, but the metadata has not yet been written,
        /// you can parse the metadata file and wait for this parameter to be set to READY before depending on the metadata.
        /// This is usually available in less than 1 second from when the container starts.
        /// </summary>
        public string MetadataFileStatus { get; set; }

        /// <summary>
        /// The Availability Zone the host container instance resides in.
        /// </summary>
        public string AvailabilityZone { get; set; }

        /// <summary>
        /// The private IP address for the task the container belongs to.
        /// </summary>
        public string HostPrivateIPv4Address { get; set; }

        /// <summary>
        /// The public IP address for the task the container belongs to.
        /// </summary>
        public string HostPublicIPv4Address { get; set; }
    }
}
