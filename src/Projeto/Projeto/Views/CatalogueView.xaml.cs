using Projeto.Models;
using Projeto.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projeto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogueView : ContentPage
    {
        ToolbarItem Favorites = new ToolbarItem { Icon = "outline_stars.png" };
        public CatalogueViewModel ViewModel { get; set; }
        private bool loadData = true;

        CatalogueView catalogueView;

        public CatalogueView()
        {
            InitializeComponent();
            BindingContext = (ViewModel = new CatalogueViewModel());
            Favorites.Clicked += Favorite_Clicked;
            ToolbarItems.Add(Favorites);
            catalogueView = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (loadData)
            {
                await ViewModel.Catalogue(false);
                loadData = false;
            }
        }

        private async Task ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            listView.SelectedItem = null;
            var item = (e.Item as CatalogoItemParaLista);
            int id = item.Id;

            MessagingCenter.Subscribe<CatalogueView, bool>(catalogueView, "Favorite", async (sender2, added) =>
            {                
                await ViewModel.Catalogue(ViewModel.Favorites);
            });
            await Navigation.PushAsync(new DetailsPage(id));
        }

        private void Filter_Clicked(object sender, System.EventArgs e)
        {
            ViewModel.IsBusy = false;
        }

        private async void Favorite_Clicked(object sender, System.EventArgs e)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender;
            bool loadFavorite = toolbarItem.Icon == "outline_stars.png";
            await ViewModel.Catalogue(loadFavorite);
            toolbarItem.Icon = loadFavorite ? "round_star.png" : "outline_stars.png";
        }
    }
}