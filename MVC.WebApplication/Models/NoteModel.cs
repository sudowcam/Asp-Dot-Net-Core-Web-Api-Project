using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.WebApplication.Models
{
    public class NoteModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [ValidateNever]
        public bool IsDeleted { get; set; }

        [Required]
        public int NoteId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ValidateNever]
        [Display(Name = "Status")]
        public string StatusName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [ValidateNever]
        [Display(Name = "Owner")]
        public string OwnerName { get; set; }

        [ValidateNever]
        public List<SelectListItem> StatusListItems { get; set; }

        [ValidateNever]
        public List<SelectListItem> UserListIems { get; set; }
    }
}
