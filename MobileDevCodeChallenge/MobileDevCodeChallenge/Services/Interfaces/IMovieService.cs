using System.Threading.Tasks;
using MobileDevCodeChallenge.Models.Responses;

namespace MobileDevCodeChallenge.Services.Interfaces
{
    public interface IMovieService
    {
        Task<MovieUpcomingResponse> GetUpcomingMovies(int page = 1);
    }
}