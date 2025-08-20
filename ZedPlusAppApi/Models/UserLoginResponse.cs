using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UserLoginResponse : JsonResponse
    {
        public UserLoginVM EmpLogin { get; set; }
    }
}