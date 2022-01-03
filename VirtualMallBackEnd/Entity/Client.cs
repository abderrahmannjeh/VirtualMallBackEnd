using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualMallBackEnd.Entity
{
    public class Client
    {
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Mobile { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public byte[]Image { get; set; } 

        public bool EmailOptin { get; set; }
        public bool SmsOptin { get; set; }

        public string IdentifiantUnique { get; set; }

        public string AccountID { get; set; }

        public Account Account { get; set; }
    }
}
