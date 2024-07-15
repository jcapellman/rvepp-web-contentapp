using rvepp.web.frontend.Enums;

namespace rvepp.web.frontend.Objects.JSON
{
    public class ContentRequestItem
    {
        public int? Id { get; set; }

        public ContentTypes ContentType { get; set; }

        public required string Title { get; set; }

        public required string Body { get; set; }
    }
}