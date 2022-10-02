using System;
using System.Collections.Generic;
using EntityFrameworkCore.Scaffolding.Extensions.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Scaffolding.Extensions.Sample;

public partial class SampleDatabaseContext : DbContext
{
    public SampleDatabaseContext()
    {
    }

    public SampleDatabaseContext(DbContextOptions<SampleDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apple> Apples { get; set; } = null!;

    public virtual DbSet<Tree> Trees { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLExpress; Initial Catalog=SampleDatabase; Integrated Security=True; Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apple>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkApple");

            entity.ToTable("Apple", tb => tb.HasComment("Apple entity"));

            entity.Property(e => e.Id)
                .HasColumnOrder(0)
                .HasComment("Primary key.");
            entity.Property(e => e.CreatedAt)
                .HasColumnOrder(3)
                .HasComment("Creation time.");
            entity.Property(e => e.CreatedBy)
                .HasColumnOrder(4)
                .HasComment("Created by.");
            entity.Property(e => e.IsDeleted)
                .HasColumnOrder(7)
                .HasComment("Is deleted.");
            entity.Property(e => e.ModifiedAt)
                .HasColumnOrder(5)
                .HasComment("Last modification time.");
            entity.Property(e => e.ModifiedBy)
                .HasColumnOrder(6)
                .HasComment("Last modified by.");
            entity.Property(e => e.Status)
                .HasColumnOrder(1)
                .HasComment("Status of the apple.")
                .HasColumnType("int");
            entity.Property(e => e.TreeId)
                .HasColumnOrder(2)
                .HasComment("The tree it is belonging to.");

            entity.HasOne(d => d.Tree).WithMany(p => p.Apples)
                .HasForeignKey(d => d.TreeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkAppleToTree");
        });

        modelBuilder.Entity<Tree>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkTree");

            entity.ToTable("Tree", tb => tb.HasComment("Tree entity"));

            entity.Property(e => e.Id)
                .HasColumnOrder(0)
                .HasComment("Primary key.");
            entity.Property(e => e.CreatedAt)
                .HasColumnOrder(4)
                .HasComment("Creation time.");
            entity.Property(e => e.CreatedBy)
                .HasColumnOrder(5)
                .HasComment("Created by.");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnOrder(2)
                .HasComment("Description of the tree.");
            entity.Property(e => e.ModifiedAt)
                .HasColumnOrder(6)
                .HasComment("Last modification time.");
            entity.Property(e => e.ModifiedBy)
                .HasColumnOrder(7)
                .HasComment("Last modified by.");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnOrder(1)
                .HasComment("Name of the tree.");
            entity.Property(e => e.Type)
                .HasColumnOrder(3)
                .HasComment("Type of the tree.")
                .HasColumnType("int");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
