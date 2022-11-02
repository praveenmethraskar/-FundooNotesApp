using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {

        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public LabelEntity AddLabel(long noteId, long userId, string labelName)
        {
            try
            {
                var noteResult = fundooContext.NotesTable.Where(x => x.noteid==noteId).FirstOrDefault();
                var userResult = fundooContext.UserTable.Where(x => x.UserId==userId).FirstOrDefault();
               

                if (noteResult!=null && userResult!=null)
                {
                    LabelEntity labelEntityobj = new LabelEntity();
                    labelEntityobj.LabelName = labelName;
                    labelEntityobj.noteid = noteResult.noteid;
                    labelEntityobj.UserId = userResult.UserId;

                    fundooContext.Add(labelEntityobj);
                    fundooContext.SaveChanges();
                    return labelEntityobj;
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

        public IEnumerable<LabelEntity> RetrieveLabel(long labelId)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.Labelid == labelId);
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

        public bool DeleteLabel(long labelId)
        {
            try
            {
                var result = fundooContext.LabelTable.FirstOrDefault(x=>x.Labelid == labelId);
                fundooContext.LabelTable.Remove(result);

                fundooContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public LabelEntity EditLabel(long noteId,long labelId, string labelName)
        {
            try
            {
                
                var labelEntityobj = fundooContext.LabelTable.FirstOrDefault(x=>x.Labelid==labelId);
                if(labelEntityobj!=null)
                {
                    labelEntityobj.LabelName = labelName;

                    fundooContext.SaveChanges();
                    return labelEntityobj;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}
