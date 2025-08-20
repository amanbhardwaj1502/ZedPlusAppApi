using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetRequirementListResponse : JsonResponse
    {
        public List<GetRequirementListVM> GetRequirementList { get; set; }
    }
}