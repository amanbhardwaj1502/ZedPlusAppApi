using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class BankDetailsVM
    {
        public Nullable<long> Id { get; set;}
        public Nullable<long> BankId { get; set; }
        public string FullName { get; set; }     
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string BranchName { get; set; }
        public string Status { get; set; }

    }
}