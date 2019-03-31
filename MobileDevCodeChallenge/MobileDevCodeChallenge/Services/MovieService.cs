using System.Threading.Tasks;
using MobileDevCodeChallenge.Http;
using MobileDevCodeChallenge.Models.Responses;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.Utility;
using MobileDevCodeChallenge.Utility.InjectionManager;

namespace MobileDevCodeChallenge.Services
{
    public class MovieService : IMovieService
    {
        private readonly string urlUpcomingMovies = "/movie/upcoming";

        public async Task<MovieUpcomingResponse> GetUpcomingMovies(int page = 1)
        {
            return await InjectionManager.ResolveInstance<IHttpCall>()
                                         .baseUrl(TMDbBaseConfiguration.GetBaseUrlTmdb())
                                         .asGet(urlUpcomingMovies)
                                         .addQueryString("api_key", TMDbBaseConfiguration.GetApiKey())
                                         .addQueryString("page", page)
                                         .requestAsync<MovieUpcomingResponse>();
        }
    }
}