using Dinamico.Models;
using N2.Collections;
using N2.Details;
using N2.Integrity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCSPTO.Mvc
{
    [N2.PartDefinition("Calendar",
        Description = "A jquery FullCalendar control populated with events from one or more news sources.",
        SortOrder = 165,
        IconUrl = "~/N2/resources/icons/calendar.png")]
    [WithEditableTitle("Title", 10, Required = false)]
    [AvailableZone("Sources", "Sources")]
    [RestrictChildren(typeof(NewsListContainerLink))]
    public class FullCalendar : PartModelBase
    {
        public override string TemplateKey
        {
            get { return "NewsList"; }
            set { base.TemplateKey = "NewsList"; }
        }

        [EditableChildren("News container", "Sources", 100)]
        public virtual IList<NewsListContainerLink> Containers
        {
            get { return GetChildren().Cast<NewsListContainerLink>(); }
        }

        public virtual void Filter(ItemList items)
        {
            TypeFilter.Filter(items, typeof(ContentPage));
        }

    }
}