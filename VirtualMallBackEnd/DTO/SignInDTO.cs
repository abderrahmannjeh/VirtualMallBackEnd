using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualMallBackEnd.Entity;

namespace VirtualMallBackEnd.DTO
{
    public class SignInDTO
    {
        public string Identify { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class SignInClientResponse
    {
        public string token { get; set; }
        public Client Client {get;set;}
    }

    public class SignInCompanyResponse
    {
        public string token { get; set; }
        public Company Company { get; set; }
    }
}
