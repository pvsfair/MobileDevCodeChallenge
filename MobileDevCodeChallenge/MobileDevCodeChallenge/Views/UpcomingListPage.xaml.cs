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
            var vm = BindingContext as UpcomingListVM;

            if (vm.MovieSelectedCommand.CanExecute(e.Item))
                vm.MovieSelectedCommand.Execute(e.Item);

            //Deselect Item
            ((ListView) sender).SelectedItem = null;
        }

        private void Handle_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var vm = BindingContext as UpcomingListVM;
            if (!vm.HasMoreMoviesToLoad)
            {
                return;
            }

            var loadAt = vm.Movies.Count - 3;

            loadAt = (loadAt < 0) ? 0 : loadAt;

            if (e.Item != vm.Movies[loadAt])
                return;

            if (vm.LoadMoreMoviesCommand.CanExecute(null))
                vm.LoadMoreMoviesCommand.Execute(null);

        }

        private void Handle_SearchEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = BindingContext as UpcomingListVM;

            if (vm.FilterMoviesCommand.CanExecute(null))
                vm.FilterMoviesCommand.Execute(null);

        }
    }
}
