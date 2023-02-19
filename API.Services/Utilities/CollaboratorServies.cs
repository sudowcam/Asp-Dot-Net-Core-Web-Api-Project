using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using Todo.API.Data.Entities;
using Todo.API.Models;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Todo.API.Utilities
{
    public class CollaboratorsService : ICollaboratorsService
    {
        protected ApplicationDbContext db { get; set; }

        public CollaboratorsService(ApplicationDbContext applicationDbContext)
        {
            db = applicationDbContext;
        }

        public async Task<IEnumerable<CollaboratorDetail>> GetCollaboratorListByNoteId(int id)
        {
            return await (from collaborator in db.Collaborators
                            join note in db.Notes on collaborator.NoteId equals note.Id
                            join user in db.Users on collaborator.UserId equals user.Id
                            where note.Id == id && note.IsDeleted == false && user.IsDeleted == false
                            select new CollaboratorDetail
                            {
                                UserId = user.Id,
                                UserName = user.Name,
                                Ownership = (note.OwnerId == user.Id),
                            })
                            .ToListAsync();
        }
    }
}
