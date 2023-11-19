using Microsoft.EntityFrameworkCore;
using ToDoList.Maintenance.Models;

namespace ToDoList.Maintenance.DataAccess
{
    public class MaintenanceDbContext : DbContext
    {
        public MaintenanceDbContext(DbContextOptions<MaintenanceDbContext> options)
            : base(options)
        { }

        public DbSet<ToDoItem> ToDoItem { get; set; }
    }
}