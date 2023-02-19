using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Todo.API.Data.Entities
{
    public partial class Notes
    {
        public Notes()
        {
            Collaborators = new HashSet<Collaborators>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [StringLength(500)]
        public string? Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DueDate { get; set; }
        public int StatusId { get; set; }
        public int OwnerId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("OwnerId")]
        [InverseProperty("Notes")]
        public virtual Users Owner { get; set; } = null!;
        [ForeignKey("StatusId")]
        [InverseProperty("Notes")]
        public virtual Status Status { get; set; } = null!;
        [InverseProperty("Note")]
        public virtual ICollection<Collaborators> Collaborators { get; set; }
    }
}
