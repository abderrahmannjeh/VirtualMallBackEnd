using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Unicode;
using System.Threading.Tasks;
using VirtualMallBackEnd.DTO;
using VirtualMallBackEnd.Entity;
using VirtualMallBackEnd.Service.Authontification.IAuthebtification;

namespace VirtualMallBackEnd.Service.Authontification
{
    public class AuthentificationService : IAuthentificationService
    {

        private readonly UserManager<Account> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManger;
        private readonly IConfiguration _configuration;

        public AuthentificationService(UserManager<Account> UserManager, RoleManager<IdentityRole> RoleManger, IConfiguration configuration)
        {
            _UserManager = UserManager;
            _RoleManger = RoleManger;
            _configuration = configuration;
        }

        public async  Task<Client> SignUpClient(SignUpDTO signUpDTO)
        {
            Client client;
            Account account = await CreateAccount(signUpDTO, "Client");
            if(account!= null)
            {
                client = new Client()
                {
                    FirstName = signUpDTO.Name,
                    Email = signUpDTO.Email,
                    Mobile = int.Parse(signUpDTO.Phone),
                    IdentifiantUnique = signUpDTO.UniqueIdentity
                    
                };
                //save Client

                return client;
            }

            return null;
        }

        public async Task <Company> SignUpCompany(SignUpDTO signUpDTO)
        {
            Company company;
            Account account = await CreateAccount(signUpDTO, "Company");
            if (account != null)
            {
                company = new Company()
                {
                    Name = signUpDTO.Name,
                    Email = signUpDTO.Email,
                    Mobile = int.Parse(signUpDTO.Phone),
                    MatFiscal = signUpDTO.UniqueIdentity

                };
                //save Company
            }

            return null;

        }

        public async Task<SignInClientResponse>SignInClient(SignInDTO signInDTO)
        {
            JwtSecurityToken token=null;
            Account account = null;
            Client client =null;
            account = await FindAccountWithIdentifyAndPassword(signInDTO.Identify,signInDTO.Password);
            if (account != null)
                ///findClient with accountID
                token = GenerateToken(account, "Client");
            return new SignInClientResponse()
            {
                Client = client,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<SignInCompanyResponse> SignInCompany(SignInDTO signInDTO)
        {
            JwtSecurityToken token = null;
            Account account = null;
            Company company = null;
            account = await FindAccountWithIdentifyAndPassword(signInDTO.Identify, signInDTO.Password);
            if (account != null)
                ///findCompany with accountID
                token = GenerateToken(account, "Client");
            return new SignInCompanyResponse()
            {
                Company = company,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
        private async Task<Account> FindAccountWithIdentifyAndPassword(string Identify , string password)
        {
            Account account;
            bool checkPassword;
            if (string.IsNullOrEmpty(Identify) )
                throw new Exception("Identify is required");
            
             account = await _UserManager.FindByEmailAsync(Identify);
            if (account == null)
                account = await _UserManager.FindByNameAsync(Identify);
            if (account == null)
                account =  _UserManager.Users.Where(User => User.PhoneNumber == Identify).FirstOrDefault();
            if (account == null)
                throw new Exception("Identity Or Password are invalid");
            checkPassword = await _UserManager.CheckPasswordAsync(account, password);
            if (!checkPassword)
                throw new Exception("Identity Or Password are invalid");
            return account;

        }




        private  JwtSecurityToken GenerateToken(Account account,string role)
        {
            List<Claim> authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,account.Id),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("Role",role)
            };
            var signInKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            JwtSecurityToken token = new JwtSecurityToken
                (
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
                
                
                );
            return token;
        }

        private async Task<Account> CreateAccount(SignUpDTO signUpDTO,string role)
        {
            bool emailExist;
            bool phoneExist;
            Account newAccount;
            if (signUpDTO == null)
                throw new Exception("SignUp Details Null ");
            emailExist = await VerifyUniqueEmailAsync(signUpDTO.Email);
            if (emailExist)
                throw new Exception("Email Is Already Used");
            phoneExist = VerifyUniquePhoneAsync(signUpDTO.Phone);
            if (phoneExist)
                throw new Exception("Phone Is Already Used");
            //Add Unique Identity Test
            newAccount = new Account()
            {
                UserName = signUpDTO.UniqueIdentity,
                Email = signUpDTO.Email,
                PhoneNumber = signUpDTO.Phone,
                Status = "NotConfirmed"

            };
            IdentityResult result = await _UserManager.CreateAsync(newAccount, signUpDTO.Password);
            if (result.Succeeded)
            {
                _ = AddRoleToUser(newAccount, role);

                return newAccount;
            }

            return null;
        }
        private async Task<bool> VerifyUniqueEmailAsync(string email)
        {
            Account account = await _UserManager.FindByEmailAsync(email);
            return account != null;
        }
        private  bool VerifyUniquePhoneAsync(string phoneNumber)
        {
            bool phoneUsed =  _UserManager.Users.Where(user => user.PhoneNumber == phoneNumber).Any();
            return phoneUsed;
        }

        private async Task AddRoleToUser(Account newAccount,string role)
        {
            if (!await _RoleManger.RoleExistsAsync(role))
                await _RoleManger.CreateAsync(new IdentityRole(role));
            await _UserManager.AddToRoleAsync(newAccount, role);
        }

      
    }
}
