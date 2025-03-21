using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EnlivenDX_Evaluation.Models;

public partial class EnlivenDxAssessmentsContext : DbContext
{
    public EnlivenDxAssessmentsContext()
    {
    }

    public EnlivenDxAssessmentsContext(DbContextOptions<EnlivenDxAssessmentsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeePersonalDetail> EmployeePersonalDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SHADOWLAPTOP132\\SQLEXPRESS;Initial Catalog=EnlivenDxAssessments;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED068925FE");

            entity.ToTable("Department");

            entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC349F72AAE8").IsUnique();

            entity.Property(e => e.DepartmentLocation)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.HasKey(e => e.DesignationId).HasName("PK__Designat__BABD60DE1CF3BFD2");

            entity.ToTable("Designation");

            entity.HasIndex(e => e.DesignationName, "UQ__Designat__372CDC238638186E").IsUnique();

            entity.Property(e => e.DesignationName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Designations)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Designati__Depar__3B75D760");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB992D46D600");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId).ValueGeneratedNever();
            entity.Property(e => e.EmpFirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmpLastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmpTechSkills)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.EmpDesignationNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmpDesignation)
                .HasConstraintName("FK__Employee__EmpDes__3F466844");

            entity.HasOne(d => d.EmpMgr).WithMany(p => p.InverseEmpMgr)
                .HasForeignKey(d => d.EmpMgrId)
                .HasConstraintName("FK__Employee__EmpMgr__403A8C7D");
        });

        modelBuilder.Entity<EmployeePersonalDetail>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB9998330CFC");

            entity.Property(e => e.EmpId).ValueGeneratedNever();
            entity.Property(e => e.EmpGender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EmpMaritalStatus)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.EmpNationality)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Emp).WithOne(p => p.EmployeePersonalDetail)
                .HasForeignKey<EmployeePersonalDetail>(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeP__EmpId__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
