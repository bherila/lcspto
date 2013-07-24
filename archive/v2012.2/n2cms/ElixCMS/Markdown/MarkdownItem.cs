// Type: N2.Templates.Items.MarkdownItem
// Assembly: N2.Templates, Version=2.2.1.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Users\benh\website-backups\N2.Templates.dll

using N2;
using N2.Details;
using N2.Integrity;
using N2.MarkDown;

namespace N2.Templates.Items
{
    [PartDefinition("Markdown", IconUrl = "~/Templates/UI/Img/text_align_left.png", Name = "Markdown")]
    [AllowedZones(AllowedZones.AllNamed)]
    public class MarkdownItem : AbstractItem
    {
        [WmdEditor("Text", 100)]
        public virtual string Text
        {
            get
            {
                return (string)(this.GetDetail("Text") ?? (object)string.Empty);
            }
            set
            {
                this.SetDetail<string>("Text", value, string.Empty);
            }
        }

        [DisplayableLiteral(Name = "ParsedText")]
        public string ParsedText
        {
            get
            {
                return new MarkdownParser().Transform(this.Text);
            }
        }

        protected override string TemplateName
        {
            get
            {
                return "Markdown";
            }
        }

        public override string TemplateUrl
        {
            get
            {
                return "~/ElixCMS/Markdown/Markdown.ascx";
            }
        }
    }
}
