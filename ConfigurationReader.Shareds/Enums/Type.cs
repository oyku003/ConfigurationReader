using System.ComponentModel;

namespace ConfigurationReader.Shared.Enums
{
    public enum Type
    {
        [Description("Int")]
        Int,
        [Description("String")]
        String,
        [Description("Boolean")]
        Boolean,
        [Description("Double")]
        Double
    }
}
