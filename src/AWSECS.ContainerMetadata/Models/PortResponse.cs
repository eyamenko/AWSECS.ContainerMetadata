namespace AWSECS.ContainerMetadata.Models
{
    public class PortResponse
    {
        public ushort? ContainerPort { get; set; }
        public string Protocol { get; set; }
        public ushort? HostPort { get; set; }
    }
}
