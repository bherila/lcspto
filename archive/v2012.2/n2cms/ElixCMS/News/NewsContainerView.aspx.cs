using System;
using N2.Collections;

namespace ElixCMS.News
{
    public partial class NewsContainerPage : N2.Templates.Web.UI.TemplatePage<NewsContainer>
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Response.Write(" P{" + CurrentItem.ID.ToString() + "}");

        }


    }
}