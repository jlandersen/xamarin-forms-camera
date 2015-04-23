using CameraApplication.WinPhone.Dependencies;

using Xamarin.Forms;

[assembly: Dependency(typeof(CameraProvider))]
namespace CameraApplication.WinPhone.Dependencies
{
    using System.Threading.Tasks;

    using CameraApplication.Common;

    using Microsoft.Phone.Tasks;

    public class CameraProvider : ICameraProvider
    {
        private TaskCompletionSource<CameraResult> tcs;

        public Task<CameraResult> TakePictureAsync()
        {
            CameraCaptureTask cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += CameraCaptureTaskOnCompleted;

            cameraCaptureTask.Show();

            tcs = new TaskCompletionSource<CameraResult>();

            return tcs.Task;
        }

        private void CameraCaptureTaskOnCompleted(object sender, PhotoResult photoResult)
        {
            if (photoResult.TaskResult == TaskResult.None)
            {
                tcs.TrySetException(photoResult.Error);
                return;
            }

            if (photoResult.TaskResult == TaskResult.Cancel)
            {
                tcs.TrySetResult(null);
                return;
            }
            
            CameraResult result = new CameraResult();
            result.Picture = ImageSource.FromStream(() => photoResult.ChosenPhoto);
            result.FullFilePath = photoResult.OriginalFileName;

            tcs.TrySetResult(result);
        }
    }
}