using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRN.Feature.Search.Models
{
    public class ArticleSearchResultItem
    {
        public string ResultTitle { get; set; }
        public string ResultBrief { get; set; }
        public string ResultImageUrl { get; set; }
        public string ResultImageAlt { get; set; }
        public string ResultSourceLink { get; set; }
    }
}