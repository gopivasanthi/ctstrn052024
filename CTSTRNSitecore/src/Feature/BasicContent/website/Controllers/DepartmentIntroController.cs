using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TRN.Feature.BasicContent.Models;

namespace TRN.Feature.BasicContent.Controllers
{
    public class DepartmentIntroController : Controller
    {
        // GET: DepartmentIntro
        public ActionResult IntroDetails()
        {
            var contextItem = Sitecore.Context.Item;

            //Sitecore.Data.Fields.ImageField titleBackground = contextItem.Fields["TitleBackground"];
            Department dept = new Department
            {
                Title = new HtmlString(FieldRenderer.Render(contextItem, "Title")),
                DepartmentEthos = new HtmlString(FieldRenderer.Render(contextItem, "DepartmentEthos")),
                TitleBackground = new HtmlString(FieldRenderer.Render(contextItem, "TitleBackground")),
                DepartmentLogo = new HtmlString(FieldRenderer.Render(contextItem, "DepartmentLogo")),
            };

            return View("/Views/TRN/BasicContent/DepartmentIntro.cshtml", dept);
        }
    }
}