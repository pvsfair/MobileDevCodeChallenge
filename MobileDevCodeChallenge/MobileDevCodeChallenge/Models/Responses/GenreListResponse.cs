using System.Collections.Generic;
using Newtonsoft.Json;

namespace MobileDevCodeChallenge.Models.Responses
{
    public class GenreListResponse
    {
        [JsonProperty("genres")]
        public List<MovieGenre> Genres { get; set; }
    }
}