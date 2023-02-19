using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Todo.API.Data.Entities
{
    public partial class Status
    {
        public Status()
        {
            Notes = new HashSet<Notes>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(100)]
        public string Description { get; set; } = null!;
        public bool IsDeleted { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Notes> Notes { get; set; }
    }
}
