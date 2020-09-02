using System.Collections.Generic;

namespace AWSECS.ContainerMetadata.Models
{
    public class Network
    {
        /// <summary>
        /// The network mode for the task to which the container belongs.
        /// </summary>
        public string NetworkMode { get; set; }

        /// <summary>
        /// The IP addresses associated with the container.
        /// </summary>
        public List<string> IPv4Addresses { get; set; }
    }
}
