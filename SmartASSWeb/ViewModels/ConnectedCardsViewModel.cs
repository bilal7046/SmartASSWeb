using System.Collections.Generic;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class ConnectedCardsViewModel
        : ShareViewModel
    {
        public ConnectedCardsViewModel()
        {
            ConnectedBusinessCards = new List<BusinessCard>();
        }

        public UserProfile CurrentUserProfile { get; set; }
        public BusinessCard CurrentBusinessCard { get; set; }
        public IEnumerable<BusinessCard> ConnectedBusinessCards { get; set; }
    }
}