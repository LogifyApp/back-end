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

            // User Configuration
            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            // Employer Configuration
            modelBuilder.Entity<Employer>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employer>(e => e.UserId);

            // Driver Configuration
            modelBuilder.Entity<Driver>()
                .HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Driver>(d => d.UserId);

            // EmployerDriverHistory Configuration
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

            // Car Configuration
            modelBuilder.Entity<Car>()
                .HasKey(c => c.Number); // Assuming 'Number' is unique identifier for Car
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Employer)
                .WithMany()
                .HasForeignKey(c => c.EmployerUserId);

            // Cargo Configuration
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

            // Point Configuration
            modelBuilder.Entity<Point>()
                .HasOne(p => p.Cargo)
                .WithMany()
                .HasForeignKey(p => p.CargoId);

            // Document Configuration
            modelBuilder.Entity<Document>()
                .HasKey(d => d.Id);

            // CargoDocument Configuration
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

            // Chat Configuration
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerUserId);
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Driver)
                .WithMany()
                .HasForeignKey(c => c.DriverUserId);

            // Message Configuration
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany()
                .HasForeignKey(m => m.ChatId);
            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId);

            // Attachment Configuration
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