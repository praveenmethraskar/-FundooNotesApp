using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long noteid { get; set; }

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


        [ForeignKey("User")]
        public long UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity User { get; set; }

    }
}
