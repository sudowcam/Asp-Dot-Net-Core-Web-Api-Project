using Todo.API.Models;

namespace Todo.API.Utilities
{
    public interface INoteServices
    {
        /// <summary>
        ///   Create a new Note entry.
        /// </summary>
        /// <param name="noteModel">Note entry details</param>
        /// <returns>Created Note model</returns>
        Task<NoteModel> CreateNewNote(NoteModel noteModel);

        /// <summary>
        ///   Get and return a single Note entry based on Note Id.
        /// </summary>
        /// <param name="id">Note Id</param>
        /// <returns>Note details</returns>
        Task<NoteModel> GetNoteById(int id);

        /// <summary>
        ///   Get and return list of Notes, that statisfy the queryParameters.
        /// </summary>
        /// <param name="queryParameters">List of queries</param>
        /// <returns>List of Note</returns>
        Task<IEnumerable<NoteModel>> GetNoteList(QueryParameters queryParameters);

        /// <summary>
        ///   Update Note entry by Note Id.
        /// </summary>
        /// <param name="id">Note Id</param>
        /// <param name="noteModel">Note details to update</param>
        /// <returns>Updated Note model</returns>
        Task<NoteModel> UpdateNoteById(int id, NoteModel noteModel);

        /// <summary>
        ///   Delete Note entry by Note Id.
        /// </summary>
        /// <param name="id">Note Id</param>
        /// <returns></returns>
        Task DeleteNoteById(int id);

        /// <summary>
        ///   Check if Id is a valid Id in database.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Returns True whem Id exist, otherwise False</returns>
        Task<bool> IsValidId(int id);
    }
}