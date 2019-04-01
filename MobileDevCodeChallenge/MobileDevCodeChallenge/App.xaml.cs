using System;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using MobileDevCodeChallenge.Services;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.Utility.InjectionManager;
using MobileDevCodeChallenge.Utility.Interfaces;
using MobileDevCodeChallenge.ViewModels;
using MobileDevCodeChallenge.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobileDevCodeChallenge
{
    public partial class App : Application
    {
        public App()
        {
            #if DEBUG
            LiveReload.Init();
            #endif
            InitializeComponent();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(InjectionManager.Container));

            var navigator = InjectionManager.ResolveInstance<INavigator>();

            setupNavigation(navigator);

            navigator.initNavigation<UpcomingListVM>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void setupNavigation(INavigator navigator)
        {
            navigator.registerPageVM<UpcomingListVM, UpcomingListPage>();
            navigator.registerPageVM<MovieDetailsVM, MovieDetailsPage>();
        }
    }
}
