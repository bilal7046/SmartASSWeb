using System.Collections.Generic;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class ContactViewModel
    {
        public ContactViewModel()
        {
            Contacts = new List<Contact>();
        }

        public IEnumerable<Contact> Contacts { get; set; }
    }

    public class ShareContactViewModel
    {
        public bool IsSelected { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsSmartAssUser { get; set; }
    }
}