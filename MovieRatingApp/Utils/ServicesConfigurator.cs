using AutoMapper;
using MovieRatingApp.Models.Http;
using MovieRatingApp.Services.Database;
using MovieRatingApp.Services.ExternalMoviesApi;
using MovieRatingApp.ViewModels;
using MovieRatingApp.Views;

namespace MovieRatingApp.Utils
{
    public static class ServicesConfigurator
    {
        public static void AddDependencies(IServiceCollection servicesCollection)
        {
            // Services related
            servicesCollection.AddTransient(_ => new HttpClient());
#if __ANDROID__
            servicesCollection.AddTransient(_ => new HttpClient(new Xamarin.Android.Net.AndroidMessageHandler()));
#else
            servicesCollection.AddTransient(_ => new HttpClient());
#endif

            servicesCollection.AddSingleton(GetMapper());

            servicesCollection.AddTransient<HttpHelperService>();
            servicesCollection.AddTransient<IExternalMoviesApiService, MoviesMiniDatabaseApiService>();
            servicesCollection.AddSingleton<IDbService, DbService>();

            // Views related
            servicesCollection.AddSingleton<MainPage>();
            servicesCollection.AddSingleton<MainViewModel>();
        }

        private static IMapper GetMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}
