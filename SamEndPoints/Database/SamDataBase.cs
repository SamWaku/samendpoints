using SamEndPoints.Models;
using Microsoft.EntityFrameworkCore;

namespace SamEndPoints.SamEndPoints.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
         : base(dbContextOptions)
        {
           
        }
        public DbSet<User> Users{ get; set; }
        public DbSet<ProductModels> ProductModels{ get; set; }
    }
}