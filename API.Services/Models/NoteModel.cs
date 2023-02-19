using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models
{
    public class NoteModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [ValidateNever]
        public bool IsDeleted { get; set; }

        [Required]
        public int NoteId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ValidateNever]
        public string StatusName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [ValidateNever]
        public string OwnerName { get; set; }

    }

}
