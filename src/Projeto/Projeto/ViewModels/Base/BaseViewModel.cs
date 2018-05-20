using Projeto.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Projeto.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string message = "Aguarde...";
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value, "IsBusy");
        }

        protected IApiService ApiService { get; }
        protected IToastService ToastService { get; }

        public BaseViewModel()
        {
            ApiService = DependencyService.Get<IApiService>();
            ToastService = DependencyService.Get<IToastService>();
        }

        public virtual void Toast(string message)
        {
            ToastService.Show(message);
        }
    }
}
