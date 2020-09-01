using System;
using System.IO;
using System.Net;

namespace AWS.ECS.Metadata
{
    public static class EcsMetadata
    {
        public static IPAddress GetLocalIPv4()
        {
            try
            {
                var requestUri = new Uri("http://169.254.169.254/latest/meta-data/local-ipv4");

                var request = (HttpWebRequest)WebRequest.Create(requestUri);

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
