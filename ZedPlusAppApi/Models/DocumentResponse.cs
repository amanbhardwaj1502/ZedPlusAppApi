using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class DocumentResponse : JsonResponse
    {
        public List<DocumentListVM> DocumentList { get; set; }
    }
}