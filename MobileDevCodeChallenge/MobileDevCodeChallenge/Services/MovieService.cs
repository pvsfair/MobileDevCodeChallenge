using System.Threading.Tasks;
using MobileDevCodeChallenge.Http;
using MobileDevCodeChallenge.Models.Responses;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.Utility.InjectionManager;

namespace MobileDevCodeChallenge.Services
{
    public class MovieService : IMovieService
    {
        private string urlUpcomingMovies = "/movie/upcoming";

        public async Task<MovieUpcomingResponse> GetUpcomingMovies(int page = 1)
        {
            var confService = InjectionManager.ResolveInstance<IConfigurationService>();
            var apiKey = confService.GetApiKey();

            return await InjectionManager.ResolveInstance<IHttpCall>().baseUrl(confService.GetBaseUrlTmdb())
                                                         .asGet(urlUpcomingMovies)
                                                         .addQueryString("api_key", confService.GetApiKey())
                                                         .addQueryString("page", page)
                                                         .requestAsync<MovieUpcomingResponse>();
        }
    }
}