using System;
using System.Collections.Generic;

namespace MobileDevCodeChallenge.Models
{
    public class Movie
    {
        public string PosterPath;
        public bool Adult;
        public string Overview;
        public string ReleaseDate;
        public List<int> GenreIds;
        public int Id;
        public string OriginalTitle;
        public string OriginalLanguage;
        public string Title;
        public string BackdropPath;
        public Decimal Popularity;
        public int VoteCount;
        public bool Video;
        public Decimal VoteAverage;
    }
}