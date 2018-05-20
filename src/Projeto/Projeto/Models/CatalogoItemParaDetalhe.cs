using Newtonsoft.Json;
using System.Collections.Generic;

namespace Projeto.Models
{
    public class CatalogoItemParaDetalhe
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

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("abv")]
        public string Abv { get; set; }

        [JsonProperty("ibu")]
        public string Ibu { get; set; }

        [JsonProperty("target_fg")]
        public string TargetFG { get; set; }

        [JsonProperty("target_og")]
        public string TargetOG { get; set; }

        [JsonProperty("ebc")]
        public string Ebc { get; set; }

        [JsonProperty("srm")]
        public string Srm { get; set; }

        [JsonProperty("ph")]
        public string Ph { get; set; }

        [JsonProperty("attenuation_level")]
        public string AttenuationLevel { get; set; }

        [JsonProperty("volume")]
        public Measure Volume { get; set; }

        [JsonProperty("boil_volume")]
        public Measure BoilVolume { get; set; }

        [JsonProperty("method")]
        public Dictionary<string, object> Method { get; set; }

        [JsonProperty("ingredients")]
        public Ingredients Ingredients { get; set; }

        [JsonProperty("food_pairing")]
        public IList<string> FoodPairing { get; set; }

        [JsonProperty("brewers_tips")]
        public string BrewersTips { get; set; }

        [JsonProperty("contributed_by")]
        public string ContributedBy { get; set; }


    }

    public class Volume
    {
        [JsonProperty("value")]
        public int Value { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class Method
    {
        [JsonProperty("mash_temp")]
        public List<MashTemp> MashTemp { get; set; }
        [JsonProperty("fermentation")]
        public Fermentation Fermentation { get; set; }
        [JsonProperty("twist")]
        public Twist Twist { get; set; }
    }


    public class MashTemp
    {
        [JsonProperty("temp")]
        public List<Measure> Temp { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
    }


    public class Fermentation
    {
        [JsonProperty("temp")]
        public Measure Temp { get; set; }
    }

    public class Twist
    {
    }

    public class Ingredients
    {
        [JsonProperty("malt")]
        public List<Malt> Malt { get; set; }

        [JsonProperty("hops")]
        public List<Hop> Hops { get; set; }

        [JsonProperty("Yeast")]
        public string Yeast { get; set; }
    }

    public class Ingredient
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        public Measure Amount { get; set; }
    }

    public class Malt : Ingredient
    {

    }

    public class Hop : Ingredient
    {
        [JsonProperty("add")]
        public string Add { get; set; }

        [JsonProperty("attribute")]
        public string Attribute { get; set; }
    }    

    public class Measure
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }
    }
}
