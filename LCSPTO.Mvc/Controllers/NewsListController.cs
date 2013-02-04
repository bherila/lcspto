/*
 *  Copyright (C) 2012 Benjamin Herila, http://bherila.net
 *  Distributed under the Boost Software License, Version 1.0, available at http://www.boost.org/LICENSE_1_0.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2.Web;
using N2.Engine;
using N2.Web.Mvc;
using Dinamico.Models;

namespace LCSPTO.Mvc.Controllers
{
    [Controls(typeof(NewsList))]
    public class NewsListController
    {
    }

    [Adapts(typeof(NewsList))]
    public class NewsListAdapter : MvcAdapter
    {
        public static void ProcessNewsContainer(ref List<ContentPage> allNews, ContentPage page)
        {
            bool noChildren = true;
            foreach (var newsItem in page.Children)
            {
                if (newsItem is ContentPage)
                {
                    noChildren = false;
                    ProcessNewsContainer(ref allNews, newsItem as ContentPage);
                }
            }
            if (noChildren && !allNews.Contains(page))
                allNews.Add(page);
        }

        public static string GetHtml(N2.ContentItem model)
        {
            if (model == null)
                return ("{model = null}"); // nothing to do 
            if (!(model is NewsList))
                return ("{adapter failure - Model is not a NewsList}"); // nothing to do 

            NewsList CurrentItem = model as NewsList;

            List<ContentPage> allNews = new List<ContentPage>();
            foreach (var containerLink in CurrentItem.Containers)
                if (containerLink.Container is ContentPage)
                    ProcessNewsContainer(ref allNews, containerLink.Container as ContentPage); // process the container

            IEnumerable<ContentPage> newsEnumerable = allNews;
            {
                // apply sort order ***
                if (CurrentItem.SortByDate == SortMode.Ascending)
                    newsEnumerable = allNews.OrderBy(a => a.Published.Value);
                else
                    newsEnumerable = allNews.OrderByDescending(a => a.Published.Value);

                // apply filter ***

                if (!CurrentItem.ShowFutureEvents)
                    newsEnumerable = newsEnumerable.Where(a => a.Published.Value <= DateTime.Now);

                if (!CurrentItem.ShowPastEvents)
                    newsEnumerable = newsEnumerable.Where(a => a.Published.Value >= DateTime.Now);

                if (CurrentItem.MaxNews > 0)
                    newsEnumerable = newsEnumerable.Take(CurrentItem.MaxNews);
            }

            var sb = new System.Text.StringBuilder(50 * 1024);

            //foreach (var x in allNews)
            //    sb.AppendFormat("<p>{0}</p>", x.Url);

            if (!String.IsNullOrEmpty(CurrentItem.Title))
            {
                sb.AppendFormat("<h{0}>{1}</h{0}>", (int)CurrentItem.TitleLevel, CurrentItem.Title);
            }

            DateTime? lastDate = null;
            foreach (ContentPage item in newsEnumerable.Where(a => a is ContentPage))
            {
                if (CurrentItem.GroupByMonth && (lastDate == null || lastDate.Value.Month != item.Published.Value.Month))
                {
                    // new month ***
                    sb.AppendFormat("<h2>{0:MMMM yyyy}</h2>\n", item.Published.Value);
                    lastDate = item.Published.Value;
                }

                bool ShowTitle = !String.IsNullOrEmpty(item.Title);// && item.ShowTitle

                // display either full article or abstract + link ***
                switch (CurrentItem.DisplayMode)
                {
                    case DisplayMode.TitleAndText:
                        if (ShowTitle)
                            sb.AppendFormat("<h{1}>{0}</h{1}>\n", item.Title ?? "Untitled", (int)CurrentItem.TitleLevel + 1);
                        sb.AppendFormat("<div class=\"date\">{0:MMMM d, yyyy}</div>\n", item.Published.Value);
                        sb.AppendFormat("<div class=\"article\">\n{0}\n</div>\n", item.Text);
                        break;

                    case DisplayMode.TitleAndAbstract:
                        if (!String.IsNullOrEmpty(item.Text))
                        {
                            if (ShowTitle)
                                sb.AppendFormat("<h{2}><a href=\"{1}\">{0}</a></h{2}>\n", item.Title ?? "Untitled", item.Url, (int)CurrentItem.TitleLevel + 1);
                            sb.AppendFormat("<div class=\"date\">{0:MMMM d, yyyy}</div>\n", item.Published.Value);
                            sb.AppendFormat("<div class=\"abstract\">\n{0}\n</div>\n", item.Summary);
                            sb.AppendFormat("<a href=\"{0}\">Read more...</a>\n", item.Url);
                        }
                        else
                        {
                            if (ShowTitle)
                                sb.AppendFormat("<h{1}>{0}</h{1}>\n", item.Title ?? "Untitled", (int)CurrentItem.TitleLevel + 1);
                            sb.AppendFormat("<div class=\"date\">{0:MMMM d, yyyy}</div>\n", item.Published.Value);
                            sb.AppendFormat("<div class=\"abstract\">\n{0}\n</div>\n", item.Summary);
                        }
                        break;

                    case DisplayMode.TitleLinkOnly:
                        sb.AppendFormat("<h{2}><a href=\"{1}\">{0}</a></h{2}>\n", item.Title ?? "Untitled", item.Url, (int)CurrentItem.TitleLevel + 1);
                        sb.AppendFormat("<div class=\"date\">{0:MMMM d, yyyy}</div>\n", item.Published.Value);

                        break;
                }
            }

            return sb.ToString();
        }

        public override void RenderTemplate(System.Web.Mvc.HtmlHelper html, N2.ContentItem model)
        {
            html.ViewContext.Writer.Write(GetHtml(model));
        }
    }
}