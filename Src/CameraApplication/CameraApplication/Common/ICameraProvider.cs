namespace CameraApplication.Common
{
    using System.Threading.Tasks;

    public interface ICameraProvider
    {
        Task<CameraResult> TakePictureAsync();
    }
}