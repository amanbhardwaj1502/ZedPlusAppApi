using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class customerPointValueResponse : JsonResponse
    {
        public List<PointValueVM> PointValueList { get; set; }
    }
}