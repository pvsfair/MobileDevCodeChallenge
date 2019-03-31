using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MobileDevCodeChallenge.Utility.Interfaces;
using MobileDevCodeChallenge.ViewModels.Interfaces;
using Xamarin.Forms;

namespace MobileDevCodeChallenge.Utility
{
    public class Navigator : INavigator
    {
        protected Dictionary<string, Type> ViewModelRelation = new Dictionary<string, Type>();
        public Page CurrentPage => Application.Current.MainPage;

        public Navigator()
        {

        }

        public void registerPageVM<TViewModel, TPage>() where TPage : Page
        {
            ViewModelRelation.Add(typeof(TViewModel).FullName, typeof(TPage));
        }

        public void initNavigation<TViewModel>(Dictionary<string, object> navParams = null)
        {
            var page = getPage<TViewModel>(navParams);

            Application.Current.MainPage = new NavigationPage(page);
        }

        public async Task navigateToPageAsync<TViewModel>(Dictionary<string, object> navParams = null)
        {
            try
            {
                var page = getPage<TViewModel>(navParams);
                await CurrentPage.Navigation.PushAsync(page, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw ;
            }
        }

        public async Task navigateBack(Dictionary<string, object> navParams = null)
        {
            Page page;

            if (CurrentPage.Navigation.NavigationStack.Count > 0)
                await CurrentPage.Navigation.PopAsync(true);
            page = CurrentPage.Navigation.NavigationStack.LastOrDefault() ?? CurrentPage;
            setupNavigationParams(page, navParams);
        }

        private Page getPage<TViewModel>(Dictionary<string, object> navParams)
        {
            var viewModelName = typeof(TViewModel).FullName;
            if (!ViewModelRelation.ContainsKey(viewModelName))
                throw new Exception("The ViewModel sent is not registered on the navigator.");

            var viewType = ViewModelRelation[typeof(TViewModel).FullName];

            var page = InjectionManager.InjectionManager.ResolveInstance(viewType) as Page;

            setupNavigationParams(page, navParams);

            return page;
        }

        private static void setupNavigationParams(Page page, Dictionary<string, object> navParams)
        {
            if (page.BindingContext is IViewModel viewModel)
                viewModel.receiveNavigationParams(navParams);
        }
    }
}