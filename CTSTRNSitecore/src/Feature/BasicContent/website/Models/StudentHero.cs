using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRN.Feature.BasicContent.Models
{
    public class StudentHero
    {
        public string StudentHereImageUrl { get; set; }
        public HtmlString StudentName { get; set; }
        public HtmlString StudentDepartment { get; set; }
    }
}