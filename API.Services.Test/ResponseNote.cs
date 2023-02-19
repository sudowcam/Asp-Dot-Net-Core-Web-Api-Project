using System.ComponentModel.DataAnnotations;

namespace API.Services.Test
{
    internal class ResponseNote
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int OwnerId { get; set; }
        public int StatusId { get; set; }

    }
}