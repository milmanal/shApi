﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShimebaMvcAPI.Models.IndoorIO
{

    public class GeoJson
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public Feature[] Features { get; set; }
    }

}
