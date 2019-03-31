using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using MobileDevCodeChallenge.Utility.Interfaces;
using Xamarin.Forms;

namespace MobileDevCodeChallenge.Utility
{
    public class Navigator : INavigator
    {
        protected Dictionary<string, Type> ViewModelRelation = new Dictionary<string, Type>();

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
        }

        public Task navigateToPageAsync<TViewModel>(Dictionary<string, object> navParams = null)
        {
            throw new System.NotImplementedException();
        }

        public Task navigateBack(Dictionary<string, object> navParams = null)
        {
            throw new System.NotImplementedException();
        }

        private Page getPage<TViewModel>(Dictionary<string, object> navParams)
        {
            var viewModelName = typeof(TViewModel).FullName;
            if (!ViewModelRelation.ContainsKey(viewModelName))
                throw new Exception("The ViewModel sent is not registered on the navigator.");

            var viewType = ViewModelRelation[typeof(TViewModel).FullName];

            var page = InjectionManager.InjectionManager.ResolveInstance(viewType) as Page;
            return page;
        }
    }
}