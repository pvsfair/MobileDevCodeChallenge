using System.Collections.Generic;
using System.Collections.ObjectModel;
using MobileDevCodeChallenge.ViewModels.Interfaces;

namespace MobileDevCodeChallenge.ViewModels
{
    public class UpcomingListVM : IViewModel
    {
        public ObservableCollection<string> Items { get; set; }

        public UpcomingListVM()
        {

            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

        }

        public void receiveNavigationParams(Dictionary<string, object> navParams = null)
        {
        }
    }
}