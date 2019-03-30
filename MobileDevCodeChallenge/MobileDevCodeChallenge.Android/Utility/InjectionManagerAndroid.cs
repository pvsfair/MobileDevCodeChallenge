using Autofac;
using MobileDevCodeChallenge.Utility.InjectionManager;
using MobileDevCodeChallenge.Utility.Interfaces;

namespace MobileDevCodeChallenge.Droid.Utility
{
    public class InjectionManagerAndroid : InjectionManager<InjectionManagerAndroid>
    {

        public override void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<CacheService>().As<ICacheService>();
        }
    }
}