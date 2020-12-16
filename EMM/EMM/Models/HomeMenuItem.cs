using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EMM.Models
{
    public enum MenuItemType
    {
        Browse,
        About
    }
    public class HomeMenuItem
    {
        public HomeMenuItem(string imagePath, MenuItemType menuItemType, string title)
        {
            Image = imagePath;
            Id = menuItemType;
            Title = title;
        }
        public MenuItemType Id { get; set; }

        public ImageSource Image { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
