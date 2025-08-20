using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetNotificationVM
    {
        public long ID { get; set; }
        public string NotificationDate { get; set; }        
        public string NotificationTitle { get; set; }        
        public string NotificationDetail { get; set; }
        public string EntryDate { get; set; }
        public string Status { get; set; }
    }
}