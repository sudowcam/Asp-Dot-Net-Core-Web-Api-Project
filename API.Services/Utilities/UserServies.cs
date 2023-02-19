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
    public class UserServies : IUserServices
    {
        protected ApplicationDbContext db { get; set; }

        public UserServies(ApplicationDbContext applicationDbContext)
        {
            db = applicationDbContext;
        }

        public async Task<IEnumerable<UserModel>> GetUserList()
        {
            return await db.Users
                            .Where(x => x.IsDeleted == false)
                            .Select(x => new UserModel()
                            {
                                UserId = x.Id,
                                UserName = x.Name,
                                AssociatedNote = (from collaborator in db.Collaborators
                                                  join note in db.Notes on collaborator.NoteId equals note.Id
                                                  join user in db.Users on collaborator.UserId equals user.Id
                                                  where user.Id == x.Id && note.IsDeleted == false
                                                  select new UserNoteDetails()
                                                  {
                                                      NoteId = note.Id,
                                                      Ownership = (note.OwnerId == x.Id),
                                                  })
                                                  .ToList(),
                            })
                            .ToListAsync();
        }

        public async Task<bool> IsValidId(int id)
        {
            return (await db.Users.FindAsync(id)) != null;
        }

        //void DeleteOwner(Note note);


        private void ApplySort(ref IQueryable<Notes> model, string orderByQueryString)
        {
            // when model is null
            if (!model.Any())
                return;

            // Sort elements using default settings if no query was supplied
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                model = model.OrderByDescending(x => x.DueDate);
                return;
            }

            // Expected url > localhost/api/note?orderBy=OwnerName,DueDate desc

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Notes).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');


            // Sort elements using default settings if unable to match query
            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                model = model.OrderByDescending(x => x.DueDate);
                return;
            }

            model = model.OrderBy(orderQuery);
        }
    }
}
