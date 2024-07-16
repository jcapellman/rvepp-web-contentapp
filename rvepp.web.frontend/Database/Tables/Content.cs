using RVEPP.Web.Frontend.Database.Tables.Base;
using RVEPP.Web.Frontend.Enums;

namespace RVEPP.Web.Frontend.Database.Tables
{
    public class Content : BaseTable
    {
        public required string Title { get; set; }

        public required ContentTypes ContentType { get; set; }

        public required string Body { get; set; }
    }
}