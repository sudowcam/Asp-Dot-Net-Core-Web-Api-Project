using Todo.API.Data.Entities;
using Todo.API.Models;

namespace Todo.API.Utilities
{
    public interface IStatusServices
    {
        /// <summary>
        ///   Get and return list of all available status
        /// </summary>
        /// <returns>Generic list of status</returns>
        Task<IEnumerable<Status>> GetStatusList();

        /// <summary>
        ///   Check if Id is a valid Id in database.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Returns True whem Id exist, otherwise False</returns>
        Task<bool> IsValidId(int id);
    }
}