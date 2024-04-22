using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Infrastructure;
public class PassInDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<CheckIn> Check_Ins { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\dev\\PassIn\\database.sql");
    }
}
