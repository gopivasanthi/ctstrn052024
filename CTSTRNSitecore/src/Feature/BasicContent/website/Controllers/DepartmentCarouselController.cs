using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Feature.BasicContent.Models;

namespace TRN.Feature.BasicContent.Controllers
{
    public class DepartmentCarouselController : Controller
    {
        // GET: DepartmentCarousel
        public ActionResult GetCarouselItems()
        {
            var contextItem = Sitecore.Context.Item;

            MultilistField carouselItemFromSitecore = contextItem.Fields["CarouselList"];
            List<CarouselItem> carouselItems = carouselItemFromSitecore.GetItems()
                                    .Select(x => new CarouselItem
                                    {
                                        CarouselTitle = x.Fields["CarouselTitle"].Value,
                                        CarouselDescription = x.Fields["CarouselDescription"].Value,
                                        CarouselImageUrl = GetCarouselImageUrl(x, "CarouselImage"),
                                        CarouselImageAlt = GetCarouselImageAlt(x, "CarouselImage"),
                                    }).ToList();

            carouselItems[0].IsActive = "active";

            return View(carouselItems);
        }

        private string GetCarouselImageUrl(Item item, string FieldName)
        {
            ImageField imageField = item.Fields[FieldName];
            return MediaManager.GetMediaUrl(imageField.MediaItem);
        }
        private string GetCarouselImageAlt(Item item, string FieldName)
        {
            ImageField imageField = item.Fields[FieldName];
            return imageField.Alt;
        }
    }
}