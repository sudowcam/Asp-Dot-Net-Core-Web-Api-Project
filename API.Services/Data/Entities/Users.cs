using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Todo.API.Data.Entities
{
    public partial class Users
    {
        public Users()
        {
            Collaborators = new HashSet<Collaborators>();
            Notes = new HashSet<Notes>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Collaborators> Collaborators { get; set; }
        [InverseProperty("Owner")]
        public virtual ICollection<Notes> Notes { get; set; }
    }
}
