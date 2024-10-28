using LogifyBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Data;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<CargoDocument> CargoDocuments { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<EmployerDriverHistory> EmployerDriverHistories { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Point> Points { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();
        
        // Add more configurations if your entities have specific relationships or constraints
    }
}