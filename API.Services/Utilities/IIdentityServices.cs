using Todo.API.Models;

namespace Todo.API.Utilities
{
    public interface IIdentityServices
    {
        /// <summary>
        ///   Identity login
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        Task Login(string email, string password);
    }
}
