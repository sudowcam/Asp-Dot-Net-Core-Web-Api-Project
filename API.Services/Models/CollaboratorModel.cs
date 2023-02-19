using System.ComponentModel.DataAnnotations;
using Todo.API.Data.Entities;

namespace Todo.API.Models
{
    public class CollaboratorList
    {
        public int NoteId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public List<CollaboratorDetail> Collaborators { get; set; }
    }

    public class CollaboratorDetail
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool Ownership { get; set; }
    }

}
