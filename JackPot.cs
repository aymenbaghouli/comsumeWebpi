using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_NET.Models
{
    public class JackPot
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }
    }
}