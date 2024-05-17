using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Feature.BasicContent.Models;

namespace TRN.Feature.BasicContent.Controllers
{
    public class CommentsListController : Controller
    {
        // GET: CommentsList
        public ActionResult GetAllComments()
        {
            var contextItem = Sitecore.Context.Item;
            if (contextItem.HasChildren)
            {
                var result = contextItem.Children?
                                    .Select(x => new Comments
                                    {
                                        ID = x.Fields["ID"].Value,
                                        Name = x.Fields["Name"].Value,
                                        Comment = x.Fields["Comment"].Value,
                                        Email = x.Fields["Email"].Value
                                    }).ToList();


                return View("/Views/TRN/BasicContent/CommentsList.cshtml", result);
            }
            else
            {
                return View("/Views/TRN/BasicContent/CommentsList.cshtml");
            }
        }
    }
}