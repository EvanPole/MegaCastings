using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MegaCastings.DBLib.Class;

public partial class MegaProductionContext : DbContext
{
    public MegaProductionContext()
    {
    }

    public MegaProductionContext(DbContextOptions<MegaProductionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=qwy.fr;database=MegaProduction;user=evan;password=qwy44*", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.21-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id)
                .HasColumnType("int(11) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Lastname)
                .HasColumnType("text")
                .HasColumnName("lastname");
            entity.Property(e => e.Firstname)
                .HasColumnType("text")
                .HasColumnName("firstname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
