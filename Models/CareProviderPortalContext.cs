using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CareProviderPortal.Models;

public partial class CareProviderPortalContext : DbContext
{
    public CareProviderPortalContext()
    {
    }

    public CareProviderPortalContext(DbContextOptions<CareProviderPortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<CareProvider> CareProviders { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SW0103027\\SQLEXPRESS;Database=CareProviderPortal;User Id=TEST;Password=Gopal@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Achievem__3214EC07382252C6");

            entity.ToTable("Achievement");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Provider).WithMany(p => p.Achievements)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("FK__Achieveme__Provi__5535A963");
        });

        modelBuilder.Entity<CareProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CareProv__3214EC07CED51E47");

            entity.ToTable("CareProvider");

            entity.HasIndex(e => e.Email, "UQ__CareProv__A9D1053488BE7722").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Specialization)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("ACTIVE");

            entity.HasOne(d => d.Department).WithMany(p => p.CareProviders)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__CareProvi__Depar__52593CB8");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC0741E2B3DC");

            entity.ToTable("Department");

            entity.HasIndex(e => e.Name, "UQ__Departme__737584F6CD67C95A").IsUnique();

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Experien__3214EC079F8DBEED");

            entity.ToTable("Experience");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Organization).HasMaxLength(200);
            entity.Property(e => e.Position).HasMaxLength(100);

            entity.HasOne(d => d.Provider).WithMany(p => p.Experiences)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Experienc__Provi__7A672E12");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
