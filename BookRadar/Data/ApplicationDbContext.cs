using BookRadar.Models;
using Microsoft.EntityFrameworkCore;

namespace BookRadar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<HistorialBusqueda> HistorialBusquedas { get; set; }
        }
}
