using System.Windows.Input;
using Xamarin.Forms;

namespace Projeto.Controls
{
    public class ListViewExt : ListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
         BindableProperty.Create(nameof(ItemTappedCommand),
                           typeof(ICommand),
                           typeof(ListView),
                           null);
        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        public ListViewExt()
           : base()
        {
            Initialize();
        }
        public ListViewExt(ListViewCachingStrategy cachingStrategy)
            : base(cachingStrategy)
        {
            Initialize();
        }

        private void Initialize()
        {         
            ItemTapped += ListView_ItemTapped;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (ItemTappedCommand != null && ItemTappedCommand.CanExecute(null))
                ItemTappedCommand.Execute(e.Item);
        }
    }
}
