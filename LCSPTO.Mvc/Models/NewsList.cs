/*
 *  Copyright (C) 2012 Benjamin Herila, http://bherila.net
 *  Distributed under the Boost Software License, Version 1.0, available at http://www.boost.org/LICENSE_1_0.txt
 */

using N2;
using N2.Collections;
using N2.Details;
using N2.Integrity;
using N2.Templates;
using System.Collections.Generic;
using Dinamico.Models;

namespace LCSPTO.Mvc
{
    public enum SortMode
    {
        Descending = 0,
        Ascending = 1
    }

    public enum DisplayMode
    {
        TitleLinkOnly = 0, 
        TitleAndAbstract = 1, 
        TitleAndText = 2,
    }

    [PartDefinition("News Container Link")]
    [RestrictParents(typeof(NewsList), typeof(FullCalendar))]
    public class NewsListContainerLink : ContentItem, N2.Definitions.IPart
    {
        [EditableLink("News container", 100, SelectableTypes = new System.Type[] { typeof(ContentPage) })]
        public virtual ContentPage Container
        {
            get { return (ContentPage)GetDetail("Container"); }
            set { SetDetail("Container", value); }
        }

    }

	//[N2.PartDefinition("News List", 
	//	Description = "A news list box that can be displayed in a column.", 
	//	SortOrder = 160,
	//	IconUrl = "~/N2/resources/icons/newspaper_go.png")]
	//[WithEditableTitle("Title", 10, Required = false)]
	//[AvailableZone("Sources", "Sources")]
	//[RestrictChildren(typeof(NewsListContainerLink))]
    public class NewsList : PartModelBase
    {
        public override string TemplateKey
        {
            get
            {
                return "NewsList";
            }
            set
            {
                base.TemplateKey = "NewsList";
            }
        }

        public enum HeadingLevel
        {
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4
        }

        [EditableEnum("Title heading level", 90, typeof(HeadingLevel))]
        public virtual int TitleLevel
        {
            get { return (int)(GetDetail("TitleLevel") ?? 3); }
            set { SetDetail("TitleLevel", value, 3); }
        }

        [EditableChildren("News container", "Sources", 100)]
        public virtual IList<NewsListContainerLink> Containers
        {
            get { return GetChildren().Cast<NewsListContainerLink>(); }
        }

		[EditableNumber("Max news", 120)]
        public virtual int MaxNews
        {
            get { return (int) (GetDetail("MaxNews") ?? 3); }
            set { SetDetail("MaxNews", value, 3); }
        }

        public virtual void Filter(ItemList items)
        {
            TypeFilter.Filter(items, typeof (ContentPage));
            CountFilter.Filter(items, 0, MaxNews);
        }

        [EditableEnum(
            Title = "Display mode",
            SortOrder = 150,
            EnumType = typeof(DisplayMode))
        ]
        public virtual DisplayMode DisplayMode
        {
            get { return (DisplayMode)(GetDetail("DisplayMode") ?? DisplayMode.TitleAndAbstract); }
            set { SetDetail("DisplayMode", (int)value, (int)DisplayMode.TitleAndAbstract); }
        }

        [EditableEnum(
            Title = "Sort mode", 
            SortOrder = 200, 
            EnumType = typeof(SortMode))
        ]
        public virtual SortMode SortByDate
        {
            get { return (SortMode)(GetDetail("SortByDate") ?? SortMode.Descending); }
            set { SetDetail("SortByDate", (int)value, (int)SortMode.Descending); }
        }

        [EditableCheckBox("Group by month", 250)]
        public virtual bool GroupByMonth
        {
            get { return (bool)(GetDetail("GroupByMonth") ?? true); }
            set { SetDetail("GroupByMonth", value, true); }
        }

        [EditableCheckBox("Show Past Items", 500, CheckBoxText = "Show Past Items")]
        public virtual bool ShowPastEvents
        {
            get { return (bool)(GetDetail("ShowPastEvents") ?? true); }
            set { SetDetail("ShowPastEvents", value, true); }
        }

        [EditableCheckBox("Show Future Items", 501, CheckBoxText = "Show Future Items")]
        public virtual bool ShowFutureEvents
        {
            get { return (bool)(GetDetail("ShowFutureEvents") ?? false); }
            set { SetDetail("ShowFutureEvents", value, false); }
        }

    }
}