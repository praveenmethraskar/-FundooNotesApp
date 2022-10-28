﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity createNotes(NotesModel notesModel, long userId);
        public NotesEntity retrieveNotes(long userId);
    }
}
