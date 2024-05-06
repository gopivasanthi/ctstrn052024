using Sitecore.Data.Fields;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Feature.BasicContent.Models;

namespace TRN.Feature.BasicContent.Controllers
{
    public class StudentDeptController : Controller
    {
        // GET: StudentDept
        public ActionResult DepartmentDetails()
        {
            var contextItem = Sitecore.Context.Item;
            LinkField DepartmentMapped = contextItem.Fields["StudentDepartment"];

            var linkedDepartmentId = DepartmentMapped.Value;

            var linkedDepartmentItem = Sitecore.Context.Database.GetItem(linkedDepartmentId);

            Dept dept = new Dept
            {
                DeptName = linkedDepartmentItem.Fields["Title"].Value,
                DeptUrl = LinkManager.GetItemUrl(linkedDepartmentItem)
            };

            return View("/Views/TRN/BasicContent/DeptLink.cshtml", dept);
        }
    }
}