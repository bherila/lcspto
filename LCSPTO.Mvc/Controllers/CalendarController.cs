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
    [Controls(typeof(FullCalendar))]
    public class FullCalendarController
    {
    }

    [Adapts(typeof(FullCalendar))]
    public class FullCalendarAdapter : MvcAdapter
    {
        private const string Header = @"
<div id=""$ID"" style=""margin:3em 0;font-size:13px""></div>
<script type=""text/javascript"">

$(document).ready(function() {
	
	var date = new Date();
	var d = date.getDate();
	var m = date.getMonth();
	var y = date.getFullYear();
		
	$('#$ID').fullCalendar({
		header: {
			left: 'prev,next today',
			center: 'title',
			right: 'month,agendaWeek,agendaDay'
		},
		editable: false,
		events: [";
	private const string Footer = @"
			],
		eventRender: function(event, element, view)
		{
			if (event.d != '') { element.qtip({ content: ""Event: "" + event.d }); }
		}
	});
		
});

</script>";

        public static string GetHtml(N2.ContentItem model)
        {
            var currentItem = model as FullCalendar;

            // begin paste from NewsListController
            var items = new List<ContentPage>();
            foreach (var containerLink in currentItem.Containers)
                if (containerLink.Container.IsPage)
                    NewsListAdapter.ProcessNewsContainer(ref items, containerLink.Container as ContentPage); // process the container
            // end paste

            var sb = new System.Text.StringBuilder(
                Header.Length + Footer.Length + 1024 * items.Count);

            var ItemsWithDetails = new List<ContentPage>();

            sb.Append(Header.Replace("$ID", "cal" + currentItem.ID));
            for (int i = 0; i < items.Count; ++i)
            {
                if (i > 0)
                    sb.Append(',');

	            var itemColor = string.IsNullOrWhiteSpace(items[i].Summary) ? "gray" : "navy";


                sb.AppendFormat(@"
    {{ title: '{3}', allDay: {4}, d: '{5}', /* url: '{2}', */
            start: new Date({0:yyyy, M-1, d, H, m}), color: '{6}',
            end: new Date({1:yyyy, M-1, d, H, m}) }}", 
                      items[i].Published.Value, 
                      items[i].Expires.HasValue ? items[i].Expires : items[i].Published,
                      items[i].Url, 
                      items[i].Title.Replace("'", "\\'"),
                      (items[i].Published.Value.Hour == 0).ToString().ToLower(),
                      items[i].Summary.Replace("'", "\\'") /* escape for json */,
					  itemColor
                      );

                if (!String.IsNullOrWhiteSpace(items[i].Summary))
                    ItemsWithDetails.Add(items[i]);
            }
            sb.Append(Footer);



            return sb.ToString();
        }

        public override void RenderTemplate(System.Web.Mvc.HtmlHelper html, N2.ContentItem model)
        {
            html.ViewContext.Writer.Write(GetHtml(model));
        }
    }
}