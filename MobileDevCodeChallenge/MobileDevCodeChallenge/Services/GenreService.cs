using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevCodeChallenge.Http;
using MobileDevCodeChallenge.Models;
using MobileDevCodeChallenge.Models.Responses;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.Utility;
using MobileDevCodeChallenge.Utility.InjectionManager;

namespace MobileDevCodeChallenge.Services
{
    public class GenreService : IGenreService
    {
        private readonly string urlGenreList = "/genre/movie/list";

        private List<MovieGenre> _movieGenreList = null;

        public async Task<List<MovieGenre>> getMovieGenreList()
        {
            if (_movieGenreList != null)
                return _movieGenreList;

            var response = await InjectionManager.ResolveInstance<IHttpCall>()
                                                    .baseUrl(TMDbBaseConfiguration.GetBaseUrlTmdb())
                                                    .asGet(urlGenreList)
                                                    .addApiKey()
                                                    .requestAsync<GenreListResponse>();
            _movieGenreList = response.Genres;

            return _movieGenreList;
        }

        public async Task<MovieGenre> getMovieGenre(int id)
        {
            if (_movieGenreList == null)
                await getMovieGenreList();
            foreach (var movieGenre in _movieGenreList)
            {
                if (movieGenre.Id == id)
                    return movieGenre;
            }

            return default;
        }
    }
}