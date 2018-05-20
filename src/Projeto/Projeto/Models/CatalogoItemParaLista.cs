using Newtonsoft.Json;

namespace Projeto.Models
{
    public class CatalogoItemParaLista
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("first_brewed")]
        public string FirstBrewed { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        public bool Favorite { get; set; }
    }
}
