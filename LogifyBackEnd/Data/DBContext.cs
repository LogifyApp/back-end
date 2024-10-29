using LogifyBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Data;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    // Define DbSets for each entity
    public DbSet<User> Users { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<EmployerDriverHistory> EmployerDriverHistories { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Point> Points { get; set; }
    public DbSet<CargoDocument> CargoDocuments { get; set; }
    public DbSet<Attachment> Attachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Soft delete filter for Car entity
        modelBuilder.Entity<Car>().HasQueryFilter(c => !c.IsDeleted);

        // Existing configurations for other entities
        modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();

        modelBuilder.Entity<Employer>()
            .HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<Employer>(e => e.UserId);

        modelBuilder.Entity<Driver>()
            .HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<Driver>(d => d.UserId);

        modelBuilder.Entity<EmployerDriverHistory>()
            .HasKey(edh => edh.Id);
        modelBuilder.Entity<EmployerDriverHistory>()
            .HasOne(edh => edh.Employer)
            .WithMany()
            .HasForeignKey(edh => edh.EmployerUserId);
        modelBuilder.Entity<EmployerDriverHistory>()
            .HasOne(edh => edh.Driver)
            .WithMany()
            .HasForeignKey(edh => edh.DriverUserId);

        modelBuilder.Entity<Car>()
            .HasKey(c => c.Number);
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Employer)
            .WithMany()
            .HasForeignKey(c => c.EmployerUserId);

        modelBuilder.Entity<Cargo>()
            .HasOne(c => c.Car)
            .WithMany()
            .HasForeignKey(c => c.CarId);
        modelBuilder.Entity<Cargo>()
            .HasOne(c => c.Driver)
            .WithMany()
            .HasForeignKey(c => c.DriverUserId);
        modelBuilder.Entity<Cargo>()
            .HasOne(c => c.Employer)
            .WithMany()
            .HasForeignKey(c => c.EmployerUserId);

        modelBuilder.Entity<Point>()
            .HasOne(p => p.Cargo)
            .WithMany(c => c.Points)
            .HasForeignKey(p => p.CargoId);

        modelBuilder.Entity<Document>()
            .HasKey(d => d.Id);

        modelBuilder.Entity<CargoDocument>()
            .HasKey(cd => cd.Id);
        modelBuilder.Entity<CargoDocument>()
            .HasOne(cd => cd.Cargo)
            .WithMany()
            .HasForeignKey(cd => cd.CargoId);
        modelBuilder.Entity<CargoDocument>()
            .HasOne(cd => cd.Document)
            .WithMany()
            .HasForeignKey(cd => cd.DocumentId);

        modelBuilder.Entity<Chat>()
            .HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(c => c.OwnerUserId);
        modelBuilder.Entity<Chat>()
            .HasOne(c => c.Driver)
            .WithMany()
            .HasForeignKey(c => c.DriverUserId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Chat)
            .WithMany()
            .HasForeignKey(m => m.ChatId);
        modelBuilder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId);

        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.Message)
            .WithMany()
            .HasForeignKey(a => a.MessageId);
        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.Document)
            .WithMany()
            .HasForeignKey(a => a.DocumentId);
    }
}