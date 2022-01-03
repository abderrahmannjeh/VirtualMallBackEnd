using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualMallBackEnd.DTO;
using VirtualMallBackEnd.Entity;
using VirtualMallBackEnd.Service.Authontification.IAuthebtification;

namespace VirtualMallBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutentificationController : ControllerBase
    {

        private readonly IAuthentificationService _AuthentificationService;

        public AutentificationController(IAuthentificationService authentificationService)
        {
            _AuthentificationService = authentificationService;
        }

        [HttpPost]
        [Route("/SignUpClient")]
        public async Task<IActionResult>SignUpClient([FromBody] SignUpDTO signUpDTO)
        {
            try
            {
                Client client = await _AuthentificationService.SignUpClient(signUpDTO);
                return StatusCode(StatusCodes.Status200OK, client); ;

            }catch(Exception Ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Ex.Message);
            }

        }

        [HttpPost]
        [Route("/SignInClient")]
        public async Task<IActionResult> SignInClient([FromBody] SignInDTO signInDTO)
        {
            try
            {
                SignInClientResponse Response = await _AuthentificationService.SignInClient(signInDTO);
                return StatusCode(StatusCodes.Status200OK, Response); 

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Ex.Message);
            }

        }

        [HttpPost]
        [Route("/SignUpCompany")]
        public async Task<IActionResult> SignUpCompany([FromBody] SignUpDTO signUpDTO)
        {
            try
            {
                Company company = await _AuthentificationService.SignUpCompany(signUpDTO);
                return StatusCode(StatusCodes.Status200OK, company); 

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Ex.Message);
            }

        }
    }
}
