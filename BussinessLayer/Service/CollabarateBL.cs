using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class CollabarateBL : ICollabarateBL
    {
        private readonly ICollabarateRL iCollabarateRL;

        public CollabarateBL(ICollabarateRL iCollabarateRL)
        {
            this.iCollabarateRL=iCollabarateRL;
        }


        public CollabratorEntity AddCollabrate(string email, long noteId)
        {
            try
            {
                return iCollabarateRL.AddCollabrate(email, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public IEnumerable<CollabratorEntity> retrieveCollaborate(long noteId, long userId)
        {
            try
            {
                return iCollabarateRL.retrieveCollaborate(noteId,userId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool DeleteCollabarator(long Collabratorid)
        {
            try
            {
                return iCollabarateRL.DeleteCollabarator(Collabratorid);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
