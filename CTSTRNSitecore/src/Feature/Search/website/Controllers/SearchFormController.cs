using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Feature.Search.Models;

namespace TRN.Feature.Search.Controllers
{
    public class SearchFormController : Controller
    {
        [HttpGet]
        public ActionResult GetSearchForm()
        {
            var renderingItem = RenderingContext.Current.Rendering.Item;

            SearchViewModel viewModel = new SearchViewModel();
            if(renderingItem != null)
            {
                viewModel.SearchTextBoxPlaceholder = renderingItem.Fields["SeachTextPlaceholder"].Value;
                viewModel.SearchButtonLabel = renderingItem.Fields["SearchButtonLabel"].Value;
                viewModel.SearchTerm = "SearchTerm";
            }
            return View("/Views/TRN/Search/Search.cshtml", viewModel);
        }
        [HttpPost]
        public ActionResult GetSearchForm(string SearchTerm)
        {
            var startPath = Sitecore.Context.Site.StartPath;
            var homeItem = Sitecore.Context.Database.GetItem(startPath);

            if (homeItem != null && !string.IsNullOrEmpty(SearchTerm))
            {
                var searchPage = homeItem.Children?.FirstOrDefault(x => x.TemplateName == "SearchPage");
                var searchUrl = LinkManager.GetItemUrl(searchPage);
                return Redirect(searchUrl + $"/?SearchTerm={SearchTerm}");
            }
            else
            {
                var renderingItem = RenderingContext.Current.Rendering.Item;

                SearchViewModel viewModel = new SearchViewModel();
                if (renderingItem != null)
                {
                    viewModel.SearchTextBoxPlaceholder = renderingItem.Fields["SeachTextPlaceholder"].Value;
                    viewModel.SearchButtonLabel = renderingItem.Fields["SearchButtonLabel"].Value;
                    viewModel.SearchTerm = "SearchTerm";
                }
                return View("/Views/TRN/Search/Search.cshtml", viewModel);
            }
        }

    }
}