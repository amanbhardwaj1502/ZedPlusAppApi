using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetLevelResponse : JsonResponse
    {
        public List<GetLevelVM> LevelList { get; set; }
    }
}