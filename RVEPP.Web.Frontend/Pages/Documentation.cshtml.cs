using RVEPP.Web.Frontend.Database;
using RVEPP.Web.Frontend.Database.Tables;
using RVEPP.Web.Frontend.Pages.Base;

namespace RVEPP.Web.Frontend.Pages
{
    public class DocumentationModel(RveppDbContext dbContext) : BasePageModel(dbContext)
    {
        public Content? Documentation { get; private set; }

        public void OnGet()
        {
            Documentation = GetLatestContentByType(Enums.ContentTypes.Documentation);
        }
    }
}