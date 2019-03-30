using System;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using MobileDevCodeChallenge.Services;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.Utility.InjectionManager;
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
            InitializeComponent();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(InjectionManager.Container));

            MainPage = InjectionManager.ResolveInstance<UpcomingListPage>();
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
    }
}
