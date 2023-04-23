using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CityMeet.Models
{
    public class GetXML
    {
        private static readonly GetXML me = new();
        public static GetXML Me { get => me; }

        public CityEvent[] eventsList;
        public string error = "";

        private GetXML()
        {
            eventsList = Loader("../../../Table.xml");
        }

        private static string Val(XAttribute? a)
        {
            return a == null ? "" : a.Value;
        }

        private CityEvent[] Loader(string filename)
        {
            List<CityEvent> list = new();
            XDocument xdoc;
            try
            {
                XDocument xDocument = XDocument.Load(filename);
                xdoc = xDocument;
            }
            catch { error = "Failed to load"; return list.ToArray(); }

            XElement? events = xdoc.Element("Events");
            if (events == null) { error = "No match events"; return list.ToArray(); }
            foreach (XElement Event in events.Elements("Event"))
            {
                XElement? title = Event.Element("Header");
                XElement? desc = Event.Element("Description");
                XElement? img = Event.Element("Image");
                XElement? date = Event.Element("Date");
                XElement? cats = Event.Element("Category");
                XElement? price = Event.Element("Price");
                if (title == null || img == null || date == null || cats == null) continue;
                string s_title = Val(title.Attribute("data"));
                string s_desc = desc == null ? "" : Val(title.Attribute("data"));
                string s_img = Val(img.Attribute("data"));
                string s_date = Val(date.Attribute("data"));
                List<string> s_cats = new();
                foreach (XElement cat in cats.Elements("CategoryItem")) s_cats.Add(Val(cat.Attribute("data")));
                string s_price = price == null ? "" : Val(price.Attribute("data")) + " \x20bd";
                string grand = Val(cats.Attribute("grand"));
                list.Add(new CityEvent(s_title, s_desc, s_img, s_date, s_cats.ToArray(), grand, s_price));
            }
            return list.ToArray();
        }
    }
}