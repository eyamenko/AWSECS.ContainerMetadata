using System.IO;
using System.Net;

namespace AWS.ECS.Metadata
{
    public static class EcsMetadata
    {
        public static IPAddress GetLocalIPv4() => GetIPv4("http://169.254.169.254/latest/meta-data/local-ipv4");

        public static IPAddress GetPublicIPv4() => GetIPv4("http://169.254.169.254/latest/meta-data/public-ipv4");

        private static IPAddress GetIPv4(string requestUriString)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(requestUriString);

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
