using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using MobileDevCodeChallenge.Models;
using MobileDevCodeChallenge.Models.Responses;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.Utility.Interfaces;
using MobileDevCodeChallenge.ViewModels.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MobileDevCodeChallenge.ViewModels
{
    public class UpcomingListVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IConfigurationService ConfigurationService { get; protected set; }
        public IMovieService Service { get; protected set; }
        public IGenreService GenreService { get; protected set; }
        public INavigator Navigator { get; protected set; }

        public ICommand LoadMoreMoviesCommand { get; }
        public ICommand MovieSelectedCommand { get; }
        public ICommand FilterMoviesCommand { get; }
//        public ICommand RefreshCommand { get; }

        public UpcomingListVM(IConfigurationService configurationService, IMovieService movieService, IGenreService genreService, INavigator navigator)
        {
            ConfigurationService = configurationService;
            Service = movieService;
            GenreService = genreService;
            Navigator = navigator;

            LoadMoreMoviesCommand = new Command(LoadMoreHandle);
            MovieSelectedCommand = new Command(MovieSelected);
            FilterMoviesCommand = new Command(restartSearchTimer);
            //            RefreshCommand = new Command(HandleRefresh);

            SearchTimer = new Timer(700) { AutoReset = true };
            SearchTimer.Elapsed += doFilterMovies;
        }

        private Configuration configuration;
        private string _posterUrlBase;
        private string _backdropUrlBase;
        private int Page { get; set; } = 1;
        private int TotalResults { get; set; } = 100;
        public bool HasMoreMoviesToLoad => MoviesBackup.Count < TotalResults;
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();
        private ObservableCollection<Movie> MoviesBackup { get; set; } = new ObservableCollection<Movie>();

        public bool ShowRefreshing { get; set; }
        public string SearchText { get; set; }

        public Timer SearchTimer { get; set; }

        public async void receiveNavigationParams(Dictionary<string, object> navParams = null)
        {
            ShowRefreshing = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowRefreshing)));
            configuration = await ConfigurationService.GetConfiguration();

            _backdropUrlBase = configuration.Images.BackdropSizes.Contains("w780") ? $"{configuration.Images.SecureBaseUrl}w780" : $"{configuration.Images.SecureBaseUrl}original";
            _posterUrlBase = configuration.Images.PosterSizes.Contains("w500") ? $"{configuration.Images.SecureBaseUrl}w500" : $"{configuration.Images.SecureBaseUrl}original";

            await LoadMoreMovies();
            ShowRefreshing = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowRefreshing)));
        }

        private async void doFilterMovies(Object source, ElapsedEventArgs e)
        {
            SearchTimer.Stop();
            await filterMoviesActivate();
        }

        private void restartSearchTimer()
        {
            SearchTimer.Stop();
            SearchTimer.Start();
        }

        private async Task filterMoviesActivate()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                ShowRefreshing = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowRefreshing)));
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    var filtered = MoviesBackup.Where(movie => movie.Title.ToLower().Contains(SearchText.ToLower()))
                            .ToList();
                    while (filtered.Count < 10 && HasMoreMoviesToLoad)
                    {
                        await LoadMoreMovies();
                        filtered = MoviesBackup.Where(movie => movie.Title.ToLower().Contains(SearchText.ToLower()))
                            .ToList();
                    }
                    
                    Movies.Clear();
                    filtered.ForEach(Movies.Add);
                    ShowRefreshing = false;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowRefreshing)));
                    return;
                }

                Movies.Clear();
                MoviesBackup.ForEach(Movies.Add);
                ShowRefreshing = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowRefreshing)));
            });
        }

        private async void MovieSelected(object selectedMovie)
        {
            Dictionary<string, object> navParams = new Dictionary<string, object>();
            navParams.Add("movieObj", selectedMovie);
            await Navigator.navigateToPageAsync<MovieDetailsVM>(navParams);
        }

        private async void LoadMoreHandle()
        {
            await LoadMoreMovies();
            if (!string.IsNullOrWhiteSpace(SearchText))
                await filterMoviesActivate();
        }

        private async Task LoadMoreMovies()
        {
            var movieUpcomingResponse = await Service.GetUpcomingMovies(Page);
            Page = movieUpcomingResponse.Page + 1;
            TotalResults = movieUpcomingResponse.TotalResults;

            foreach (var movie in movieUpcomingResponse.Results)
            {
                movie.BackdropPath = (string.IsNullOrWhiteSpace(movie.BackdropPath)) ? "NoBackdrop.png" : $"{_backdropUrlBase}{movie.BackdropPath}"; 
                movie.PosterPath = (string.IsNullOrWhiteSpace(movie.PosterPath)) ? "NoPoster.png" : $"{_posterUrlBase}{movie.PosterPath}";
                movie.MainGenres = await getMainGenres(movie.GenreIds, 3);
                movie.AllGenres = await getMainGenres(movie.GenreIds);
                var splitReleaseDate = movie.ReleaseDate.Split('-');
                movie.ReleaseDate = $"{splitReleaseDate[2]}/{splitReleaseDate[1]}/{splitReleaseDate[0]}";
                Movies.Add(movie);
                MoviesBackup.Add(movie);
            }
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