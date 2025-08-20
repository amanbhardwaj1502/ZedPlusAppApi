using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class PinCodeResponse : JsonResponse
    {
        public PinCodeVM PicodeDetails { get; set; }
    }
}