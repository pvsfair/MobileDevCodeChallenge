using System;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using MobileDevCodeChallenge.Services;
using MobileDevCodeChallenge.Services.Interfaces;
using MobileDevCodeChallenge.ViewModels;
using MobileDevCodeChallenge.Views;

namespace MobileDevCodeChallenge.Utility.InjectionManager
{
    public abstract class InjectionManager
    {
        public static IContainer Container { get; protected set; }
        public static InjectionManager Instance { get; protected set; }

        public static T ResolveInstance<T>()
        {
            return Container.Resolve<T>();
        }

        public abstract void RegisterDependencies(ContainerBuilder builder);

    }

    public abstract class InjectionManager<T> : InjectionManager where T : InjectionManager, new()
    {
        public static IContainer Initialize()
        {
            if (Instance != null && typeof(T) == Instance.GetType())
                throw new InvalidOperationException("Container already initialized");

            if (Container != null)
                return Container;

            ContainerBuilder builder = new ContainerBuilder();
            Instance = new T();

            RegisterLocalReferences(builder);

            Instance.RegisterDependencies(builder);

            Container = builder.Build();
            return Container;
        }

        private static void RegisterLocalReferences(ContainerBuilder builder)
        {
            builder.RegisterType<MovieService>().As<IMovieService>();
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>();

            builder.RegisterType<UpcomingListVM>();
            builder.RegisterType<MovieDetailsVM>();

            builder.RegisterType<UpcomingListPage>();
            builder.RegisterType<MovieDetailsPage>();
        }
    }
}