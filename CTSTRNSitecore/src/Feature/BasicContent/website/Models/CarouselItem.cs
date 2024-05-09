using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRN.Feature.BasicContent.Models
{
    public class CarouselItem
    {
        public string CarouselTitle { get; set; }
        public string CarouselImageUrl { get; set; }
        public string CarouselImageAlt { get; set; }
        public string CarouselDescription { get; set; }
        public string IsActive { get; set; } = "";
    }
}