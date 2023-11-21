using MovieRatingApp.ViewModels;

namespace MovieRatingApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as MainViewModel;
            if (viewModel != null)
            {
                Task.Run(viewModel.LoadItemsAsync);
            }
        }
    }
}
