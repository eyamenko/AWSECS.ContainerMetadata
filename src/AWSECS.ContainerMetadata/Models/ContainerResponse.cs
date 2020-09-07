using System;
using System.Collections.Generic;

namespace AWSECS.ContainerMetadata.Models
{
    /// <summary>
    /// AWS ECS container metadata response.
    /// </summary>
    public class ContainerResponse
    {
        /// <summary>
        /// The Docker ID for the container.
        /// </summary>
        public string DockerId { get; set; }

        /// <summary>
        /// The name of the container as specified in the task definition.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of the container supplied to Docker.
        /// The Amazon ECS container agent generates a unique name for the container to avoid name collisions when multiple copies of the same task definition are run on a single instance.
        /// </summary>
        public string DockerName { get; set; }

        /// <summary>
        /// The image for the container.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The SHA-256 digest for the image.
        /// </summary>
        public string ImageID { get; set; }

        /// <summary>
        /// Any ports exposed for the container. This parameter is omitted if there are no exposed ports.
        /// </summary>
        public List<PortResponse> Ports { get; set; }

        /// <summary>
        /// Any labels applied to the container. This parameter is omitted if there are no labels applied.
        /// </summary>
        public Dictionary<string, string> Labels { get; set; }

        /// <summary>
        /// The desired status for the container from Amazon ECS.
        /// </summary>
        public string DesiredStatus { get; set; }

        /// <summary>
        /// The known status for the container from Amazon ECS.
        /// </summary>
        public string KnownStatus { get; set; }

        /// <summary>
        /// The exit code for the container. This parameter is omitted if the container has not exited.
        /// </summary>
        public int? ExitCode { get; set; }

        /// <summary>
        /// The resource limits specified at the container level (such as CPU and memory). This parameter is omitted if no resource limits are defined.
        /// </summary>
        public LimitsResponse Limits { get; set; }

        /// <summary>
        /// The time stamp for when the container was created. This parameter is omitted if the container has not been created yet.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The time stamp for when the container started. This parameter is omitted if the container has not started yet.
        /// </summary>
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// The time stamp for when the container stopped. This parameter is omitted if the container has not stopped yet.
        /// </summary>
        public DateTime? FinishedAt { get; set; }

        /// <summary>
        /// The type of the container. Containers that are specified in your task definition are of type NORMAL.
        /// You can ignore other container types, which are used for internal task resource provisioning by the Amazon ECS container agent.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The network information for the container, such as the network mode and IP address. This parameter is omitted if no network information is defined.
        /// </summary>
        public List<NetworkResponse> Networks { get; set; }
    }
}
