using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileDevCodeChallenge.Models;
using MobileDevCodeChallenge.Models.Responses;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.Utility.Interfaces;
using MobileDevCodeChallenge.ViewModels.Interfaces;
using Xamarin.Forms;

namespace MobileDevCodeChallenge.ViewModels
{
    public class UpcomingListVM : IViewModel
    {
        public IConfigurationService ConfigurationService { get; protected set; }
        public IMovieService Service { get; protected set; }
        public IGenreService GenreService { get; protected set; }
        public INavigator Navigator { get; protected set; }

        public ICommand LoadMoreMoviesCommand { get; }
        public ICommand MovieSelectedCommand { get; }
//        public ICommand RefreshCommand { get; }

        public UpcomingListVM(IConfigurationService configurationService, IMovieService movieService, IGenreService genreService, INavigator navigator)
        {
            ConfigurationService = configurationService;
            Service = movieService;
            GenreService = genreService;
            Navigator = navigator;

            LoadMoreMoviesCommand = new Command(LoadMoreMovies);
            MovieSelectedCommand = new Command(MovieSelected);
//            RefreshCommand = new Command(HandleRefresh);
        }

        private Configuration configuration;
        private string _posterUrlBase;
        private string _backdropUrlBase;
        private int Page { get; set; } = 1;
        private int TotalResults { get; set; } = 100;
        public bool HasMoreMoviesToLoad => Movies.Count < TotalResults;
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();
//        public bool ListViewRefreshing { get; set; }

        public async void receiveNavigationParams(Dictionary<string, object> navParams = null)
        {
            configuration = await ConfigurationService.GetConfiguration();

            _backdropUrlBase = configuration.Images.BackdropSizes.Contains("w780") ? $"{configuration.Images.SecureBaseUrl}w780" : $"{configuration.Images.SecureBaseUrl}original";
            _posterUrlBase = configuration.Images.PosterSizes.Contains("w500") ? $"{configuration.Images.SecureBaseUrl}w500" : $"{configuration.Images.SecureBaseUrl}original";

            LoadMoreMovies();
        }

//        private void HandleRefresh()
//        {
//            ListViewRefreshing = true;
//
//            Page = 1;
//            Movies.Clear();
//            LoadMoreMovies();
//
//            ListViewRefreshing = false;
//        }

        private async void MovieSelected(object selectedMovie)
        {
            Dictionary<string, object> navParams = new Dictionary<string, object>();
            navParams.Add("movieObj", selectedMovie);
            await Navigator.navigateToPageAsync<MovieDetailsVM>(navParams);
        }

        private async void LoadMoreMovies()
        {
//            ListViewRefreshing = true;
            var movieUpcomingResponse = await Service.GetUpcomingMovies(Page);
            Page = movieUpcomingResponse.Page + 1;
            TotalResults = movieUpcomingResponse.TotalResults;

            foreach (var movie in movieUpcomingResponse.Results)
            {
                movie.BackdropPath = $"{_backdropUrlBase}{movie.BackdropPath}";
                movie.PosterPath = $"{_posterUrlBase}{movie.PosterPath}";
                movie.MainGenres = await getMainGenres(movie.GenreIds, 3);
                movie.AllGenres = await getMainGenres(movie.GenreIds);
                var splitReleaseDate = movie.ReleaseDate.Split('-');
                movie.ReleaseDate = $"{splitReleaseDate[2]}/{splitReleaseDate[1]}/{splitReleaseDate[0]}";
                Movies.Add(movie);
            }
//            ListViewRefreshing = false;
        }

        private async Task<string> getMainGenres(List<int> movieGenreIds, int i = -1)
        {
            var sb = new StringBuilder();

            i = (i == -1) ? movieGenreIds.Count : i;

            for (int j = 0; j < i && j < movieGenreIds.Count; j++)
            {
                var genreName = (await GenreService.getMovieGenre(movieGenreIds[j])).Name;
                if (genreName == null)
                {
                    i++;
                    continue;
                }
                if (j > 0)
                    sb.Append(", ");
                sb.Append(genreName);
            }

            return sb.ToString();
        }
    }
}