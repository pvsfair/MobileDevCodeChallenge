using MobileDevCodeChallenge.Models.Responses;

namespace MobileDevCodeChallenge.Services.Interfaces
{
    public interface IMovieService
    {
        MovieUpcomingResponse GetUpcomingMovies(int page);
    }
}