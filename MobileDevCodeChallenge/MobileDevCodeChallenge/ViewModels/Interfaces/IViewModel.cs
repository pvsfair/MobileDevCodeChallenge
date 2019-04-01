using System.Collections.Generic;

namespace MobileDevCodeChallenge.ViewModels.Interfaces
{
    public interface IViewModel
    {
        void receiveNavigationParams(Dictionary<string, object> navParams = null);
    }
}