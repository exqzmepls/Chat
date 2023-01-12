using Newtonsoft.Json;

namespace Common.Dtos
{
    public class PingResponse
    {
        [JsonProperty(Required = Required.Always)]
        public string IP { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Port { get; set; }
    }
}
