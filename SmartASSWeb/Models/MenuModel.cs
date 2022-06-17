using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartASSWeb.Models
{
    public class Child
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }

    public class Menu
    {
        public string MainHeading { get; set; }
        public int Level { get; set; }
        public List<Menu2> Menus { get; set; }
    }

    public class Menu2
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public string IconClass { get; set; }
        public bool HasChild { get; set; }
        public List<Child> Child { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }

    public class MenuModel
    {
        public List<Menu> Menu { get; set; }
    }
}