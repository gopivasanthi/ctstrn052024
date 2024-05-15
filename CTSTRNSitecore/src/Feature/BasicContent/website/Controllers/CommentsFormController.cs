using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Feature.BasicContent.Models;

namespace TRN.Feature.BasicContent.Controllers
{
    public class CommentsFormController : Controller
    {
        [HttpGet]
        public ActionResult GetCommentsForm()
        {
            var commentsForm = RenderingContext.Current.Rendering.Item;
            Comments comments = new Comments()
            {
                Name = "Name",
                Email = "Email",
                Comment = "Comment"
            };

            return View("/Views/TRN/BasicContent/CommentsForm.cshtml", comments);
        }
        [HttpPost]
        public ActionResult GetCommentsForm(Comments input)
        {
            var commentsForm = RenderingContext.Current.Rendering.Item;
            Comments comments = new Comments()
            {
                Name = input.Name,
                Email = input.Email,
                Comment = input.Comment
            };

            return View("/Views/TRN/BasicContent/CommentsConfirmation.cshtml", comments);
        }
    }
}