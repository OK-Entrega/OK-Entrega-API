using System.ComponentModel;

namespace Commom.Enum
{
    public enum EnReasonDevolution
    {
        [Description("Produto com defeito")]
        Defect = 0,
        [Description("Produto danificado no transporte")]
        Damaged = 1,
        [Description("Arrependimento")]
        Repentance = 2,
        [Description("Produto diferente do solicitado")]
        Divergent = 3,
        [Description("Outra")]
        Other = 4
    }
}
