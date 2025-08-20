using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedPlusAppApi.Models
{
    public class GetWishListResponse: JsonResponse
    {
        public List<WishListGetVM> WIshList { get; set; }
    }
}
