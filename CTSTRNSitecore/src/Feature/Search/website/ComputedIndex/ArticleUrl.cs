using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRN.Feature.Search.ComputedIndex
{
    public class ArticleUrl : AbstractComputedIndexField
    {
        public override object ComputeFieldValue(IIndexable indexable)
        {
            ItemUrlBuilderOptions options = new ItemUrlBuilderOptions()
            {
                AlwaysIncludeServerUrl = false,
                LowercaseUrls = true,
            };
            var indexableItem = ((SitecoreIndexableItem)indexable).Item;
            if(indexableItem != null)
            {
                if (indexableItem.TemplateName == "Article")
                {
                    var resultUrl = LinkManager.GetItemUrl(indexableItem, options);
                    string[] seperator = new string[] { "home" };
                    var subStr = resultUrl.Split(seperator, StringSplitOptions.None)[1];
                    return subStr;
                }
                else
                    return "noarticleurlfound";
            }else
                return "noarticleurlfound";
        }
    }
}