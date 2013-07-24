// Type: N2.Addons.RawHtml.RawHtmlItem
// Assembly: N2.Templates, Version=2.2.1.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Users\benh\website-backups\N2.Templates.dll

using N2;
using N2.Details;
using N2.Integrity;
using N2.Templates.Items;
using System.Web.UI.WebControls;

namespace N2.Addons.RawHtml
{
    public enum HtmlPlace
    {
        InPlace,
        HtmlHead,
    }

    [PartDefinition("Raw HTML", IconUrl = "~/Templates/UI/Img/text_align_left.png", Name = "RawHTML")]
    [AllowedZones(AllowedZones.AllNamed)]
    public class RawHtmlItem : AbstractItem
    {
        [EditableTextBox("Text", 1, TextMode = TextBoxMode.MultiLine)]
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

        [EditableEnum(typeof(HtmlPlace))]
        public virtual HtmlPlace Placement
        {
            get
            {
                return (HtmlPlace)(this.GetDetail("Placement") ?? (object)HtmlPlace.InPlace);
            }
            set
            {
                this.SetDetail<HtmlPlace>("Placement", value, HtmlPlace.InPlace);
            }
        }

        public override string TemplateUrl
        {
            get
            {
                return "~/ElixShared/RawHtml/RawHtmlDisplay.ascx";
            }
        }
    }
}
