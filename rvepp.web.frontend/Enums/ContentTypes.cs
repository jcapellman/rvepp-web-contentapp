using System.Text.Json.Serialization;

namespace RVEPP.Web.Frontend.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContentTypes
    {
        News = 1,
        Downloads = 2
    }
}