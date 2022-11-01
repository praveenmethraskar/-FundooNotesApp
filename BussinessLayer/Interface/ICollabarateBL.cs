using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ICollabarateBL
    {
        public CollabratorEntity AddCollabrate(string email, long noteId);
    }
}
