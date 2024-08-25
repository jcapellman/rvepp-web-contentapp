using RVEPP.Web.Frontend.Database;
using RVEPP.Web.Frontend.Database.Tables;
using RVEPP.Web.Frontend.Pages.Base;

namespace RVEPP.Web.Frontend.Pages
{
    public class AboutModel(RveppDbContext dbContext) : BasePageModel(dbContext)
    {
        public Content? AboutContent { get; private set; }

        public void OnGet()
        {
            AboutContent = GetLatestContentByType(Enums.ContentTypes.About);
        }
    }
}