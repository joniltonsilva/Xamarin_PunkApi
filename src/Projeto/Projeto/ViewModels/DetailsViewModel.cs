using Projeto.Models;
using Projeto.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projeto.ViewModels
{
    public class IngredientsGroupList : ObservableCollection<Ingredient>
    {
        public string GroupName { get; set; }
    }

    public class DetailsViewModel : BaseViewModel
    {
        private CatalogoItemParaDetalhe item;
        public CatalogoItemParaDetalhe Item
        {
            get => item;
            set => SetProperty(ref item, value);
        }

        public ObservableCollection<IngredientsGroupList> Ingredients { get; set; }

        public DetailsViewModel() : base()
        {
            Title = "Cervejeiros SA";
            Ingredients = new ObservableCollection<IngredientsGroupList>(); 
        }

        public async Task Buscar(int id)
        {
            IsBusy = true;
            try
            {
                List<CatalogoItemParaDetalhe> itemParaDetalhes = await ApiService.GetAsync<List<CatalogoItemParaDetalhe>>(AppSettings.API_URL + $"/{id}");
                if (itemParaDetalhes.Count > 0)
                {                    
                    Item = itemParaDetalhes[0];
                    if(Item.Ingredients.Malt != null && Item.Ingredients.Malt.Count > 0)
                    {
                        var malt = new IngredientsGroupList() { GroupName = "MALT" };                        
                        Item.Ingredients.Malt.ForEach(x => malt.Add(x));
                        Ingredients.Add(malt);
                    }

                    if (Item.Ingredients.Hops != null && Item.Ingredients.Hops.Count > 0)
                    {
                        var hops = new IngredientsGroupList() { GroupName = "HOPS" };
                        Item.Ingredients.Hops.ForEach(x => hops.Add(x));
                        Ingredients.Add(hops);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }

        }

        public void ToggleFavorite(Action<bool> callBack)
        {
            bool added = AppSettings.ToggleFavorite(Item.Id);
            Toast(added ? "Added to favorites": "Removed in favorites");
            callBack?.Invoke(added);            
        }
    }
}
