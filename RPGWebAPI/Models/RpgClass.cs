using System.Text.Json.Serialization;

namespace RPGWebAPI.Models
{
    // [JsonConverter(typeof(JsonStringEnumConverter))] converts enum values to string.
    // This is used because the classes were showing up as integers(1, 2, 3) in schema.
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Mage = 2,
        Cleric = 3
    }
}
