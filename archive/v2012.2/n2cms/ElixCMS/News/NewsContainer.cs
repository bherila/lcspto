using N2.Integrity;
using N2.Web;
using N2.Definitions;
using N2;

namespace ElixCMS.News
{
    [PageDefinition("Elix News Container", 
		Description = "A list of news. News items can be added to this page.",
		SortOrder = 150,
		IconUrl = "~/Templates/UI/Img/newspaper_link.png")]
    [RestrictParents(typeof (IStructuralPage))]
	[SortChildren(SortBy.PublishedDescending)]
    //[Template("~/ElixCMS/News/NewsContainerView.aspx")]
    //[ConventionTemplate("NewsList")]
    public class NewsContainer : N2.Templates.Items.TextPage
    {
    }
}