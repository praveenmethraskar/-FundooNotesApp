using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity createNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> retrieveNotes(long userId, long noteId);
        public bool DeleteNotesId(long noteId);
        public NotesEntity UpdateNote(long userId, long noteId, NotesModel notesModel);
        public bool PinNotes(long noteId, long userId);
        public bool Archieve(long noteId, long userId);
        public bool Trash(long noteId, long userId);
        public NotesEntity BgColor(long userId, long noteId, string backgroundColor, NotesModel notesModel);
    }
}
