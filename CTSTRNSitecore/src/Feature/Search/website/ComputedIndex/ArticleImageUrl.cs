using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRN.Feature.Search.ComputedIndex
{
    public class ArticleImageUrl : AbstractComputedIndexField
    {
        public override object ComputeFieldValue(IIndexable indexable)
        {
            var indexableItem = ((SitecoreIndexableItem)indexable).Item;
            if (indexableItem != null)
            {
                if (indexableItem.TemplateName == "Article")
                {
                    ImageField articleImageField = indexableItem.Fields["ArticleImage"];
                    if (articleImageField != null)
                    {
                        if (articleImageField.MediaItem != null)
                            return MediaManager.GetMediaUrl(articleImageField.MediaItem);
                        else
                            return "noarticleimagefound";
                    }else
                        return "noarticleimagefound";

                }
                else
                    return "noarticleimagefound";
            }
            else
                return "noarticleimagefound";
        }
    }
}