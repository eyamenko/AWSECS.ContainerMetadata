using System.Collections.Generic;

namespace AWSECS.ContainerMetadata.Models
{
    public class NetworkResponse
    {
        public string NetworkMode { get; set; }
        public List<string> IPv4Addresses { get; set; }
        public List<string> IPv6Addresses { get; set; }
    }
}
