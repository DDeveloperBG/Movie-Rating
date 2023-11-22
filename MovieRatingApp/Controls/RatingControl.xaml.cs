using MovieRatingApp.Models.ViewDTOs;
using System.Windows.Input;

namespace MovieRatingApp.Controls
{
    public partial class RatingControl : ContentView
    {
        const string STAR_IMG_SRC = "Resources/Images/star_icon.svg";
        const string EMPTY_STAR_COLOR = "white";
        const string READ_ONLY_STAR_COLOR = "gray";
        const byte MAX_STARS_COUNT = 5;

        // Input
        public static readonly BindableProperty MovieIdProperty =
            BindableProperty.Create(nameof(MovieId), typeof(string), typeof(RatingControl), defaultValue: string.Empty);

        // Input
        public static readonly BindableProperty UserRatedProperty =
            BindableProperty.Create(nameof(UserRated), typeof(bool), typeof(RatingControl), defaultValue: false);

        // Input
        public static readonly BindableProperty StarsCountProperty =
            BindableProperty.Create(nameof(StarsCount), typeof(int), typeof(RatingControl), defaultValue: -1, propertyChanged: OnStarsCountPropertyChanged);

        // Input
        public static readonly BindableProperty ColorProperty =
          BindableProperty.Create(nameof(Color), typeof(string), typeof(RatingControl), defaultValue: "yellow");

        public static readonly BindableProperty UpdateMovieCommandProperty =
            BindableProperty.Create(nameof(UpdateMovieCommand), typeof(ICommand), typeof(RatingControl));

        // Internal
        public static readonly BindableProperty ImagesProperty =
            BindableProperty.Create(nameof(Images), typeof(List<RatingViewImage>), typeof(RatingControl));

        // Internal
        public static readonly BindableProperty CanSubmitRatingProperty =
           BindableProperty.Create(nameof(CanSubmitRating), typeof(bool), typeof(RatingControl), defaultValue: false);

        public RatingControl()
        {
            InitializeComponent();
        }

        public string MovieId
        {
            get => (string)GetValue(MovieIdProperty);
            set => SetValue(MovieIdProperty, value);
        }

        public bool UserRated
        {
            get => (bool)GetValue(UserRatedProperty);
            set => SetValue(UserRatedProperty, value);
        }

        public int StarsCount
        {
            get => (int)GetValue(StarsCountProperty);
            set => SetValue(StarsCountProperty, value);
        }

        public string Color
        {
            get => (string)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public ICommand UpdateMovieCommand
        {
            get => (ICommand)GetValue(UpdateMovieCommandProperty);
            set => SetValue(UpdateMovieCommandProperty, value);
        }

        public List<RatingViewImage> Images
        {
            get => (List<RatingViewImage>)GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }

        public bool CanSubmitRating
        {
            get => (bool)GetValue(CanSubmitRatingProperty);
            set => SetValue(CanSubmitRatingProperty, value);
        }

        public void OnStarClicked(object sender, EventArgs args)
        {
            if (!UserRated)
            {
                var imageButton = (ImageButton)sender;

                var starNumber = (byte)imageButton.CommandParameter;
                if (StarsCount == starNumber)
                {
                    starNumber = 0;
                }

                StarsCount = starNumber;
                if (!CanSubmitRating)
                {
                    CanSubmitRating = true;
                }
            }
        }

        public void OnSubmitClicked(object sender, EventArgs args)
        {
            if (UpdateMovieCommand?.CanExecute(null) == true)
            {
                var commandArgument = new UpdateMovieCommandInput
                {
                    MovieId = MovieId,
                    StarsCount = (byte)StarsCount
                };

                UpdateMovieCommand.Execute(commandArgument);
                UserRated = true;
                CanSubmitRating = false;
                UpdateImages();
            }
        }

        private static void OnStarsCountPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingControl)bindable;
            control.UpdateImages();
        }

        private void UpdateImages()
        {
            List<RatingViewImage> images = new();
            string fillColor = UserRated ? READ_ONLY_STAR_COLOR : Color;
            for (byte i = 1; i <= MAX_STARS_COUNT; i++)
            {
                string color = i > StarsCount ? EMPTY_STAR_COLOR : fillColor;

                RatingViewImage image = new()
                {
                    Number = i,
                    ImageUrl = STAR_IMG_SRC,
                    Color = color,
                };

                images.Add(image);
            }

            Images = images;
        }
    }
}