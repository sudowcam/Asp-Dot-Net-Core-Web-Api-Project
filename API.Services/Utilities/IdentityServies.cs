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
    public class IdentityServies : IIdentityServices
    {
        protected ApplicationDbContext db { get; set; }

        public IdentityServies(ApplicationDbContext applicationDbContext)
        {
            db = applicationDbContext;
        }

        public async Task Login(string email, string password)
        {
            
        }

    }
}
