using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RVEPP.Web.Frontend.Database;
using RVEPP.Web.Frontend.Database.Tables;
using RVEPP.Web.Frontend.Enums;
using RVEPP.Web.Frontend.Objects.JSON;

namespace RVEPP.Web.Frontend.Controllers
{
    [Authorize]
    [Route("api/content")]
    [ApiController]
    public class ContentController(RveppDbContext dbContext) : ControllerBase
    {
        [HttpGet]        
        [Route("{contentType}")]
        public List<Content> Get(ContentTypes contentType) => [.. dbContext.Content.Where(a => a.ContentType == contentType && a.Active)];

        [HttpPost]
        public async Task<ActionResult> NewContentAsync(ContentRequestItem requestItem)
        {
            var newContent = new Content {
                Body = requestItem.Body,
                ContentType = requestItem.ContentType,
                Title = requestItem.Title
            };

            dbContext.Content.Add(newContent);

            return await dbContext.SaveChangesAsync() > 0 ? Ok() : BadRequest("Failure saving to database");
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateContentAsync(ContentRequestItem requestItem)
        {
            if (!requestItem.Id.HasValue)
            {
                return NotFound();
            }

            var existingContent = dbContext.Content.FirstOrDefault(a => a.Id == requestItem.Id.Value && a.Active);

            if (existingContent is null)
            {
                return NotFound();
            }

            existingContent.Title = requestItem.Title;
            existingContent.ContentType = requestItem.ContentType;
            existingContent.Body = requestItem.Body;

            return await dbContext.SaveChangesAsync() > 0 ? Ok() : BadRequest("Failure saving to database");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteContentAsync(int id)
        {
            var existingContent = dbContext.Content.FirstOrDefault(a => a.Id == id && a.Active);

            if (existingContent is null)
            {
                return NotFound();
            }

            existingContent.Active = false;
            
            return await dbContext.SaveChangesAsync() > 0 ? Ok() : BadRequest("Failure saving to database");
        }
    }
}