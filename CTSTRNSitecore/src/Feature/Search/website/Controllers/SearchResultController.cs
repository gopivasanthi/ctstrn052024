using Polly.Caching;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRN.Feature.Search.Models;

namespace TRN.Feature.Search.Controllers
{
    public class SearchResultController : Controller
    {
        // GET: SearchResult
        public ActionResult GetSearchResult()
        {
            var searchTerm = Request.QueryString["SearchTerm"];
            var contextDbName = Sitecore.Context.Database.Name;

            //get the index
            var indexName = $"sitecore_{contextDbName}_index";

            var indexForSearch = ContentSearchManager.GetIndex(indexName);
            //create the search context
            using (var context = indexForSearch.CreateSearchContext())
            {
                //perform the search
                var result = context.GetQueryable<SearchResultItem>()
                                    .Where(x => x.TemplateName == "Article")
                                    .Where(x => x.Language == Sitecore.Context.Language.Name)
                                    .Where(x => x.Content.Contains(searchTerm))
                                    .GetResults()
                                    .Select(x => new ArticleSearchResultItem()
                                    {
                                        ResultTitle = ReadFieldValueFromSearchProvider(x, "articletitle"),
                                        ResultBrief = ReadFieldValueFromSearchProvider(x, "title"),
                                        ResultImageAlt = ReadFieldValueFromSearchProvider(x, "articleimage"),
                                        ResultImageUrl = ReadFieldValueFromSearchProvider(x, "trnarticleimageurl"),
                                        ResultSourceLink = ReadFieldValueFromSearchProvider(x, "trnarticleurl"),
                                    }).ToList();
                return View("/Views/TRN/Search/SearchResult.cshtml", result);
            }
        }

        private string ReadFieldValueFromSearchProvider(SearchHit<SearchResultItem> resultItem, string fieldName)
        {
            if (resultItem == null)
                return "";
            if (resultItem.Document is null)
                return "";
            if (resultItem.Document.Fields.ContainsKey(fieldName))
            {
                var result = resultItem.Document.Fields[fieldName];
                if (result is null)
                    return "";
                else
                {
                    var resultStr = Convert.ToString(resultItem.Document.Fields[fieldName]);
                    if (resultStr.Contains("sitecore/shell/"))
                    {
                        var res = resultStr.Replace("/sitecore/shell/", "");
                        return res;
                    }
                    else
                    {
                        return resultStr;
                    }
                }
            }
            else
                return "";

        }
    }
}