using System.ComponentModel;

namespace Commom.Enum
{
    public enum EnReasonOccurrence
    {
        [Description("Acidente de trânsito")]
        Accident = 0,
        [Description("Roubo de carga")]
        Theft = 1,
        [Description("Endereço errado")]
        WrongAddress = 2,
        [Description("Reentrega")]
        Redelivery = 3,
        [Description("Outra")]
        Other = 4
    }
}
