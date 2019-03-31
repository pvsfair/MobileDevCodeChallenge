using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileDevCodeChallenge.Models;
using MobileDevCodeChallenge.Models.Responses;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.ViewModels.Interfaces;
using Xamarin.Forms;

namespace MobileDevCodeChallenge.ViewModels
{
    public class UpcomingListVM : IViewModel
    {
        public IConfigurationService ConfigurationService { get; protected set; }
        public IMovieService Service { get; protected set; }
        public ICommand LoadMoreMoviesCommand { get; }

        public UpcomingListVM(IConfigurationService configurationService, IMovieService service)
        {
            ConfigurationService = configurationService;
            Service = service;
            
            Page = 0;
            LoadMoreMoviesCommand = new Command(LoadMoreMovies);
        }

        private int Page { get; set; }
        private int TotalResults { get; set; } = 100;
        public bool HasMoreMoviesToLoad => Movies.Count < TotalResults;
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        public async void receiveNavigationParams(Dictionary<string, object> navParams = null)
        {
            LoadMoreMovies();
        }

        private async void LoadMoreMovies()
        {
            var movieUpcomingResponse = await Service.GetUpcomingMovies(++Page);
            Page = movieUpcomingResponse.Page;
            TotalResults = movieUpcomingResponse.TotalResults;
            movieUpcomingResponse.Results.ForEach(movie => Movies.Add(movie));
        }
    }
}