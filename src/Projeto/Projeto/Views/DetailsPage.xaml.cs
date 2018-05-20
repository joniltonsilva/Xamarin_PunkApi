using Projeto.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projeto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        ToolbarItem favoriteItem = new ToolbarItem { };

        private DetailsViewModel ViewModel;
        private int ItemId;

        public DetailsPage(int itemId)
        {
            ItemId = itemId;
            InitializeComponent();
            BindingContext = (ViewModel = new DetailsViewModel());

            favoriteItem.Clicked += FavoriteItem_Clicked;
            ToolbarItems.Add(favoriteItem);
        }

        private void FavoriteItem_Clicked(object sender, System.EventArgs e)
        {
            ViewModel.ToggleFavorite((added) =>
            {
                favoriteItem.Icon = added ? "star.png" : "out_star.png";
                CatalogueView view = new CatalogueView();
                MessagingCenter.Send<CatalogueView, bool>(view, "Favorite", added);
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.Buscar(ItemId);
            favoriteItem.Icon = AppSettings.IsFavorite(ViewModel.Item.Id) ? "star.png" : "out_star.png";
        }
    }
}