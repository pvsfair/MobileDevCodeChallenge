using System.Collections.Generic;
using System.ComponentModel;
using MobileDevCodeChallenge.Models;
using MobileDevCodeChallenge.Utility.Interfaces;
using MobileDevCodeChallenge.ViewModels.Interfaces;
using Xamarin.Forms;

namespace MobileDevCodeChallenge.ViewModels
{
    public class MovieDetailsVM : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigator Navigator { get; set; }

        public MovieDetailsVM(INavigator navigator)
        {
            Navigator = navigator;
        }

        public Movie SelectedMovie { get; set; }

        public async void receiveNavigationParams(Dictionary<string, object> navParams = null)
        {
            if (!navParams.ContainsKey("movieObj"))
                await Navigator.navigateBack();

            SelectedMovie = navParams["movieObj"] as Movie;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMovie)));
        }
    }
}