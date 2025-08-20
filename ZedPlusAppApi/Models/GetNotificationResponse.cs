using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetNotificationResponse : JsonResponse
    {
        public List<GetNotificationVM> GetNotification { get; set; }
    }
}