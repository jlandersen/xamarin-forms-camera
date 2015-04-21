namespace CameraApplication.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using CameraApplication.Common;

    using Xamarin.Forms;

    public class TakePictureViewModel : ObservableObject
    {
        private ImageSource picture;

        public ImageSource Picture
        {
            get
            {
                return this.picture;
            }
            set
            {
                if (Equals(value, this.picture))
                {
                    return;
                }
                this.picture = value;
                OnPropertyChanged();
            }
        }

        public ICommand TakePicture { get; set; }

        private ICameraProvider cameraProvider;

        public TakePictureViewModel(ICameraProvider cameraProvider)
        {
            if (cameraProvider == null)
            {
                throw new ArgumentNullException("cameraProvider");
            }

            TakePicture = new Command(async () => await TakePictureAsync());
            this.cameraProvider = cameraProvider;
        }

        private async Task TakePictureAsync()
        {
            var photoResult = await cameraProvider.TakePictureAsync();

            if (photoResult != null)
            {
                Picture = photoResult.Picture;
            }
        }
    }
}