using System.ComponentModel;

namespace ConfigurationReader.Enums
{
    public enum TypeEnum
    {
        [Description("System.String")]
        String=0,
        [Description("System.Int32")]
        Int =1,
        [Description("System.Boolean")]
        Boolean=2,
        [Description("System.Double")]
        Double =3
    }
}
