using System;
using Newtonsoft.Json;

namespace Common.Contracts
{
    public class QuitRequestMessage
    {
        [JsonProperty(Required = Required.Always)]
        public Guid SessionId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string ChatName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Login { get; set; }
    }
}