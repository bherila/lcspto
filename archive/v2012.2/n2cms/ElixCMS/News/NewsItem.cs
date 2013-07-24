using System.Web.UI.WebControls;
using N2.Definitions;
using N2.Details;
using N2.Integrity;
using N2.Web;
using N2.Persistence;
using N2;

namespace ElixCMS.News
{
    [PageDefinition("News", Description = "A news page.", SortOrder = 155,
		IconUrl = "~/Templates/UI/Img/newspaper.png")]
    [RestrictParents(typeof (NewsContainer))]
	[Template("~/ElixCMS/News/NewsItemTemplate.aspx")]
    public class News : N2.Templates.Items.AbstractContentPage, ISyndicatable
    {
        public News()
        {
            Visible = false;
        }

        public override void AddTo(ContentItem newParent)
        {
            Utility.Insert(this, newParent, "Published DESC");
        }

        [EditableText("Introduction", 90, ContainerName = N2.Templates.Tabs.Content, TextMode = TextBoxMode.MultiLine, Rows = 4, Columns = 80)]
        public virtual string Introduction
        {
            get { return (string) (GetDetail("Introduction") ?? string.Empty); }
            set { SetDetail("Introduction", value, string.Empty); }
        }

        string ISyndicatable.Summary
        {
            get { return Introduction; }
        }

		[Persistable(PersistAs = PropertyPersistenceLocation.Detail)]
		public virtual bool Syndicate { get; set; }
    }
}