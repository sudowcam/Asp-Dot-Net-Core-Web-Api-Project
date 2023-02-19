using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Todo.API.Data.Entities
{
    public partial class Collaborators
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NoteId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("NoteId")]
        [InverseProperty("Collaborators")]
        public virtual Notes Note { get; set; } = null!;
        [ForeignKey("UserId")]
        [InverseProperty("Collaborators")]
        public virtual Users User { get; set; } = null!;
    }
}
