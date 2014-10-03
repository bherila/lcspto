/*
 *  Copyright (C) 2012 Benjamin Herila, http://bherila.net
 *  Distributed under the Boost Software License, Version 1.0, available at http://www.boost.org/LICENSE_1_0.txt
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N2;
using N2.Web;
using N2.Engine;
using N2.Web.Mvc;
using Dinamico.Models;
using N2.Web.Parts;

namespace LCSPTO.Mvc.Controllers
{
    [Controls(typeof(FullCalendar))]
    public class FullCalendarController
    {
    }

    [Adapts(typeof(FullCalendar))]
    public class FullCalendarAdapter : PartsAdapter
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
			var sb = new System.Text.StringBuilder();
			var currentItem = model as FullCalendar;
			if (currentItem == null)
				return "Error: currentItem == null";

            // begin paste from NewsListController
            var items = new List<ContentPage>();
            foreach (var containerLink in currentItem.Containers)
				if (containerLink != null)
				{
					if (containerLink.Container != null)
					{
						if (containerLink.Container.IsPage)
							NewsListAdapter.ProcessNewsContainer(ref items, containerLink.Container as ContentPage); // process the container
					}
					else
					{
						// containerLink.container is null
						sb.AppendLine("<!-- Warning: containerLink.container is null -->");
					}
				}
				else
				{
					// containerLink is null
					sb.AppendLine("<!-- Warning: containerLink is null -->");
				}
            // end paste


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
                      jsEscape(items[i].Title),
                      (items[i].Published.Value.Hour == 0).ToString().ToLower(),
                      jsEscape(items[i].Summary),
					  itemColor
                      );

                if (!String.IsNullOrWhiteSpace(items[i].Summary))
                    ItemsWithDetails.Add(items[i]);
            }
            sb.Append(Footer);



            return sb.ToString();
        }

		static string jsEscape(string str)
		{
			return str
				.Replace("\\", "\\\\")
				.Replace("'", "\\'")
				.Replace("\n", "\\n")
				.Replace("\r", "\\r");
		}


	    public override void RenderPart(HtmlHelper html, ContentItem part, TextWriter writer = null)
	    {
			html.ViewContext.Writer.Write(GetHtml(part));
	    }
    }
}