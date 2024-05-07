using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CrossoutCraftingAssistant
{
    public partial class FullRecipeClass
    {
        [JsonProperty("recipe")]
        public Recipe Recipe { get; set; }
    }

    public partial class Recipe
    {

        [JsonProperty("sumBuy")]
        public long SumBuy { get; set; }

        [JsonProperty("sumSell")]
        public long SumSell { get; set; }

        [JsonProperty("sumBuyFormat")]
        public string SumBuyFormat { get; set; }

        [JsonProperty("sumSellFormat")]
        public string SumSellFormat { get; set; }

        [JsonProperty("buyPriceTimesNumber")]
        public long BuyPriceTimesNumber { get; set; }

        [JsonProperty("sellPriceTimesNumber")]
        public long SellPriceTimesNumber { get; set; }

        [JsonProperty("formatBuyPriceTimesNumber")]
        public string FormatBuyPriceTimesNumber { get; set; }

        [JsonProperty("formatSellPriceTimesNumber")]
        public string FormatSellPriceTimesNumber { get; set; }

        [JsonProperty("ingredientSum")]
        public Recipe IngredientSum { get; set; }

        [JsonProperty("item")]
        public Item Item { get; set; }

        [JsonProperty("ingredients")]
        public List<Recipe> Ingredients { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("localizedName")]
        public string LocalizedName { get; set; }

        [JsonProperty("availableName")]
        public string AvailableName { get; set; }

        [JsonProperty("sellOffers")]
        public long SellOffers { get; set; }

        [JsonProperty("sellPrice")]
        public long SellPrice { get; set; }

        [JsonProperty("buyOrders")]
        public long BuyOrders { get; set; }

        [JsonProperty("buyPrice")]
        public long BuyPrice { get; set; }

        [JsonProperty("craftable")]
        public long Craftable { get; set; }

        [JsonProperty("craftingSellSum")]
        public long CraftingSellSum { get; set; }

        [JsonProperty("craftingBuySum")]
        public long CraftingBuySum { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("demandSupplyRatio")]
        public double DemandSupplyRatio { get; set; }

        [JsonProperty("margin")]
        public double Margin { get; set; }

        [JsonProperty("roi")]
        public double Roi { get; set; }

        [JsonProperty("craftingMargin")]
        public double CraftingMargin { get; set; }

        [JsonProperty("formatDemandSupplyRatio")]
        public string FormatDemandSupplyRatio { get; set; }

        [JsonProperty("formatMargin")]
        public string FormatMargin { get; set; }

        [JsonProperty("formatRoi")]
        public string FormatRoi { get; set; }

        [JsonProperty("formatCraftingMargin")]
        public string FormatCraftingMargin { get; set; }

        [JsonProperty("craftVsBuy")]
        public string CraftVsBuy { get; set; }

        [JsonProperty("lastUpdateTime")]
        public DateTimeOffset LastUpdateTime { get; set; }

        [JsonProperty("rarityId")]
        public long RarityId { get; set; }

        [JsonProperty("rarityName")]
        public string RarityName { get; set; }

        [JsonProperty("recipeId")]
        public long RecipeId { get; set; }

        [JsonProperty("typeName")]
        public string TypeName { get; set; }

        [JsonProperty("faction")]
        public string Faction { get; set; }

        [JsonProperty("formatBuyPrice")]
        public string FormatBuyPrice { get; set; }

        [JsonProperty("formatSellPrice")]
        public string FormatSellPrice { get; set; }

        [JsonProperty("formatCraftingSellSum")]
        public string FormatCraftingSellSum { get; set; }

        [JsonProperty("formatCraftingBuySum")]
        public string FormatCraftingBuySum { get; set; }

        [JsonProperty("craftingResultAmount")]
        public long CraftingResultAmount { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("imagePath")]
        public string ImagePath { get; set; }

    }


    public partial class FullRecipeClass
    {
        public static FullRecipeClass FromJson(string json) => JsonConvert.DeserializeObject<FullRecipeClass>(json, CrossoutCraftingAssistant.Converter.Settings);
    }


    public static class Serialize
    {
        public static string ToJson(this FullRecipeClass self) => JsonConvert.SerializeObject(self, CrossoutCraftingAssistant.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }



}
