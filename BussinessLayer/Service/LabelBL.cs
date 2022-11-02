using BussinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class LabelBL : ILabelBL
    {

        private readonly ILabelRL iLabelRL;

        public LabelBL(ILabelRL iLabelRL)
        {
            this.iLabelRL=iLabelRL;
        }


        public LabelEntity AddLabel(long noteId, long userId, string labelName)
        {
            try
            {
                return iLabelRL.AddLabel(noteId,userId,labelName);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId)
        {
            
            try
            {
                return iLabelRL.RetrieveLabel(labelId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }


    }
}
