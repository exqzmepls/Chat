namespace Client.Core.ServersLookups
{
    public class ServerInfo
    {
        public string IP { get; set; }

        public int Port { get; set; }

        public override string ToString()
        {
            return $"{IP}:{Port}";
        }

        public override bool Equals(object obj)
        {
            return obj is ServerInfo other && IP == other.IP && Port == other.Port;
        }

        public override int GetHashCode()
        {
            return IP.GetHashCode() + Port.GetHashCode();
        }
    }
}