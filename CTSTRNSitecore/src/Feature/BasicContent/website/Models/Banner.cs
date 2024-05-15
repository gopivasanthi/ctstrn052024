using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TRN.Foundation.Navigation.Models;

namespace TRN.Feature.BasicContent.Models
{
    public class Banner
    {
        public HtmlString BannerTitle { get; set; }
        public HtmlString BannerImage { get; set; }
        public HtmlString BannerDesc { get; set; }
        public NavigationItem Spoc { get; set; }
        public NavigationItem Details { get; set; }
    }
}