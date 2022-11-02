using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity AddLabel(long noteId, long userId, string labelName);
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId);
        public bool DeleteLabel(long labelId);
    }
}
