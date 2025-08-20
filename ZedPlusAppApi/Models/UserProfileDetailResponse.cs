using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UserProfileDetailResponse : JsonResponse
    {
        public ProfileDetailsVM ProfileDetails { get; set; }
    }
}