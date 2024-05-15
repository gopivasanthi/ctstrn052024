using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Feature.BasicContent.Models;
using TRN.Foundation.Navigation.Models;

namespace TRN.Feature.BasicContent.Controllers
{
    public class BannerController : Controller
    {
        // GET: Banner
        public ActionResult GetBannerDetails()
        {
            var renderingItem = RenderingContext.Current.Rendering.Item;
            LinkField spoc = renderingItem.Fields["EventSpoc"];
            LinkField details = renderingItem.Fields["EventDetails"];
            Banner banner = new Banner()
            {
                BannerTitle = new HtmlString(FieldRenderer.Render(renderingItem, "BannerTitle")),
                BannerDesc = new HtmlString(FieldRenderer.Render(renderingItem, "BannerDesc")),
                BannerImage = new HtmlString(FieldRenderer.Render(renderingItem, "BannerImage")),
                Spoc = new NavigationItem
                {
                    NavgiationItemUrl = spoc.IsInternal ? LinkManager.GetItemUrl(spoc.TargetItem) : spoc.Url,
                    NavigationItemName = spoc.IsInternal ? spoc.TargetItem.DisplayName : spoc.Text
                },
                Details = new NavigationItem
                {
                    NavgiationItemUrl = details.IsInternal ? LinkManager.GetItemUrl(details.TargetItem) : details.Url,
                    NavigationItemName = details.IsInternal ? details.TargetItem.DisplayName : details.Text
                }
            };
            return View("/Views/TRN/BasicContent/Banner.cshtml", banner);
        }
    }
}