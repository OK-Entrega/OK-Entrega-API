using System.ComponentModel;

namespace Commom.Enum
{
    public enum EnCompanySegment
    {
        [Description("Comida e bebida")]
        FoodAndDrink = 0,
        [Description("Roupa e calçado")]
        ClothingAndFootwear = 1,
        [Description("Saúde")]
        Health = 2,
        [Description("Venda e marketing")]
        SaleAndMarketing = 3,
        [Description("Tecnologia")]
        Technology = 4,
        [Description("Outro")]
        Other = 5
    }
}
