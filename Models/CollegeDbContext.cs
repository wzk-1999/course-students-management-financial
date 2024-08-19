using Microsoft.EntityFrameworkCore;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Models;

public partial class CollegeDbContext : DbContext
{
    public CollegeDbContext()
    {
    }

    public CollegeDbContext(DbContextOptions<CollegeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<StudentType> StudentTypes { get; set; }

    public virtual DbSet<Term> Terms { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<FinancialStatement> FinancialStatement { get; set; }
    public virtual DbSet<StatementEntry> StatementEntry { get; set; }
    public virtual DbSet<FeePolicy> FeePolicy { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=VOCBook15\\SQLEXPRESS;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure enum conversion
        modelBuilder.Entity<Student>()
            .Property(s => s.Status)
            .HasConversion(
                v => v.ToString(),
                v => (StudentStatus)Enum.Parse(typeof(StudentStatus), v));

        //modelBuilder.Entity<Student>(entity =>
        //{
        //    entity.HasKey(e => e.StudentID).HasName("PK__Students__3214EC27");

        //    entity.Property(e => e.StudentID)
        //        .HasColumnName("StudentID")
        //        .HasAnnotation("SqlServer:Identity", "786280, 1");

        //});

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC27556039F6");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

            entity.HasOne(d => d.Province).WithMany(p => p.Cities)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cities__Province__3D5E1FD2");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Course__3214EC27478F3D59");

            entity.ToTable("Course");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.ProgramId).HasColumnName("ProgramID");
            entity.Property(e => e.Term).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Program).WithMany(p => p.Courses)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Course__ProgramI__4222D4EF");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Program__3214EC27B0C6B79E");

            entity.ToTable("Program");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(5);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Province__3214EC27C0E6866E");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(2);
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<StudentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentT__3214EC27C1E35886");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Type).HasMaxLength(30);
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Terms__3214EC27EA474227");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Semester).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
