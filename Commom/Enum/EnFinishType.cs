using System.ComponentModel;

namespace Commom.Enum
{
    public enum EnFinishType
    {
        [Description("Entregue")]
        Success = 0,
        [Description("Devolvida")]
        Devolution = 1
    }
}
