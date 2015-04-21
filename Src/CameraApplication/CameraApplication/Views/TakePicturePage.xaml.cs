namespace CameraApplication.Views
{
    using CameraApplication.Common;
    using CameraApplication.ViewModels;

    using Xamarin.Forms;

    public partial class TakePicturePage : ContentPage
    {
        public TakePicturePage()
        {
            InitializeComponent();

            BindingContext = new TakePictureViewModel(DependencyService.Get<ICameraProvider>());
        }
    }
}
