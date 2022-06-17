using System;

namespace SmartASSWeb.Bll.Models
{
    public class Transaction
    {
        public DateTime DATE { get; set; }
        public string PAY_REQUEST_ID { get; set; }
        public string REFERENCE { get; set; }
        public int AMOUNT { get; set; }
        public string CUSTOMER_EMAIL_ADDRESS { get; set; }
    }
}
