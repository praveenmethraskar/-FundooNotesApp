using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabarateRL : ICollabarateRL
    {
        private readonly FundooContext fundooContext;
        
        public CollabarateRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public CollabratorEntity AddCollabrate(string email, long noteId)
        {
            try
            {
                var noteResult = fundooContext.NotesTable.Where(x=>x.noteid==noteId).FirstOrDefault();
                var emailResult = fundooContext.UserTable.Where(x => x.Email==email).FirstOrDefault();

                if(emailResult!=null && noteResult!=null)
                {
                    CollabratorEntity collabratorEntityobj = new CollabratorEntity();
                    collabratorEntityobj.Email = emailResult.Email;
                    collabratorEntityobj.noteid = noteResult.noteid;
                    collabratorEntityobj.UserId = emailResult.UserId;

                    fundooContext.Add(collabratorEntityobj);
                    fundooContext.SaveChanges();
                    return collabratorEntityobj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CollabratorEntity> retrieveCollaborate(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.CollabratorTable.Where(x => x.noteid == noteId && x.UserId == userId);
                if (result!=null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCollabarator(long Collabratorid)
        {
            try
            {
                var result = fundooContext.CollabratorTable.FirstOrDefault(x => x.Collabratorid==Collabratorid);

                fundooContext.CollabratorTable.Remove(result);

                fundooContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
