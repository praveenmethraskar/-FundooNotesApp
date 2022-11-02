using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity AddLabel(long noteId, long userId, string labelName);
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId);
        public bool DeleteLabel(long labelId);
    }
}
