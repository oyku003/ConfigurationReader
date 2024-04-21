using System.ComponentModel;

namespace ConfigurationReader.Services.Constants
{
    public enum DataTypes
    {
        [Description("System.Boolean")]
        Boolean,

        [Description("System.Int32")]
        Int,

        [Description("System.Double")]
        Double,

        [Description("System.String")]
        String
    }
}
