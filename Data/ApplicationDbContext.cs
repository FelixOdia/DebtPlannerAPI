using Microsoft.EntityFrameworkCore;
using DebtPlannerAPI.Model;

namespace DebtPlannerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<RepaymentPlan> RepaymentPlans { get; set; }
    }
}
