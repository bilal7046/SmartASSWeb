using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Services;

namespace SmartASSWeb.ViewModels
{
    public class BusinessCardViewModel
    {
        public BusinessCardViewModel()
        {
            Tags = new List<LookupDataItem>();
            ProductServices = new List<LookupDataItem>();
        }
        public BusinessCard BusinessCard { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public List<LookupDataItem> Tags { get; set; }
        public List<LookupDataItem> ProductServices { get; set; }

        public SelectList GetTags(string[] savedTags)
        {
            var col = (from t in this.Tags
                join st in savedTags on t.Key equals st into tagCol
                from tg in tagCol.DefaultIfEmpty()
                select new SelectListItem
                {
                    Value = t.Key,
                    Text = t.Value,
                    Selected = t.Key == tg
                });
            return new SelectList(col, "Value", "Key");
        }
        public SelectList GetProductServices(string [] savedProductServices)
        {
            var col = (from t in this.ProductServices
                join st in savedProductServices on t.Key equals st into tagCol
                from tg in tagCol.DefaultIfEmpty()
                select new SelectListItem
                {
                    Value = t.Key,
                    Text = t.Value,
                    Selected = t.Key == tg
                });
            return new SelectList(col, "Value", "Key");
        }
    }
}