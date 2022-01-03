using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using VirtualMallBackEnd.DTO;
using VirtualMallBackEnd.Entity;

namespace VirtualMallBackEnd.Service.Authontification.IAuthebtification
{
    public interface IAuthentificationService
    {
        public Task<Company> SignUpCompany(SignUpDTO signUpDTO);

        public Task<Client> SignUpClient(SignUpDTO signUpDTO);
        public Task<SignInCompanyResponse> SignInCompany(SignInDTO signInDTO);

        public  Task<SignInClientResponse> SignInClient(SignInDTO signInDTO);
    }
}
