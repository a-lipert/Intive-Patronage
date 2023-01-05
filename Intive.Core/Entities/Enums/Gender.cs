using System.Text.Json.Serialization;

namespace Intive.Core.Entities.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male,
        Female
    }
}
