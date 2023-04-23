using System;
using System.IO;

namespace CityMeet.Models
{
    public class CityEvent
    {
        public CityEvent(string title, string desc, string image, string date, string[] category, string general_category, string price)
        {
            Header = title;
            Description = desc;
            Date = date;
            Category = category;
            GeneralCategory = general_category;
            Price = price;

            byte[] bytes = Convert.FromBase64String(image);
            Stream stream = new MemoryStream(bytes);
            Image = new Avalonia.Media.Imaging.Bitmap(stream);
        }
        public string Header { get; }
        public string Description { get; }
        public Avalonia.Media.Imaging.Bitmap Image { get; }
        public string Date { get; }
        public string Price { get; }

        private readonly string[] Category;
        private readonly string GeneralCategory;
        public bool CheckCat(string scat)
        {
            foreach (var cat in Category)
                if (cat == scat) return true;
            return GeneralCategory == scat;
        }
    }
}