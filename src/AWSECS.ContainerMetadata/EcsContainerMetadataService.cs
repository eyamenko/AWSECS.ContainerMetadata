using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using AWSECS.ContainerMetadata.Models;

namespace AWSECS.ContainerMetadata
{
    /// <summary>
    /// ECS container metadata service.
    /// </summary>
    public static class EcsContainerMetadataService
    {
        /// <summary>
        /// Get the host port using the container port.
        /// </summary>
        /// <param name="containerPort">Container port.</param>
        /// <returns></returns>
        public static int? GetHostPort(int containerPort)
        {
            try
            {
                var containerMetadata = GetMetadata();

                return containerMetadata?.PortMappings?.FirstOrDefault(m => m.ContainerPort == containerPort)?.HostPort;
            }
            catch
            {
                // Ignore
            }

            return null;
        }

        /// <summary>
        /// Get the container port using the host port.
        /// </summary>
        /// <param name="hostPort">Host port.</param>
        /// <returns></returns>
        public static int? GetContainerPort(int hostPort)
        {
            try
            {
                var containerMetadata = GetMetadata();

                return containerMetadata?.PortMappings?.FirstOrDefault(m => m.HostPort == hostPort)?.ContainerPort;
            }
            catch
            {
                // Ignore
            }

            return null;
        }

        /// <summary>
        /// Get the private IP address for the host.
        /// </summary>
        /// <returns>Private IP address.</returns>
        public static IPAddress GetHostPrivateIPv4Address()
        {
            try
            {
                var containerMetadata = GetMetadata();

                if (!string.IsNullOrEmpty(containerMetadata?.HostPrivateIPv4Address))
                {
                    return IPAddress.Parse(containerMetadata.HostPrivateIPv4Address);
                }
            }
            catch
            {
                // Ignore
            }

            return GetIPv4AddressFromEndpoint("http://169.254.169.254/latest/meta-data/local-ipv4");
        }

        /// <summary>
        /// Get the public IP address for the host.
        /// </summary>
        /// <returns>Public IP address.</returns>
        public static IPAddress GetHostPublicIPv4Address()
        {
            try
            {
                var containerMetadata = GetMetadata();

                if (!string.IsNullOrEmpty(containerMetadata?.HostPublicIPv4Address))
                {
                    return IPAddress.Parse(containerMetadata.HostPublicIPv4Address);
                }
            }
            catch
            {
                // Ignore
            }

            return GetIPv4AddressFromEndpoint("http://169.254.169.254/latest/meta-data/public-ipv4");
        }

        /// <summary>
        /// Get ECS container metadata.
        /// </summary>
        /// <returns>ECS container metadata.</returns>
        public static EcsContainerMetadata GetMetadata()
        {
            try
            {
                var containerMetadataFile = Environment.GetEnvironmentVariable("ECS_CONTAINER_METADATA_FILE");

                if (!string.IsNullOrEmpty(containerMetadataFile) && File.Exists(containerMetadataFile))
                {
                    return JsonSerializer.Deserialize<EcsContainerMetadata>(File.ReadAllText(containerMetadataFile));
                }
            }
            catch
            {
                // Ignore
            }

            return null;
        }

        private static IPAddress GetIPv4AddressFromEndpoint(string requestUriString)
        {
            try
            {
                var request = WebRequest.Create(requestUriString);

                using var response = request.GetResponse();
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);

                return IPAddress.Parse(reader.ReadToEnd());
            }
            catch
            {
                // Ignore
            }

            return null;
        }
    }
}
