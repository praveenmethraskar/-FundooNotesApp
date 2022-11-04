using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Interface;
using System;
using System.Security.Claims;

namespace FundooWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IuserRL iuserBL;

        private readonly ILogger<UserController> _logger;

        public UserController(IuserRL iuserBL, ILogger<UserController> logger)
        {
            this.iuserBL=iuserBL;
            _logger=logger;
        } 
        [HttpPost]
        [Route("Registration")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userRegistrationModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration successful", data = result });
                
                }
                else
                {
                    
                    return BadRequest(new
                    {
                        success = false,
                        message = "Registration unsuccessful"
                    });

                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]

        public IActionResult LoginUser(UserLoginModel userLoginModel)
        {
            try
            {
                var resultLog = iuserBL.Login(userLoginModel);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Login Successful", data = resultLog });
                }
                else
                {
                    
                    return BadRequest(new { success = false, message = "Login UnSuccessful" });
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]

        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var resultLog = iuserBL.ForgetPassword(email);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Reset Email Send" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var resultlog = iuserBL.ResetPassword(email, newPassword, confirmPassword);
                if (resultlog != null)
                {
                    return Ok(new { success = true, message = "Reset Successful", data = resultlog });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Failed" });
                }
            }
            catch(System.Exception)
            {
                throw;
            }
        }

    }
}
