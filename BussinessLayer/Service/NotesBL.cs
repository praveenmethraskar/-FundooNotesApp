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
        public bool DeleteNotesId(long noteId)
        {
            try
            {
                return iNotesRL.DeleteNotesId(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public NotesEntity UpdateNote(long userId, long noteId, NotesModel notesModel)
        {
            try
            {
                return iNotesRL.UpdateNote(userId, noteId, notesModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool PinNotes(long noteId, long userId)
        {
            try
            {
                return iNotesRL.PinNotes(noteId, userId);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool Archieve(long noteId, long userId)
        {
            try
            {

                return iNotesRL.Archieve(noteId,userId);
            }
            catch(Exception e)
            {
                throw;
            }
        }


        public bool Trash(long noteId, long userId)
        {
            try
            {
                return iNotesRL.Trash(noteId,userId);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public NotesEntity BgColor(long userId, long noteId, string backgroundColor, NotesModel notesModel)
        {
            try
            {
                return iNotesRL.BgColor(userId, noteId, backgroundColor, notesModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}
