﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IuserBL
    {
        public UserEntity Registration(UserRegistrationModel userRegistrationModel);

        //Added for Login
        public string Login(UserLoginModel userLoginModel);
        public string ForgetPassword(string email);
        public string ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
