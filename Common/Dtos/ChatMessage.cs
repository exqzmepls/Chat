﻿using System;
using Newtonsoft.Json;

namespace Common.Dtos
{
    public class ChatMessage
    {
        [JsonProperty(Required = Required.Always)]
        public Guid SessionId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Chat { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Text { get; set; }
    }
}
