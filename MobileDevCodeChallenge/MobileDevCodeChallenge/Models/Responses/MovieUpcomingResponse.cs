using System.Collections.Generic;

namespace MobileDevCodeChallenge.Models.Responses
{
    public class MovieUpcomingResponse
    {
        public int Page;
        public List<Movie> Results;
        public Dates Dates;
        public int TotalPages;
        public int TotalResults;
    }
}