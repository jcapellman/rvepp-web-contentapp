using Microsoft.AspNetCore.Mvc.RazorPages;

using RVEPP.Web.Frontend.Database;
using RVEPP.Web.Frontend.Database.Tables;

namespace RVEPP.Web.Frontend.Pages
{
    public class IndexModel(RveppDbContext dbContext) : PageModel
    {
        public Content? LatestNews { get; private set; }

        public Content? LatestDownload { get; private set; }

        private Content? GetLatestContentByType(Enums.ContentTypes type) =>
            dbContext.Content.Where(a => a.ContentType == type && a.Active).OrderByDescending(a => a.Created).FirstOrDefault();

        public void OnGet()
        {
            LatestNews = GetLatestContentByType(Enums.ContentTypes.News);

            LatestDownload = GetLatestContentByType(Enums.ContentTypes.Downloads);
        }
    }
}