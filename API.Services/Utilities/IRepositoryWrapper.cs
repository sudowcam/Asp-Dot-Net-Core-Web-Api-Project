using Todo.API.Models;

namespace Todo.API.Utilities
{
    /// <summary>
    ///   Repository interface wrapper.
    /// </summary>
    public interface IRepositoryWrapper
    {
        /// <summary>
        ///  Collaborators interface declaration.
        /// </summary>
        ICollaboratorsService Collaborators { get; }
        /// <summary>
        ///  Identity interface declaration.
        /// </summary>
        IIdentityServices Identity { get; }
        /// <summary>
        ///  Notes interface declaration.
        /// </summary>
        INoteServices Notes { get; }
        /// <summary>
        ///  Status interface declaration.
        /// </summary>
        IStatusServices Status { get; }
        /// <summary>
        ///  Users interface declaration.
        /// </summary>
        IUserServices Users { get; }

    }
}