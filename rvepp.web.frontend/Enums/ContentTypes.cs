using System.Text.Json.Serialization;

namespace rvepp.web.frontend.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContentTypes
    {
        News = 1,
        Downloads = 2
    }
}