using CommunityToolkit.Mvvm.ComponentModel;
using MovieRatingApp.Models.ViewDTOs;
using MovieRatingApp.Services.Database;

namespace MovieRatingApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IDbService dbService;

        [ObservableProperty]
        private List<MainPageMovie> movies;

        [ObservableProperty]
        private bool moviesAreLoading;

        public MainViewModel(IDbService dbService)
        {
            this.dbService = dbService;
            
            Movies = new List<MainPageMovie>();
            MoviesAreLoading = true;
        }

        public async Task LoadItemsAsync()
        {
            Movies = await dbService.GetMoviesAsync<MainPageMovie>();
            MoviesAreLoading = false;
        }
    }
}
