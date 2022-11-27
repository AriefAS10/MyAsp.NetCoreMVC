using System;
using System.Collections.Generic;
using DataPribadiNetCoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DataPribadiNetCoreMVC.Data;

public partial class DataPribadiDbContext : DbContext
{
    public DataPribadiDbContext()
    {
    }

    public DataPribadiDbContext(DbContextOptions<DataPribadiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DataDb> DataDbs { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=DataPribadiDB;Persist Security Info=True;User ID=arief;Password=arief123; trusted_connection=yes; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataDb>(entity =>
        {
            entity.HasOne(d => d.Country).WithMany(p => p.DataDbs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DataDB_Countries");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
