namespace AWSECS.ContainerMetadata.Contracts
{
    public interface IAWSContainerMetadataClient
    {
        string GetContainerMetadata();
        string GetHostPrivateIPv4Address();
        string GetHostPublicIPv4Address();
    }
}
