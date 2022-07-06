using APIwebEscola.Models;
using Microsoft.EntityFrameworkCore;



namespace APIwebEscola.Contexts {
    public class EscolaContext : DbContext {

        public EscolaContext(DbContextOptions<EscolaContext> options) : base(options) { }
       
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Turma> Turma { get; set; }

        //navegation Property
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().ToTable("Aluno");

            modelBuilder.Entity<Aluno>()
            .HasOne(e => e.Turma)
            .WithMany(e => e.Alunos)
            .HasForeignKey(e => e.turmaid);

            modelBuilder.Entity<Turma>().ToTable("Turma");

            //modelBuilder.Entity<Turma>()
            //.HasMany(e => e.Alunos)
            //.WithOne(e => e.Turma);             

        }

    }
}
