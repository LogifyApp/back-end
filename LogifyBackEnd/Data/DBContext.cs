using LogifyBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Data;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<CargoDocument> CargoDocuments { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<EmployerDriverHistory> EmployerDriverHistories { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Point> Points { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Attachment_pk");

            entity.ToTable("Attachment");

            entity.Property(e => e.DocumentId).HasColumnName("Document_Id");
            entity.Property(e => e.MessageId).HasColumnName("Message_id");

            entity.HasOne(d => d.Document).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Attachment_Document");

            entity.HasOne(d => d.Message).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Attachment_Message");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("Car_pk");

            entity.ToTable("Car");

            entity.Property(e => e.Number)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmployerUserId).HasColumnName("Employer_User_Id");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.EmployerUser).WithMany(p => p.Cars)
                .HasForeignKey(d => d.EmployerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Car_Employer");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cargo_pk");

            entity.ToTable("Cargo");

            entity.Property(e => e.CarId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Car_Id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.Description)
                .HasMaxLength(1023)
                .IsUnicode(false);
            entity.Property(e => e.DriverUserId).HasColumnName("Driver_User_Id");
            entity.Property(e => e.EmployerUserId).HasColumnName("Employer_User_Id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Car).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cargo_Car");

            entity.HasOne(d => d.DriverUser).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.DriverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cargo_Driver");

            entity.HasOne(d => d.EmployerUser).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.EmployerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cargo_Employer");
        });

        modelBuilder.Entity<CargoDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cargo_Document_pk");

            entity.ToTable("Cargo_Document");

            entity.Property(e => e.CargoId).HasColumnName("Cargo_Id");
            entity.Property(e => e.DocumentId).HasColumnName("Document_Id");

            entity.HasOne(d => d.Cargo).WithMany(p => p.CargoDocuments)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cargo_Document_Cargo");

            entity.HasOne(d => d.Document).WithMany(p => p.CargoDocuments)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cargo_Document_Document");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Chat_pk");

            entity.ToTable("Chat");

            entity.Property(e => e.DriverUserId).HasColumnName("Driver_User_Id");
            entity.Property(e => e.OwnerUserId).HasColumnName("Owner_User_Id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_date");

            entity.HasOne(d => d.DriverUser).WithMany(p => p.Chats)
                .HasForeignKey(d => d.DriverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Chat_Driver");

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.Chats)
                .HasForeignKey(d => d.OwnerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Chat_Owner");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Document_pk");

            entity.ToTable("Document");

            entity.Property(e => e.FileUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FileURL");
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Filetype)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Driver_pk");

            entity.ToTable("Driver");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_Id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithOne(p => p.Driver)
                .HasForeignKey<Driver>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Driver_User");
            
            entity.Property(d => d.Status)
                .HasConversion<string>();
            
            base.OnModelCreating(modelBuilder);
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Employer_pk");

            entity.ToTable("Employer");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithOne(p => p.Employer)
                .HasForeignKey<Employer>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Owner_User");
        });

        modelBuilder.Entity<EmployerDriverHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmployerDriverHistory_pk");

            entity.ToTable("EmployerDriverHistory");

            entity.Property(e => e.DriverUserId).HasColumnName("Driver_User_Id");
            entity.Property(e => e.EmployerUserId).HasColumnName("Employer_User_Id");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.DriverUser).WithMany(p => p.EmployerDriverHistories)
                .HasForeignKey(d => d.DriverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EmployerDriverHistory_Driver");

            entity.HasOne(d => d.EmployerUser).WithMany(p => p.EmployerDriverHistories)
                .HasForeignKey(d => d.EmployerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EmployerDriverHistory_Employer");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Message_pk");

            entity.ToTable("Message");

            entity.Property(e => e.ChatId).HasColumnName("Chat_id");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_time");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_Chat");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_User");
        });

        modelBuilder.Entity<Point>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Point_pk");

            entity.ToTable("Point");

            entity.Property(e => e.CargoId).HasColumnName("Cargo_Id");
            entity.Property(e => e.Label)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Cargo).WithMany(p => p.Points)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Point_Cargo");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
