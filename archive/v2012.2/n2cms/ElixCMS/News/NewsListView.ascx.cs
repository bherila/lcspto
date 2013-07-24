using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace ElixCMS.News
{
    public partial class NewsListView : N2.Templates.Web.UI.TemplateUserControl<N2.ContentItem, NewsList>
    {

        protected void appendDate(StringBuilder sb, DateTime date)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            literal.Text = GetHtml(this);
        }

        protected static string GetHtml(N2.Templates.Web.UI.TemplateUserControl<N2.ContentItem, NewsList> self)
        {
            if (self.CurrentItem == null)
                return string.Empty;

            List<News> allNews = new List<News>();
            {
                foreach (var containerLink in self.CurrentItem.Containers)
                {
                    var container = containerLink.Container as NewsContainer;
                    foreach (var newsItem in container.GetChildren<News>())
                        if (newsItem.Published.HasValue) //TODO: Show unpublished news (e.g. calendar items)
                            allNews.Add(newsItem);
                }
            }

            IEnumerable<News> newsEnumerable = allNews;
            {
                // apply sort order ***
                if (self.CurrentItem.SortByDate == SortMode.Ascending)
                    newsEnumerable = allNews.OrderBy(a => a.Published.Value);
                else
                    newsEnumerable = allNews.OrderByDescending(a => a.Published.Value);

                // apply filter ***
                if (self.CurrentItem.MaxNews > 0)
                    newsEnumerable = newsEnumerable.Take(self.CurrentItem.MaxNews);
            }

            DateTime? lastDate = null;
            StringBuilder sb = new StringBuilder(50 * 1024);
            foreach (News item in newsEnumerable)
            {
                if (self.CurrentItem.GroupByMonth && (lastDate == null || lastDate.Value.Month != item.Published.Value.Month))
                {
                    // new month ***
                    sb.AppendFormat("<h2>{0:MMMM yyyy}</h2>\n", item.Published.Value);
                    lastDate = item.Published.Value;
                }

                // display either full article or abstract + link ***
                switch (self.CurrentItem.DisplayMode)
                {
                    case DisplayMode.TitleAndText:
                        if (!String.IsNullOrEmpty(item.Title) && item.ShowTitle)
                            sb.AppendFormat("<h3>{0}</h3>\n", item.Title);
                        sb.AppendFormat("<div class=\"date\">{0}</div>\n", item.Published.Value);
                        sb.AppendFormat("<div class=\"article\">\n{0}\n</div>\n", item.Text);
                        break;

                    case DisplayMode.TitleAndAbstract:
                        if (!String.IsNullOrEmpty(item.Text))
                        {
                            if (!String.IsNullOrEmpty(item.Title) && item.ShowTitle)
                                sb.AppendFormat("<h3><a href=\"{1}\">{0}</a></h3>\n", item.Title, item.Url);
                            sb.AppendFormat("<div class=\"date\">{0}</div>\n", item.Published.Value);
                            sb.AppendFormat("<div class=\"abstract\">\n{0}\n</div>\n", item.Introduction);
                            sb.AppendFormat("<a href=\"{0}\">Read more...</a>\n", item.Url);
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(item.Title) && item.ShowTitle)
                                sb.AppendFormat("<h3>{0}</h3>\n", item.Title);
                            sb.AppendFormat("<div class=\"date\">{0}</div>\n", item.Published.Value);
                            sb.AppendFormat("<div class=\"abstract\">\n{0}\n</div>\n", item.Introduction);
                        }
                        break;

                    case DisplayMode.TitleLinkOnly:
                        sb.AppendFormat("<h3><a href=\"{1}\">{0}</a></h3>\n", item.Title ?? "Untitled", item.Url);
                        sb.AppendFormat("<div class=\"date\">{0}</div>\n", item.Published.Value);

                        break;
                }
            }

            return sb.ToString();
        }

    }
}