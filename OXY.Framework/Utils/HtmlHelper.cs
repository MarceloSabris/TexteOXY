using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OXY.Net.Framework.Utils
{
    public class HtmlHelper
    {       
        public String Parse(String text)
        {
            String html = String.Copy(text);
            html = html.Replace(@"&lt;lt/&gt;", "<");
            html = html.Replace(@"&lt;gt/&gt;", ">");

            html = html.Replace(@"&lt;", "<");
            html = html.Replace(@"&gt;", ">");

            html = html.Replace(@"<lt/>", "<");
            html = html.Replace(@"<gt/>", ">");

            html = html.Replace(@"&amp;nbsp;", " ");
            html = html.Replace(@"&amp;", " ");
            html = html.Replace(@"&nbsp;", " ");
            html = html.Replace(@"nbsp;", " ");

            return html;
        }
    }
}
