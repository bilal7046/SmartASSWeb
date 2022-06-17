using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SmartASSWeb.Bll.Core;

namespace SmartASSWeb.Helpers
{
    public static class LookupHelper
    {
        public static IEnumerable<SelectListItem> GetEnumCollection<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            }).ToList();
        }
    }
}