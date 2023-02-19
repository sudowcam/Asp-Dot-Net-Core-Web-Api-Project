using Todo.API.Models;

namespace Todo.API.Utilities
{
    public interface IUserServices
    {
        /// <summary>
        ///   Get and return list of all available User
        /// </summary>
        /// <returns>List of User and their Notes ownership</returns>
        Task<IEnumerable<UserModel>> GetUserList();

        /// <summary>
        ///   Check if Id is a valid Id in database.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Returns True whem Id exist, otherwise False</returns>
        Task<bool> IsValidId(int id);
    }
}
