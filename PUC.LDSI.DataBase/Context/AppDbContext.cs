using Microsoft.EntityFrameworkCore;
using PUC.LDSI.Domain.Entities;
using System.Linq;

namespace PUC.LDSI.DataBase.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<AvaliacaoOpcao> AvaliacaoOpcoes { get; set; }
        public DbSet<AvaliacaoQuestao> AvaliacaoQuestoes { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Prova> Provas { get; set; }
        public DbSet<ProvaOpcao> ProvaOpcoes { get; set; }
        public DbSet<ProvaQuestao> ProvaQuestoes { get; set; }
        public DbSet<Publicacao> Publicacoes { get; set; }
        public DbSet<Turma> Turmas { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

            foreach (var relationship in modelbuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                if (relationship.DeclaringEntityType.Name == modelbuilder.Entity<AvaliacaoQuestao>().Metadata.Name)
                    relationship.DeleteBehavior = DeleteBehavior.Cascade;
                else if (relationship.DeclaringEntityType.Name == modelbuilder.Entity<AvaliacaoOpcao>().Metadata.Name)
                    relationship.DeleteBehavior = DeleteBehavior.Cascade;
                else
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelbuilder);
        }
    }
}