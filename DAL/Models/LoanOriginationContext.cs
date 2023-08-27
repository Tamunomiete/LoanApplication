using System;
using System.Collections.Generic;
using LoanApplication.DAL.Models.LoanProducts;
using LoanApplication.DAL.Models.Occurence;
using Microsoft.EntityFrameworkCore;

namespace LoanApplication.DAL.Models;

public partial class LoanOriginationContext : DbContext
{
    public LoanOriginationContext()
    {
    }

    public LoanOriginationContext(DbContextOptions<LoanOriginationContext> options)
        : base(options)
    {
    }

    public virtual DbSet< tbl_LoanApplication>  tbl_LoanApplication { get; set; }

    public virtual DbSet<tbl_occurence> tbl_ocuurence { get; set; }

    public virtual DbSet<tbl_LionBankLoanProducts> tbl_LionBankLoanProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-LKKLGQJ;Database=LoanOrigination;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tbl_LoanApplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanAppl__3214EC0750206F83");

            entity.ToTable("tbl_LoanApplication");

            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");
            entity.Property(e => e.BVNNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BorrowerName).HasMaxLength(100);
            entity.Property(e => e.LoanAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Interest).HasColumnType("decimal(10, 6)");
            entity.Property(e => e.LoanApplicationId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LoanRepaymentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
