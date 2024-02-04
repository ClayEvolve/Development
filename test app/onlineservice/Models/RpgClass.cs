using System.Text.Json.Serialization;

namespace onlineservice.Models
{
    [JsonConverter(typeof (JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Mage = 2
    }
}