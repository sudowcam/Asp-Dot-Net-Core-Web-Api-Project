using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<UserNoteDetails> AssociatedNote { get; set; }

    }

    public class UserNoteDetails
    {
        public int NoteId { get; set; }

        public bool Ownership { get; set; }
    }
}
