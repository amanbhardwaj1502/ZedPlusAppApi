using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class StateResponse : JsonResponse
    {
        public List<StateVM> StateList { get; set; }
    }
    // This class is used to represent the response for state-related API calls.
}