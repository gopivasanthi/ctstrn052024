using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRN.Feature.BasicContent.Models
{
    public class Banner
    {
        public HtmlString BannerTitle { get; set; }
        public HtmlString BannerImage { get; set; }
        public HtmlString BannerDesc { get; set; }
    }
}