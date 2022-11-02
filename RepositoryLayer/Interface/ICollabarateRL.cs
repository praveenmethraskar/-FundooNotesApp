using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabarateRL
    {
        public CollabratorEntity AddCollabrate(string email, long noteId);
        public IEnumerable<CollabratorEntity> retrieveCollaborate(long noteId, long userId);
        public bool DeleteCollabarator(long Collabratorid, long noteId);
    }
}
