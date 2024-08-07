using RVEPP.Web.Frontend.Enums;

namespace RVEPP.Web.Frontend.Objects.JSON
{
    public class ContentRequestItem
    {
        public int? Id { get; set; }

        public required ContentTypes ContentType { get; set; }

        public required string Title { get; set; }

        public required string Body { get; set; }
    }
}