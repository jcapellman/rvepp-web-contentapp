using Microsoft.AspNetCore.Mvc.RazorPages;
using RVEPP.Web.Frontend.Database;
using RVEPP.Web.Frontend.Database.Tables;

namespace RVEPP.Web.Frontend.Pages
{
    public class IndexModel(RveppDbContext dbContext) : PageModel
    {
        public Content? LatestNews { get; private set; }

        public void OnGet()
        {
            LatestNews = dbContext.Content.Where(a => a.ContentType == Enums.ContentTypes.News && a.Active).OrderByDescending(a => a.Created).FirstOrDefault();
        }
    }
}