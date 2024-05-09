using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Feature.BasicContent.Models;

namespace TRN.Feature.BasicContent.Controllers
{
    public class StudentsListController : Controller
    {
        // GET: StudentsList
        public ActionResult GetListofStudents()
        {

            var contextItem = Sitecore.Context.Item;
            string StudentsParentItemIdinSitecore = "{103D2D86-CB68-4EC3-8481-45290E626504}";
            var studentsParentItem = Sitecore.Context.Database.GetItem(StudentsParentItemIdinSitecore);
            List<StudentListItem> studentsList = studentsParentItem.GetChildren()
                                .Where(x => x.Fields["StudentDepartment"].Value == contextItem.ID.ToString())
                                .Select(x => new StudentListItem
                                {
                                    StudentName = x.Fields["StudentName"].Value,
                                    StudentProfilePageUrl = LinkManager.GetItemUrl(x),
                                    StudentProfilePicAlt = GetProfilePicAlt(x, "StudentProfilePic"),
                                    StudentProfilePicUrl = GetProfilePicUrl(x, "StudentProfilePic")
                                }).ToList();
            return View("/Views/TRN/BasicContent/ListofStudents.cshtml", studentsList);
        }

        private string GetProfilePicAlt(Item item, string FieldName)
        {
            ImageField image = item.Fields[FieldName];
            return image.Alt;
        }
        private string GetProfilePicUrl(Item item, string FieldName)
        {
            ImageField image = item.Fields[FieldName];
            return MediaManager.GetMediaUrl(image.MediaItem);
        }
    }
}