namespace AWS.ECS.Metadata.Models
{
    public class PortMapping
    {
        /// <summary>
        /// The port on the container that is exposed.
        /// </summary>
        public int ContainerPort { get; set; }

        /// <summary>
        /// The port on the host container instance that is exposed.
        /// </summary>
        public int HostPort { get; set; }

        /// <summary>
        /// The bind IP address that is assigned to the container by Docker.
        /// This IP address is only applied with the bridge network mode, and it is only accessible from the container instance.
        /// </summary>
        public string BindIp { get; set; }

        /// <summary>
        /// The network protocol used for the port mapping.
        /// </summary>
        public string Protocol { get; set; }
    }
}
