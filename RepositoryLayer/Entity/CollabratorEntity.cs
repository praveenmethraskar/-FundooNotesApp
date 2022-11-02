using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CollabratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Collabratorid { get; set; }

        public string Email { get; set; }


        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual UserEntity User { get; set; }

        [ForeignKey("notes")]
        public long noteid { get; set; }
        public virtual NotesEntity notes { get; set; }
    }
}
