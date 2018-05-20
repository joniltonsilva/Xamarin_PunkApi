using Projeto.Models;
using Projeto.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Extended;

namespace Projeto.ViewModels
{
    public class CatalogueViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<CatalogoItemParaLista> CatalogueCollection { get; private set; }
        public List<int> FavoritesList { get; private set; }
        private const int PageSize = 1;
        public bool Favorites = false;

        private string pesquisaPor;
        public string PesquisaPor
        {
            get => pesquisaPor;
            set => SetProperty(ref pesquisaPor, value);
        }

        public CatalogueViewModel() : base()
        {
            Title = "Cervejeiros SA";
            Message = "It's freezing";
            CatalogueCollection = new InfiniteScrollCollection<CatalogoItemParaLista>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = !Favorites || (FavoritesList != null && FavoritesList.Count > CatalogueCollection.Count);
                    var page = CatalogueCollection.Count / PageSize;
                    var items = await Find(page + 1, PageSize);
                    IsBusy = false;
                    return items;
                }
            };
        }

        public async Task Catalogue(bool favorites)
        {
            Favorites = favorites;
            if (favorites)
            {
                FavoritesList = AppSettings.ObterFavoritos();
                if(FavoritesList == null)
                {
                    Toast("No favorite added");
                }
            }
            CatalogueCollection.Clear();
            await CatalogueCollection.LoadMoreAsync();
        }

        private async Task<List<CatalogoItemParaLista>> Find(int pagina, int itemsPorPagina)
        {
            List<CatalogoItemParaLista> catalogoItems = null;
            //IsBusy = true;
            try
            {
                if (Favorites)
                {
                    if (FavoritesList != null && FavoritesList.Count > 0)
                    {
                        List<int> idLista = FavoritesList.Skip((pagina - 1) * itemsPorPagina).Take(itemsPorPagina).ToList();
                        string ids = string.Join("|", idLista.Select(n => n.ToString()).ToArray());
                        catalogoItems = await ApiService.GetAsync<List<CatalogoItemParaLista>>(AppSettings.API_URL + $"?ids={ids}");
                    }
                }
                else
                {
                    catalogoItems = await ApiService.GetAsync<List<CatalogoItemParaLista>>(AppSettings.API_URL + $"?page={pagina}&per_page={itemsPorPagina}");
                }

                catalogoItems?.ForEach(x =>
                {
                    x.Favorite = AppSettings.IsFavorite(x.Id);
                });

            }
            catch (Exception ex)
            {
                Toast("Sorry, exception: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return catalogoItems;
        }


    }
}
