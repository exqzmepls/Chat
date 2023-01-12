using System;
using Newtonsoft.Json;

namespace Common.Dtos
{
    public class QuitRequest
    {
        [JsonProperty(Required = Required.Always)]
        public Guid SessionId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string ChatName { get; set; }
    }
}
