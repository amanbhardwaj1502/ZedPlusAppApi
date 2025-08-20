using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetGrievanceListResponse : JsonResponse
    {
        public List<GetGrievanceListVM> GetGrievanceList { get; set; }
    }
}