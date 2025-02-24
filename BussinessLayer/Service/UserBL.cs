﻿using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class UserBL : IuserBL
    {
        private readonly IuserRL iuserRL;
        public UserBL(IuserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return iuserRL.Login(userLoginModel);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public string ForgetPassword(string email)
        {
            try
            {
                return iuserRL.ForgetPassword(email);
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
                return iuserRL.ResetPassword(email, newPassword, confirmPassword);
            }
            catch (Exception e)
            {
                throw;
            }
        }


    }
}
