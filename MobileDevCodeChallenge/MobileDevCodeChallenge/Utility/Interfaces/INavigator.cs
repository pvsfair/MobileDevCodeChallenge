using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileDevCodeChallenge.Utility.Interfaces
{
    public interface INavigator
    {
        void registerPageVM<TViewModel, TPage>() where TPage : Xamarin.Forms.Page;

        void initNavigation<TViewModel>(Dictionary<string, object> navParams = null);

        Task navigateToPageAsync<TViewModel>(Dictionary<string, object> navParams = null);
        Task navigateBack(Dictionary<string, object> navParams = null);
    }
}