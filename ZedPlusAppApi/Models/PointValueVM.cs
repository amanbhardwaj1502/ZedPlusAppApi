using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class PointValueVM
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public double TotalPV { get; set; }
    }
}