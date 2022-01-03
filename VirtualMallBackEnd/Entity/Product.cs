using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualMallBackEnd.Entity
{
    public class Product
    {
        [Key, Column(Order = 0)]
        public string ProductCode { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public float ProfitMargin { get; set; }
        [Key,Column(Order =1)]
        public int CompanyID { get; set; }
        public Company Company {get;set;}

        public int CategoryID { get; set; }
        public Category Category { get; set; } 

    }
}
