using System.Collections.Generic;
using Newtonsoft.Json;

namespace MobileDevCodeChallenge.Models.Responses
{
    public class MovieUpcomingResponse
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public List<Movie> Results { get; set; }

        [JsonProperty("dates")]
        public Dates Dates { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }
}