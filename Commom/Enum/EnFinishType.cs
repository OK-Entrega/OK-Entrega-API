using System.ComponentModel;

namespace Commom.Enum
{
    public enum EnFinishType
    {
        [Description("Entregue com sucesso")]
        Success = 0,
        [Description("Devolução")]
        Devolution = 1
    }
}
