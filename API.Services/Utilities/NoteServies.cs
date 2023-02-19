using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using Todo.API.Data.Entities;
using Todo.API.Models;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Todo.API.Utilities
{
    public class NoteServies : INoteServices
    {
        protected ApplicationDbContext db { get; set; }

        public NoteServies(ApplicationDbContext applicationDbContext)
        {
            db = applicationDbContext;
        }

        public async Task<NoteModel> CreateNewNote(NoteModel noteModel)
        {
            int NewNoteId = 0;
            var model = new Notes
            {
                Title = noteModel.Title,
                Description = noteModel.Description,
                DueDate = noteModel.DueDate,
                IsDeleted = false,
                OwnerId = noteModel.OwnerId,
                StatusId = noteModel.StatusId
            };
            db.Add(model);

            try
            {
                await db.SaveChangesAsync();
                NewNoteId = model.Id;
            }
            catch(Exception ex)
            {
                // Implement logger
            }

            // Return newly added model
            return await db.Notes
                            .Where(note => note.Id == NewNoteId)
                            .Select(note => new NoteModel()
                            {
                                Title = note.Title,
                                Description = note.Description,
                                DueDate = note.DueDate,
                                OwnerId = note.OwnerId,
                                StatusId = note.StatusId
                            }).FirstOrDefaultAsync();
        }

        public async Task<NoteModel> GetNoteById(int id)
        {
            return await db.Notes.Where(note => note.IsDeleted == false && note.Id == id)
                                .Select(note => new NoteModel
                                {
                                    NoteId = note.Id,
                                    Title = note.Title,
                                    DueDate = note.DueDate,
                                    Description = note.Description,
                                    OwnerId = note.OwnerId,
                                    OwnerName = note.Owner.Name,
                                    StatusId = note.Status.Id,
                                    StatusName = note.Status.Name
                                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NoteModel>> GetNoteList(QueryParameters query)
        {
            var model = db.Notes.Where(note => note.IsDeleted == false);

            if (model.Any())
            {
                #region Search & Filter
                // GeneralSearch
                if (!string.IsNullOrWhiteSpace(query.GeneralSearch))
                {
                    model = model
                        .Where(note => note.Title.ToUpper().Contains(query.GeneralSearch.Trim().ToUpper()) || 
                                        note.Description.ToUpper().Contains(query.GeneralSearch.Trim().ToUpper()) ||
                                        note.Owner.Name.ToUpper().Contains(query.GeneralSearch.Trim().ToUpper()) ||
                                        note.Status.Name.ToUpper().Contains(query.GeneralSearch.Trim().ToUpper()) ||
                                        note.DueDate.ToString().ToUpper().Contains(query.GeneralSearch.Trim().ToUpper())
                        );
                }
                
                // Title
                if (!string.IsNullOrWhiteSpace(query.Title))
                {
                    model = model.Where(note => note.Title.ToUpper().Contains(query.Title.Trim().ToUpper()));
                }
                
                // Ownership
                if (!string.IsNullOrWhiteSpace(query.Ownership))
                {
                    model = model.Where(note => note.Owner.Name.ToUpper().Contains(query.Ownership.Trim().ToUpper()));
                }
               
                // Status
                if (!string.IsNullOrWhiteSpace(query.Status))
                {
                    model = model.Where(note => note.Status.Name.ToUpper().Contains(query.Status.Trim().ToUpper()));
                }
                #endregion

                ApplySort(ref model, query.OrderBy);
            }

            return await model.Select(note => new NoteModel
                                {
                                    NoteId = note.Id,
                                    Title = note.Title,
                                    DueDate = note.DueDate,
                                    Description = note.Description,
                                    OwnerId = note.OwnerId,
                                    OwnerName = note.Owner.Name,
                                    StatusId = note.Status.Id,
                                    StatusName = note.Status.Name
                                }).ToListAsync();
        }

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

        public async Task<NoteModel> UpdateNoteById(int id, NoteModel noteModel)
        {
            var model = await db.Notes.FindAsync(id);
            model.Title = noteModel.Title;
            model.Description = noteModel.Description;
            model.DueDate = noteModel.DueDate;
            model.StatusId = noteModel.StatusId;
            model.OwnerId = noteModel.OwnerId;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Implement logger
                return new NoteModel();
            }
            return noteModel;
        }

        public async Task DeleteNoteById(int id)
        {
            var model = await db.Notes.FindAsync(id);
            model.IsDeleted = true;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Implement logger
            }
            return;
        }

        public async Task<bool> IsValidId(int id)
        {
            return (await db.Notes.FindAsync(id)) != null;
        }


    }
}
