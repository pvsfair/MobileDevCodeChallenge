using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileDevCodeChallenge.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDevCodeChallenge.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieDetailsPage : ContentPage
	{
		public MovieDetailsPage (MovieDetailsVM vm)
		{
			InitializeComponent ();
            BindingContext = vm;
        }
	}
}