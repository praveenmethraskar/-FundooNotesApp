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
        public IEnumerable<CollabratorEntity> retrieveCollaborate(long noteId, long userId);
        public bool DeleteCollabarator(long Collabratorid, long noteId);
    }
}
