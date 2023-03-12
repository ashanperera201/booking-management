using System;
using System.Collections.Generic;
using BookingManagementDomain.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementDomain.Context;

public partial class BookingManagementContext : DbContext
{
    public BookingManagementContext()
    {
    }

    public BookingManagementContext(DbContextOptions<BookingManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NGS1H9H;Initial Catalog=BookingManagement;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3214EC0707689331");

            entity.ToTable("Booking");

            entity.HasIndex(e => e.UniqueId, "UQ__Booking__A2A2A54B7BD7EFE6").IsUnique();

            entity.Property(e => e.AssignedDriver).HasMaxLength(100);
            entity.Property(e => e.BookedTime).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(400);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.FromAddress).HasMaxLength(200);
            entity.Property(e => e.GoodType).HasMaxLength(100);
            entity.Property(e => e.ToAddress).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(400);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Weight).HasColumnType("numeric(10, 2)");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC078C17853A");

            entity.ToTable("Permission");

            entity.HasIndex(e => e.UniqueId, "UQ__Permissi__A2A2A54B374F1C05").IsUnique();

            entity.Property(e => e.CreatedBy).HasMaxLength(400);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.PermissionName).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(400);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Permissio__RoleI__0A9D95DB");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07E47A3555");

            entity.ToTable("Role");

            entity.HasIndex(e => e.UniqueId, "UQ__Role__A2A2A54B1DD87A3B").IsUnique();

            entity.Property(e => e.CreatedBy).HasMaxLength(400);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.RoleName).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(400);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07344E27F1");

            entity.ToTable("User");

            entity.HasIndex(e => e.UniqueId, "UQ__User__A2A2A54B5ECFE367").IsUnique();

            entity.Property(e => e.CreatedBy).HasMaxLength(400);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(400);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UserContact).HasMaxLength(200);
            entity.Property(e => e.UserEmail).HasMaxLength(200);
            entity.Property(e => e.UserName).HasMaxLength(200);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__RoleId__123EB7A3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
