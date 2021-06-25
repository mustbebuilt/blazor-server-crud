using System;
using Microsoft.EntityFrameworkCore;

namespace blazorServerOnlyVersionv1.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Film> Films { get; set; }
    }
}
