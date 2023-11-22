using CommunityToolkit.Mvvm.ComponentModel;
using MovieRatingApp.Models.ViewDTOs;
using MovieRatingApp.Services.Database;
using System.Windows.Input;

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
            UpdateMovieCommand = new Command(UpdateMovieAsync);
        }

        public ICommand UpdateMovieCommand { get; init; }

        public async Task LoadItemsAsync()
        {
            Movies = await dbService.GetMoviesAsync<MainPageMovie>();
            MoviesAreLoading = false;
        }

        public void UpdateMovieAsync(object inputObj)
        {
            var input = (UpdateMovieCommandInput)inputObj;
            Task.Run(() => dbService.UpdateMovieRatingAsync(input.MovieId, input.StarsCount));
        }
    }
}
