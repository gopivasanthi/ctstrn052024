using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Foundation.Navigation.Models;

namespace TRN.Foundation.Navigation.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        public ActionResult GetNavigationItems()
        {
            var StartPath = Sitecore.Context.Site.StartPath;
            var homeItem = Sitecore.Context.Database.GetItem(StartPath);
            var result = homeItem.Children?
                .Where(x => CheckForNavigableItem(x))
                .Select(x => new MainNavigation()
                {
                    NavItem = new NavigationItem
                    {
                        NavigationItemName = x.Name,
                        NavgiationItemUrl = LinkManager.GetItemUrl(x)
                    },
                    SubNavigations = GetSubNavigationItem(x) ?? new List<NavigationItem>()
                }).ToList();
            return View("/Views/TRN/Navigation/Navigation.cshtml", result);
        }
        private bool CheckForNavigableItem(Item item)
        {
            CheckboxField navigablePageField = item.Fields["IsNavigablePage"];
            if(navigablePageField != null)
            {
                return navigablePageField.Checked;
            }else
                return false;
        }
        private List<NavigationItem> GetSubNavigationItem(Item item)
        {
            return
                item.Children?
                    .Where(x => CheckForNavigableItem(x))
                    .Select(x => new NavigationItem()
                    {
                        NavgiationItemUrl = LinkManager.GetItemUrl(x),
                        NavigationItemName = x.Name
                    }).ToList();
        }
    }
}