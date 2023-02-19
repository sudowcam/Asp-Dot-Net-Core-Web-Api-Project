using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.WebApplication.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public List<UserNoteDetails> AssociatedNote { get; set; }
    }
    public class UserNoteDetails
    {
        public int NoteId { get; set; }
        public bool Ownership { get; set; }
    }
}