using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class JsonResponse
    {

        public JsonResponse()
        {
            this.Status_Code = "200";
            this.Status = "Success";
            this.Message = string.Empty;
            this.Error_Field = string.Empty;
            Api_Type = string.Empty;
        }


        public string Status_Code { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }

        public string Error_Field { get; set; }

        public string Api_Type { get; set; }
    }
}