using MovieRatingApp.Models.ViewDTOs;

namespace MovieRatingApp.Controls
{
    public partial class RatingControl : ContentView
    {
        const string STAR_IMG_SRC = "Resources/Images/star_icon.svg";
        const string EMPTY_IMG_COLOR = "white";
        const int MAX_STARS_COUNT = 5;

        public static readonly BindableProperty StarsCountProperty =
            BindableProperty.Create(nameof(StarsCount), typeof(int), typeof(RatingControl), defaultValue: 0, propertyChanged: OnStarsCountPropertyChanged);

        public static readonly BindableProperty ColorProperty =
          BindableProperty.Create(nameof(Color), typeof(string), typeof(RatingControl), defaultValue: "yellow");

        public static readonly BindableProperty ImagesProperty =
            BindableProperty.Create(nameof(Images), typeof(List<RatingViewImage>), typeof(RatingControl));

        public RatingControl()
        {
            InitializeComponent();
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

        public List<RatingViewImage> Images
        {
            get => (List<RatingViewImage>)GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }

        private static void OnStarsCountPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingControl)bindable;
            control.UpdateImages();
        }

        private void UpdateImages()
        {
            List<RatingViewImage> images = new();
            for (int i = 0; i < MAX_STARS_COUNT; i++)
            {
                RatingViewImage image = new()
                {
                    ImageUrl = STAR_IMG_SRC,
                    Color = i < StarsCount ? Color : EMPTY_IMG_COLOR,
                };

                images.Add(image);
            }

            Images = images;
        }
    }
}