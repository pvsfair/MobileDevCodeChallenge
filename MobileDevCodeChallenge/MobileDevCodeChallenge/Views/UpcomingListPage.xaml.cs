using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MobileDevCodeChallenge.Models;
using MobileDevCodeChallenge.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDevCodeChallenge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpcomingListPage : ContentPage
    {

        public UpcomingListPage(UpcomingListVM vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", $"{(e.Item as Movie).Title} was tapped.", "OK");

            //Deselect Item
            ((ListView) sender).SelectedItem = null;
        }

        private void Handle_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var vm = BindingContext as UpcomingListVM;
            if (!vm.HasMoreMoviesToLoad)
                return;

            var s = e.Item as Movie;
            if (s == vm.Movies.Last())
            {
                if (vm.LoadMoreMoviesCommand.CanExecute(null))
                    vm.LoadMoreMoviesCommand.Execute(null);
            }
        }
    }
}
