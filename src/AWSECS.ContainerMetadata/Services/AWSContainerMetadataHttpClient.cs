using System;
using System.IO;
using System.Net;
using AWSECS.ContainerMetadata.Contracts;
using Microsoft.Extensions.Logging;

namespace AWSECS.ContainerMetadata.Services
{
    public class AWSContainerMetadataHttpClient : IAWSContainerMetadataClient
    {
        private readonly ILogger _logger;

        public AWSContainerMetadataHttpClient(ILogger<AWSContainerMetadataHttpClient> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string GetContainerMetadata()
        {
            if (Uri.TryCreate(Environment.GetEnvironmentVariable("ECS_CONTAINER_METADATA_URI_V4"), UriKind.Absolute, out var containerMetadataUri))
            {
                return GetResponseString(containerMetadataUri);
            }

            return null;
        }

        public string GetHostPrivateIPv4Address() => GetResponseString(new Uri("http://169.254.169.254/latest/meta-data/local-ipv4"));

        public string GetHostPublicIPv4Address() => GetResponseString(new Uri("http://169.254.169.254/latest/meta-data/public-ipv4"));

        private string GetResponseString(Uri requestUri)
        {
            try
            {
                var request = WebRequest.Create(requestUri);

                using var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    _logger.LogError("Failed to execute HTTP request. Request URI: {RequestUri}, Status code: {StatusCode}.", requestUri, response.StatusCode);

                    return null;
                }

                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.UnknownError)
            {
                // Network is unreachable
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get AWS metadata response.");
            }

            return null;
        }
    }
}
