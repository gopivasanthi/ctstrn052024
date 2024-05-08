using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Feature.BasicContent.Models;

namespace TRN.Feature.BasicContent.Controllers
{
    public class StudentHeroController : Controller
    {
        // GET: StudentHero
        public ActionResult HeroBanner()
        {
            var contextItem = Sitecore.Context.Item;
            ImageField heroImage = contextItem.Fields["StudentHeroImage"];
            var heroImageItem = heroImage.MediaItem;
            string heroImageUrl = string.Empty;
            if(heroImageItem != null)
            {
                heroImageUrl = MediaManager.GetMediaUrl(heroImageItem);
            }
            LinkField studentDepartment = contextItem.Fields["StudentDepartment"];
            var studentDepartmentstr = studentDepartment.Value;
            ID studentDepartmentId = new ID(studentDepartmentstr);
            var studentDepartmentItem = Sitecore.Context.Database.GetItem(studentDepartmentId);

            StudentHero studentHero = new StudentHero
            {
                StudentName = new HtmlString(FieldRenderer.Render(contextItem, "StudentName")),
                StudentHereImageUrl = heroImageUrl,
                StudentDepartment = studentDepartmentItem == null ? new HtmlString("") : new HtmlString(FieldRenderer.Render(studentDepartmentItem, "Title"))
            };

            return View("/Views/TRN/BasicContent/StudentHeroBanner.cshtml", studentHero);
        }
    }
}