using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UserUplineDetailResponse : JsonResponse
    {
        public UserUplinedetailsVM UserUplineDetails { get; set; }
    }
}