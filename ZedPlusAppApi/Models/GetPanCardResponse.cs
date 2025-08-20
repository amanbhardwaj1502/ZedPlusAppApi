using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetPanCardResponse : JsonResponse
    {
        public GetPanCardVM PanCard { get; set; }
    }
}