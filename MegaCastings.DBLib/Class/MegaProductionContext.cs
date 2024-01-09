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

    public virtual DbSet<Announce> Announces { get; set; }

    public virtual DbSet<AnnounceCandidate> AnnounceCandidates { get; set; }

    public virtual DbSet<BigCategory> BigCategories { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Pack> Packs { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersAdmin> UsersAdmins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=qwy.fr;database=MegaProduction;user=evan;password=qwy44*");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announce>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.Bigcategoryid, "FK_Announces_BigCategory");

            entity.HasIndex(e => e.Contracttypeid, "FK_Announces_Contracts");

            entity.HasIndex(e => e.Subcategoryid, "FK_Announces_SubCategory");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Bigcategoryid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("bigcategoryid");
            entity.Property(e => e.Content)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.Contracttypeid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("contracttypeid");
            entity.Property(e => e.Datetime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("timestamp")
                .HasColumnName("datetime");
            entity.Property(e => e.Enddate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("timestamp")
                .HasColumnName("enddate");
            entity.Property(e => e.Hourlyrate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("hourlyrate");
            entity.Property(e => e.Isactive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("isactive");
            entity.Property(e => e.Startdate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("timestamp")
                .HasColumnName("startdate");
            entity.Property(e => e.Subcategoryid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("subcategoryid");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("title");

            entity.HasOne(d => d.Bigcategory).WithMany(p => p.Announces)
                .HasForeignKey(d => d.Bigcategoryid)
                .HasConstraintName("FK_Announces_BigCategory");

            entity.HasOne(d => d.Contracttype).WithMany(p => p.Announces)
                .HasForeignKey(d => d.Contracttypeid)
                .HasConstraintName("FK_Announces_Contracts");

            entity.HasOne(d => d.Subcategory).WithMany(p => p.Announces)
                .HasForeignKey(d => d.Subcategoryid)
                .HasConstraintName("FK_Announces_SubCategory");
        });

        modelBuilder.Entity<AnnounceCandidate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.Announceid, "FK_AnnounceCandidates_Announces");

            entity.HasIndex(e => e.Candidatesid, "FK_AnnounceCandidates_Users");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Announceid)
                .HasColumnType("int(11)")
                .HasColumnName("announceid");
            entity.Property(e => e.Candidatesid)
                .HasColumnType("int(11)")
                .HasColumnName("candidatesid");

            entity.HasOne(d => d.Announce).WithMany(p => p.AnnounceCandidates)
                .HasForeignKey(d => d.Announceid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnnounceCandidates_Announces");

            entity.HasOne(d => d.Candidates).WithMany(p => p.AnnounceCandidates)
                .HasForeignKey(d => d.Candidatesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnnounceCandidates_Users");
        });

        modelBuilder.Entity<BigCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("BigCategory");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasDefaultValueSql("''''''")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(25)
                .HasDefaultValueSql("''''''")
                .HasColumnName("abbreviation");
            entity.Property(e => e.Label)
                .HasMaxLength(100)
                .HasDefaultValueSql("''''''")
                .HasColumnName("label");
        });

        modelBuilder.Entity<Pack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Desc)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("desc");
            entity.Property(e => e.Label)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("label");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.Bigcategoryid, "FK_Partners_BigCategory");

            entity.HasIndex(e => e.Packid, "FK_Partners_Packs");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Bigcategoryid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("bigcategoryid");
            entity.Property(e => e.Datetime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("timestamp")
                .HasColumnName("datetime");
            entity.Property(e => e.Desc)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("desc");
            entity.Property(e => e.Isactive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("isactive");
            entity.Property(e => e.Label)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("label");
            entity.Property(e => e.Packid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("packid");
            entity.Property(e => e.Siret)
                .HasMaxLength(14)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("siret");

            entity.HasOne(d => d.Bigcategory).WithMany(p => p.Partners)
                .HasForeignKey(d => d.Bigcategoryid)
                .HasConstraintName("FK_Partners_BigCategory");

            entity.HasOne(d => d.Pack).WithMany(p => p.Partners)
                .HasForeignKey(d => d.Packid)
                .HasConstraintName("FK_Partners_Packs");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("SubCategory");

            entity.HasIndex(e => e.Bigcategoryid, "BigCategory");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Bigcategoryid)
                .HasColumnType("int(11)")
                .HasColumnName("bigcategoryid");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasDefaultValueSql("''''''")
                .HasColumnName("title");

            entity.HasOne(d => d.Bigcategory).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.Bigcategoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategory_BigCategory");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.Annonceid, "FK_Users_Announces");

            entity.HasIndex(e => e.Bigcategoryid, "FK_Users_BigCategory");

            entity.HasIndex(e => e.Subcategoryid, "FK_Users_SubCategory");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Annonceid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("annonceid");
            entity.Property(e => e.Bigcategoryid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("bigcategoryid");
            entity.Property(e => e.Birthdate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("birthdate");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("firstname");
            entity.Property(e => e.Isactive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("isactive");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("password");
            entity.Property(e => e.Subcategoryid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("subcategoryid");

            entity.HasOne(d => d.Annonce).WithMany(p => p.Users)
                .HasForeignKey(d => d.Annonceid)
                .HasConstraintName("FK_Users_Announces");

            entity.HasOne(d => d.Bigcategory).WithMany(p => p.Users)
                .HasForeignKey(d => d.Bigcategoryid)
                .HasConstraintName("FK_Users_BigCategory");

            entity.HasOne(d => d.Subcategory).WithMany(p => p.Users)
                .HasForeignKey(d => d.Subcategoryid)
                .HasConstraintName("FK_Users_SubCategory");
        });

        modelBuilder.Entity<UsersAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("UsersAdmin");

            entity.Property(e => e.Id)
                .HasColumnType("int(11) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(75)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("firstname");
            entity.Property(e => e.Isactive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("isactive");
            entity.Property(e => e.Lastname)
                .HasMaxLength(75)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("password");
            entity.Property(e => e.Perm)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("perm");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
