namespace StageTest.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using StageTest.Domain.Entities;

    public class AppDbContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Processo> Processos { get; set; }
        public DbSet<Subprocesso> Subprocessos { get; set; }
        public DbSet<Ferramenta> Ferramentas { get; set; }
        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<Documentacao> Documentacoes { get; set; }
        public DbSet<Equipe> Equipe { get; set; }
        public DbSet<ProcessoFerramenta> ProcessoFerramentas { get; set; }
        public DbSet<ProcessoResponsavel> ProcessoResponsaveis { get; set; }
        public DbSet<ProcessoDocumentacao> ProcessoDocumentacoes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=RAULPCGAMER\\SQLEXPRESS;Database=MyDatabase;User Id=skada;Password=root12345;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subprocesso>()
            .HasOne(s => s.SubprocessoPai)
            .WithMany(s => s.SubprocessosFilhos)
            .HasForeignKey(s => s.SubprocessoPaiId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subprocesso>()
            .Property(s => s.ProceId)
            .HasColumnName("ProceId")
            .IsRequired();


            modelBuilder.Entity<Equipe>()
           .HasMany(e => e.Responsaveis)
           .WithOne(r => r.Equipe)
           .HasForeignKey(r => r.EquipeId);
        }
    }
}
