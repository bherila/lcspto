using N2.Collections;
using N2.Details;
using N2.Integrity;
using N2.Templates.Items;
using N2.Templates;
using System.Collections.Generic;
using N2;

namespace ElixCMS.News
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
    [RestrictParents(typeof(NewsList))]
    public class NewsListContainerLink : ContentItem, N2.Definitions.IPart
    {
        [EditableLink("News container", 100, SelectableTypes = new System.Type[] { typeof(NewsContainer) })]
        public virtual NewsContainer Container
        {
            get { return (NewsContainer)GetDetail("Container"); }
            set { SetDetail("Container", value); }
        }

    }

    [N2.PartDefinition("Elix News List", 
		Description = "A news list box that can be displayed in a column.", 
		SortOrder = 160,
		IconUrl = "~/Templates/UI/Img/newspaper_go.png")]
    [WithEditableTitle("Title", 10, Required = false)]
    [AllowedZones(Zones.RecursiveRight, Zones.RecursiveLeft, Zones.Right, Zones.Left, Zones.Content, Zones.ColumnLeft, Zones.ColumnRight)]
    [AvailableZone("Sources", "Sources")]
    [RestrictChildren(typeof(NewsListContainerLink))]
    public class NewsList : SidebarItem
    {
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
            TypeFilter.Filter(items, typeof (News));
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

        public override string TemplateUrl
        {
            get
            {
                return "~/ElixCMS/News/NewsListView.ascx";
            }
        }
    }
}