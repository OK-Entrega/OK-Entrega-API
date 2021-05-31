using System.ComponentModel;

namespace Commom.Enum
{
    public enum EnVehicleType
    {
        [Description("Caminhão")]
        Truck = 0,
        [Description("Furgão")]
        Van = 1,
        [Description("Navio")]
        Ship = 2,
        [Description("Moto")]
        Motorcycle = 3,
        [Description("Outro")]
        Other = 4
    }
}
