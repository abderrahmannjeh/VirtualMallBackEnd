using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualMallBackEnd.Entity
{
    public class Company
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string MatFiscal { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }
        
        public int Mobile { get; set; }

        public int Fax { get; set; }
        public byte[] Logo { get; set; }

        public string AccountID { get; set; }

        public Account Account { get; set; }
    }
}
