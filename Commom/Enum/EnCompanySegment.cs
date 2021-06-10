using System.ComponentModel;

namespace Commom.Enum
{
    public enum EnCompanySegment
    {
        [Description("Roupa e calçado")]
        ClothingAndFootwear = 0,
        [Description("Finanças")]
        Finances = 1,
        [Description("Comida e bebida")]
        FoodAndDrink = 2,
        [Description("Saúde")]
        Health = 3,
        [Description("Automobilismo")]
        Motorsport = 4,
        [Description("Venda e marketing")]
        SaleAndMarketing = 5,
        [Description("Tecnologia")]
        Technology = 6,
        [Description("Outro")]
        Other = 7
    }
}
