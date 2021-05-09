using System.ComponentModel;

namespace Commom.Enum
{
    public enum EnIssueType
    {
        [Description("Normal")]
        Normal = 1,
        [Description("Contingência FS-IA")]
        FSIA = 2,
        [Description("Contingência SCAN")]
        SCAN = 3,
        [Description("Contingência DPEC")]
        DPEC = 4,
        [Description("Contingência FS-DA")]
        FSDA = 5,
        [Description("Contingência SVC-AN")]
        SVCAN = 6,
        [Description("Contingência SVC-RS")]
        SVCRS = 7
    }
}
