using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetLegalDocumentListResponse : JsonResponse
    {
        public List<GetLegalDocumentListVM> GetLegalDocumentList { get; set; }
    }
}