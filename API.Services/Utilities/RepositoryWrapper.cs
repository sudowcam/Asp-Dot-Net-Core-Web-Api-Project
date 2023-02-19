using Todo.API.Models;

namespace Todo.API.Utilities
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _db;
        private ICollaboratorsService _collaborators;
        private IIdentityServices _identity;
        private INoteServices _notes;
        private IStatusServices _status;
        private IUserServices _users;

        public ICollaboratorsService Collaborators
        {
            get
            {
                if (_collaborators == null)
                {
                    _collaborators = new CollaboratorsService(_db);
                }
                return _collaborators;
            }
        }

        public IIdentityServices Identity
        {
            get
            {
                if (_identity == null)
                {
                    _identity = new IdentityServies(_db);
                }
                return _identity;
            }
        }

        public INoteServices Notes
        {
            get
            {
                if (_notes == null)
                {
                    _notes = new NoteServies(_db);
                }
                return _notes;
            }
        }

        public IStatusServices Status
        {
            get
            {
                if (_status == null)
                {
                    _status = new StatusServies(_db);
                }
                return _status;
            }
        }

        public IUserServices Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserServies(_db);
                }
                return _users;
            }
        }

        public RepositoryWrapper()
        {
            _db = new ApplicationDbContext();
        }
    }

}
