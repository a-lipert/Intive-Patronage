using System.Text.Json.Serialization;

namespace Intive.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BookOrderBy
    {
        Title = 0,
        AuthorName = 1,
        Rating = 2,
    }
}
