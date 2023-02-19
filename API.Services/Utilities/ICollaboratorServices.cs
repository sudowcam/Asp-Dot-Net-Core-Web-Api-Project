using Todo.API.Models;

namespace Todo.API.Utilities
{
    public interface ICollaboratorsService
    {
        /// <summary>
        ///   Get and return list of all collaborators assiociated with Note Id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>list of all collaborators assiociated with Note Id</returns>
        Task<IEnumerable<CollaboratorDetail>> GetCollaboratorListByNoteId(int id);
    }
}
