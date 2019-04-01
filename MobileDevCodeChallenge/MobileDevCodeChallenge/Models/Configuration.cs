using System.Collections.Generic;
using Newtonsoft.Json;

namespace MobileDevCodeChallenge.Models
{
    public class Configuration
    {
        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("change_keys")]
        public List<string> ChangeKeys { get; set; }
    }
}
