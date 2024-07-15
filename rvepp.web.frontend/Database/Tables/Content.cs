using rvepp.web.frontend.Database.Tables.Base;
using rvepp.web.frontend.Enums;

namespace rvepp.web.frontend.Database.Tables
{
    public class Content : BaseTable
    {
        public required string Title { get; set; }

        public required ContentTypes ContentType { get; set; }

        public required string Body { get; set; }
    }
}