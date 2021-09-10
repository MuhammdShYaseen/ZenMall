using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMall.Models.SqlServerModels
{
   public class ProdactsM
    {
        [JsonProperty("prodactId")]
        public long ProdactID { get; set; }

        [JsonProperty("pname")]
        public string PName { get; set; }

        [JsonProperty("pmarks")]
        public string PMarks { get; set; }

        [JsonProperty("psaction")]
        public string PSaction { get; set; }

        [JsonProperty("pprice")]
        public long PPrice { get; set; }

        [JsonProperty("pdiscount")]
        public long PDiscount { get; set; }

        [JsonProperty("pdiscraption")]
        public string PDiscraption { get; set; }

        [JsonProperty("pimage")]
        public string PImage { get; set; }
    }
}
