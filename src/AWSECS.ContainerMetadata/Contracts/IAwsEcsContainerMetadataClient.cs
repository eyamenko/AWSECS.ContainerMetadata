namespace AWSECS.ContainerMetadata.Contracts
{
    public interface IAwsEcsContainerMetadataClient
    {
        string GetContainerMetadata();
        string GetHostPrivateIPv4Address();
        string GetHostPublicIPv4Address();
    }
}
