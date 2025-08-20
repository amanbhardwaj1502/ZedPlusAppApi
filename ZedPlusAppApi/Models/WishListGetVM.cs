using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedPlusAppApi.Models
{
    public class WishListGetVM
    {

        public int Id { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? Date { get; set; }

        public int? ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
