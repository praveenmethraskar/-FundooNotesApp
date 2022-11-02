using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class NotesModel
    {
        public string title { get; set; }
        public string Desciption { get; set; }
        public DateTime remainder { get; set; }
        public string color { get; set; }
        public string image { get; set; }
        public bool archive { get; set; }
        public bool pin { get; set; }
        public bool trash { get; set; }
        public DateTime created { get; set; }
        public DateTime edited { get; set; }
    }
}
