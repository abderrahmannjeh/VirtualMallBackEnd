using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualMallBackEnd.DTO
{
    public class SignUpDTO
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Name {get;set;}
        public string UniqueIdentity { get; set; }

    }
}
