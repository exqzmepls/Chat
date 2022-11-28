using System.Net;

namespace Client.Core
{
    internal class HostNameProvider : IHostNameProvider
    {
        public string GetHostName(string ip)
        {
            try
            {
                var host = Dns.GetHostByAddress(ip);
                return host.HostName;
            }
            catch
            {
                return default;
            }
        }
    }
}
