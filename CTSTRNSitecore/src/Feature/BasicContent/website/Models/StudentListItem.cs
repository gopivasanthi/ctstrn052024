using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRN.Feature.BasicContent.Models
{
    public class StudentListItem
    {
        public string StudentName { get; set; }
        public string StudentProfilePageUrl { get; set; }
        public string StudentProfilePicUrl { get; set; }
        public string StudentProfilePicAlt { get; set; }
    }
}