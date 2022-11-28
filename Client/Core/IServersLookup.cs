using System.Collections.Generic;

namespace Client.Core
{
    public interface IServersLookup
    {
        IEnumerable<string> GetServers();
    }
}
