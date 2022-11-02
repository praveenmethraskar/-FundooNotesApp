﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Labelid { get; set; }

        public string LabelName { get; set; }


        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual UserEntity User { get; set; }

        [ForeignKey("notes")]
        public long noteid { get; set; }
        public virtual NotesEntity notes { get; set; }
    }
}
