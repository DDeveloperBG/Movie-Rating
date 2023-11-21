using Android.Media;
using static Android.Provider.MediaStore;

namespace MovieRatingApp.Controls;

public partial class RatingControl : ContentView
{
    const string FILLED_STAR_IMG_SRC = "Resources/Images/filled_star_icon.svg";
    const string EMPTY_STAR_IMG_SRC = "Resources/Images/empty_star_icon.svg";
    const int MAX_STARS_COUNT = 10;

    public static readonly BindableProperty StarsCountProperty =
        BindableProperty.Create(nameof(StarsCount), typeof(int), typeof(RatingControl), propertyChanged: OnStarsCountPropertyChanged);

    public static readonly BindableProperty ImagesProperty =
        BindableProperty.Create(nameof(Images), typeof(List<string>), typeof(RatingControl));

    public RatingControl()
    {
        InitializeComponent();
    }

    public int StarsCount
    {
        get => (int)GetValue(StarsCountProperty);
        set => SetValue(StarsCountProperty, value);
    }

    public List<string> Images
    {
        get => (List<string>)GetValue(ImagesProperty);
        set => SetValue(ImagesProperty, value);
    }

    private static void OnStarsCountPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (RatingControl)bindable;
        control.UpdateImages((int)newValue);
    }

    private void UpdateImages(int starsCount)
    {
        List<string> images = new List<string>();
        for (int i = 0; i < MAX_STARS_COUNT; i++)
        {
            string src = i < StarsCount ? FILLED_STAR_IMG_SRC : EMPTY_STAR_IMG_SRC;

            images.Add(src);
        }

        Images = images;
    }
}