
using Android.App;
using Android.Widget;
using Projeto.Droid.Services;
using Projeto.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ToastService))]
namespace Projeto.Droid.Services
{
    public class ToastService : IToastService
    {
        public void Show(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}