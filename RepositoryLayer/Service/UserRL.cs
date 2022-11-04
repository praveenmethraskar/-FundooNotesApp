using CommonLayer;
using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IuserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration iconfiguration;

        public UserRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration=iconfiguration;
        }
        public static string key = "adef@@kfxcbv";

        public static string ConvertToEncript(string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Password)) { return ""; }
                else
                {
                    Password+=key;
                    var passwordBytes = Encoding.UTF8.GetBytes(Password);
                    return Convert.ToBase64String(passwordBytes);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string ConvertToDecrypt(string base64EncodeData)
        {
            try
            {
                string key = "adef@@kfxcbv";
                if (string.IsNullOrEmpty(base64EncodeData)){ return "";}
                else
                {
                    var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
                    var result = Encoding.UTF8.GetString(base64EncodeBytes);
                    result = result.Substring(0, result.Length - key.Length);
                    return result;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                //query to check only for email and password
                var resultLog = fundooContext.UserTable.Where(x => x.Email == userLoginModel.Email && x.Password ==ConvertToEncript(userLoginModel.Password)).FirstOrDefault();


                if (resultLog != null && ConvertToDecrypt(resultLog.Password) ==  userLoginModel.Password)
                {
                    //taken userLoginModel to get the stored data used for login
                    //taken userLoginModel to get the stored data used for login
                    //userLoginModel.Email = resultLog.Email;
                    //userLoginModel.Password = resultLog.Password;
                    var token = GenerateSecurityToken(resultLog.Email, resultLog.UserId);
                    return token;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string ForgetPassword(string email)
        {
            try
            {
                var emailcheck = fundooContext.UserTable.FirstOrDefault(x => x.Email == email);
                if (emailcheck != null)
                {
                    var token = GenerateSecurityToken(emailcheck.Email, emailcheck.UserId);
                    MSMQ mSMQ = new MSMQ();
                    mSMQ.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string ResetPassword(string email, string newPassword, string confirmPassword)
        {

            try
            {

                if (ConvertToEncript(newPassword) == ConvertToEncript(confirmPassword))
                {
                    var user = fundooContext.UserTable.FirstOrDefault(x => x.Email == email);
                    user.Password = ConvertToEncript(newPassword);
                    fundooContext.SaveChanges();
                    return user.Password;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception e)
            {
                throw;
            }

        }

        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntityobj = new UserEntity();
                userEntityobj.FirstName = userRegistrationModel.FirstName;
                userEntityobj.LastName = userRegistrationModel.LastName;
                userEntityobj.Email = userRegistrationModel.Email;
                userEntityobj.Password =ConvertToEncript(userRegistrationModel.Password);

                fundooContext.UserTable.Add(userEntityobj);
                int result = fundooContext.SaveChanges();
                if(result != 0)
                {
                    return userEntityobj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        

        public string GenerateSecurityToken(string email, long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.iconfiguration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }






}
