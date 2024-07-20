using RVEPP.Web.Frontend.Database;
using RVEPP.Web.Frontend.Database.Tables;
using RVEPP.Web.Frontend.Pages.Base;

namespace RVEPP.Web.Frontend.Pages
{
    public class IndexModel(RveppDbContext dbContext) : BasePageModel(dbContext)
    {
        public Content? LatestNews { get; private set; }

        public Content? LatestDownload { get; private set; }

        public void OnGet()
        {
            LatestNews = GetLatestContentByType(Enums.ContentTypes.News);

            LatestDownload = GetLatestContentByType(Enums.ContentTypes.Downloads);
        }
    }
}