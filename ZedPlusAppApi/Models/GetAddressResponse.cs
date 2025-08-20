using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetAddressResponse : JsonResponse
    {
        public List<GetAddressVM> AddressList { get; set; }
    }
}