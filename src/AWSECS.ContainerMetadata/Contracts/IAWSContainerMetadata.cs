using System.Net;
using AWSECS.ContainerMetadata.Models;

namespace AWSECS.ContainerMetadata.Contracts
{
    /// <summary>
    /// AWS ECS container metadata service.
    /// </summary>
    public interface IAWSContainerMetadata
    {
        /// <summary>
        /// Gets AWS ECS container metadata.
        /// </summary>
        /// <returns>AWS ECS container metadata.</returns>
        ContainerResponse GetContainerMetadata();

        /// <summary>
        /// Gets the private IP address for the AWS ECS container's host.
        /// </summary>
        /// <returns>Private IP address.</returns>
        IPAddress GetHostPrivateIPv4Address();

        /// <summary>
        /// Get the public IP address for the AWS ECS container's host.
        /// </summary>
        /// <returns>Public IP address.</returns>
        IPAddress GetHostPublicIPv4Address();
    }
}
