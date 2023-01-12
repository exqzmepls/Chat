using System.Collections.Generic;

namespace Client.Core.ServersLookups
{
    public interface IServersLookup
    {
        IEnumerable<ServerInfo> GetServers();
    }
}
