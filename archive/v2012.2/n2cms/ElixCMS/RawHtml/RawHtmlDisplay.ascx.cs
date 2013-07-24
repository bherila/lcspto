// Type: N2.Addons.RawHtml.RawHtmlDisplay
// Assembly: N2.Templates, Version=2.2.1.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Users\benh\website-backups\N2.Templates.dll

using N2;
using N2.Templates.Web.UI;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Addons.RawHtml
{
    public class RawHtmlDisplay : TemplateUserControl<ContentItem, RawHtmlItem>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal literal = new Literal()
            {
                Text = this.CurrentItem.Text
            };
            if (this.CurrentItem.Placement == HtmlPlace.HtmlHead)
            {
                this.Page.Header.Controls.Add((Control)literal);
                if (!(this.Request["edit"] == "drag"))
                    return;
                this.Controls.Add((Control)new Literal()
                {
                    Text = "{ HEAD content }"
                });
            }
            else
                this.Controls.Add((Control)literal);
        }
    }
}
