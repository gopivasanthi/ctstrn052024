using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRN.Feature.Search.ComputedIndex
{
    public class TitleBackgroundUrl : AbstractComputedIndexField
    {
        public override object ComputeFieldValue(IIndexable indexable)
        {
            var indexableItem = ((SitecoreIndexableItem)indexable).Item;
            if (indexableItem != null)
            {
                if (indexableItem.TemplateName == "Article")
                {
                    ImageField titleBackgroundField = indexableItem.Fields["TitleBackground"];
                    if (titleBackgroundField != null)
                    {
                        if (titleBackgroundField.MediaItem != null)
                            return MediaManager.GetMediaUrl(titleBackgroundField.MediaItem);
                        else
                            return "noarticleimagefound";
                    }
                    else
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