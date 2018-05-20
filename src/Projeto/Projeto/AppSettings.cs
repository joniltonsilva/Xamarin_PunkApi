using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.Generic;

namespace Projeto
{
    public static class AppSettings
    {
        public const string API_URL = "https://api.punkapi.com/v2/beers";

        private static ISettings Preferences
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private static List<int> favoritos;

        public static bool IsFavorite(int id)
        {
            favoritos = Favoritos ?? new List<int>();
            return favoritos.Contains(id);
        }
        public static bool ToggleFavorite(int id)
        {
            bool added = false;
            favoritos = Favoritos ?? new List<int>();
            if (favoritos.Contains(id))
            {
                favoritos.Remove(id);
            }
            else
            {
                added = true;
                favoritos.Add(id);
            }
            Favoritos = favoritos;
            return added;
        }        

        public static List<int> ObterFavoritos()
        {
            return Favoritos;
        }

        private static List<int> Favoritos
        {
            get => JsonConvert.DeserializeObject<List<int>>(Preferences.GetValueOrDefault(nameof(Favoritos), ""));
            set => Preferences.AddOrUpdateValue(nameof(Favoritos), JsonConvert.SerializeObject(value));
        }
    }
}
