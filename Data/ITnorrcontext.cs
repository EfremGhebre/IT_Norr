using IT_Norr.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IT_Norr.Data
{
    public class ITnorrcontext : IdentityDbContext
    {
        public ITnorrcontext(DbContextOptions<ITnorrcontext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveHistory> LeaveHistories { get; set; }

      
    }
}
