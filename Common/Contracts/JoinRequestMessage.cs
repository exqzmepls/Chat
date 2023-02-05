using System;
using Newtonsoft.Json;

namespace Common.Contracts
{
    public class JoinRequestMessage
    {
        [JsonProperty(Required = Required.Always)]
        public Guid SessionId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string ClientHostName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string ChatName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Login { get; set; }
    }
}