using System;
using System.Collections.ObjectModel;
using System.IO;

namespace CityMeet.Models
{
    public class MyTabItem
    {
        public string Header { get; }
        public string Category { get; }
        public ObservableCollection<CityEvent> EventsList
        {
            get
            {
                var res = new ObservableCollection<CityEvent>();
                foreach (var Event in GetXML.Me.eventsList)
                    if (Event.CheckCat(Category)) res.Add(Event);
                return res;
            }
        }
        public Avalonia.Media.Imaging.Bitmap CatImage { get; }
        public MyTabItem(string header, string cat, string image)
        {
            Header = header;
            Category = cat;
            byte[] bytes = Convert.FromBase64String(image);
            Stream stream = new MemoryStream(bytes);
            CatImage = new Avalonia.Media.Imaging.Bitmap(stream);
        }
    }
}