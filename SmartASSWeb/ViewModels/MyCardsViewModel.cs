using System.Collections.Generic;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class MyCardsViewModel
        : ShareViewModel
    {
        public MyCardsViewModel()
        {
            BusinessCards = new List<BusinessCard>();
        }

        public UserProfile CurrentUserProfile { get; set; }
        public BusinessCard CurrentBusinessCard { get; set; }
        public IEnumerable<BusinessCard> BusinessCards { get; set; }
        public List<ShareContactViewModel> Contacts { get; set; }
    }

    public class ShareViewModel
    {
        public ShareViewModel()
        {
            Contacts = new List<ShareContactViewModel>();
        }
        public string SelectedBusinessCardId { get; set; }
        public List<ShareContactViewModel> Contacts { get; set; }
    }
}