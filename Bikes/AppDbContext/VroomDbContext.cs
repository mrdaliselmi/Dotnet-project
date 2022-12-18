using Bikes.Models;
using Microsoft.EntityFrameworkCore;

namespace Bikes.AppDbContext
{
    public class VroomDbContext : DbContext
    {
        public VroomDbContext(DbContextOptions<VroomDbContext> options):
            base(options)
        {

        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }

    }
}
