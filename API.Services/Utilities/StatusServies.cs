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
    public class StatusServies : IStatusServices
    {
        protected ApplicationDbContext db { get; set; }

        public StatusServies(ApplicationDbContext applicationDbContext)
        {
            db = applicationDbContext;
        }

        public async Task<IEnumerable<Status>> GetStatusList()
        {
            return await db.Status
                            .Where(status => status.IsDeleted == false)
                            .ToListAsync();
        }


        public async Task<bool> IsValidId(int id)
        {
            return (await db.Status.FindAsync(id)) != null;
        }
    }
}
