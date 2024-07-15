using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using rvepp.web.frontend.Database;
using rvepp.web.frontend.Database.Tables;
using rvepp.web.frontend.Enums;
using rvepp.web.frontend.Objects.JSON;

namespace rvepp.web.frontend.Controllers
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
        public ActionResult NewContent(ContentRequestItem requestItem)
        {
            var newContent = new Content {
                Body = requestItem.Body,
                ContentType = requestItem.ContentType,
                Title = requestItem.Title,
                Active = true,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            dbContext.Content.Add(newContent);

            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPatch]
        public ActionResult UpdateContent(ContentRequestItem requestItem)
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
            existingContent.Modified = DateTime.Now;

            dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteContent(int id)
        {
            var existingContent = dbContext.Content.FirstOrDefault(a => a.Id == id && a.Active);

            if (existingContent is null)
            {
                return NotFound();
            }

            existingContent.Active = false;
            existingContent.Modified = DateTime.Now;

            dbContext.SaveChanges();

            return Ok();
        }
    }
}