using RVEPP.Web.Frontend.Database;
using RVEPP.Web.Frontend.Database.Tables;
using RVEPP.Web.Frontend.Pages.Base;

namespace RVEPP.Web.Frontend.Pages
{
    public class DownloadModel(RveppDbContext dbContext) : BasePageModel(dbContext)
    {
        public List<Content>? Downloads { get; private set; }

        public void OnGet()
        {
            Downloads = [.. dbContext.Content.Where(a => a.ContentType == Enums.ContentTypes.Downloads && a.Active).OrderByDescending(a => a.Modified)];
        }
    }
}