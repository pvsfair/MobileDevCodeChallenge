using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevCodeChallenge.Models;

namespace MobileDevCodeChallenge.Services.Interfaces
{
    public interface IGenreService
    {
        Task<List<MovieGenre>> getMovieGenreList();
        Task<MovieGenre> getMovieGenre(int id);
    }
}