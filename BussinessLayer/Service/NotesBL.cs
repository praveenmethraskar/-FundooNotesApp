using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL iNotesRL;

        public NotesBL(INotesRL iNotesRL)
        {
            this.iNotesRL=iNotesRL;
        }

        public NotesEntity createNotes(NotesModel notesModel, long UserId)
        {
            try
            {
                return iNotesRL.createNotes(notesModel, UserId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public IEnumerable<NotesEntity> retrieveNotes(long userId, long noteId)
        {
            try
            {
                return iNotesRL.retrieveNotes(userId, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

    }
}
