
using Microsoft.EntityFrameworkCore;
using ProjetoLivros.Models;

namespace ProjetoLivros.Context
{
    public class LivrosContext : DbContext
    {
        // Cada Tabela -> DbSet
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TiposUsuarios { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public LivrosContext(DbContextOptions<LivrosContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // String de Conexão
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-A2J49GH\\SQLEXPRESS;Initial Catalog=Livros;User Id=sa;Password=Senai@134;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(
                    // Representação da Tabela
                    entity =>
                    {
                        // Primary Key
                        entity.HasKey(u => u.UsuarioId);

                        entity.Property(u => u.NomeCompleto)
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                        entity.Property(u => u.Email)
                        .IsRequired()
                        .IsUnicode(false);

                        // Email é único
                        // Todo campo único é um Index dentro do banco de dados
                        entity.HasIndex(u => u.Email)
                        .IsUnique();

                        entity.Property(u => u.Senha)
                      .IsRequired()
                      .HasMaxLength(255)
                      .IsUnicode(false);

                        entity.Property(u => u.Telefone)
                      .HasMaxLength(15)
                      .IsUnicode(false);

                        entity.Property(u => u.DataCadastro)
                      .IsRequired();

                        entity.Property(u => u.DataAtualizacao)
                      .IsRequired();

                        entity.HasOne(u => u.TipoUsuario)
                        .WithMany(t => t.Usuarios)
                        .HasForeignKey(u => u.TipoUsuarioId)
                        .OnDelete(DeleteBehavior.Cascade);
                    }
                );

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                // Configuro a Primary Kay
                entity.HasKey(t => t.TipoUsuarioId);

                // Vou campo a campo configurando
                entity.Property(t => t.DescricaoTipo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

                // Descrição não pode se repetir
                // Todo campo UNIQUE é um indice
                entity.HasIndex(t => t.DescricaoTipo)
                .IsUnique();
            });

            modelBuilder.Entity<Livro>(entity =>
            {
                entity.HasKey(l => l.LivroId);

                entity.Property(l => l.Titulo)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

                entity.Property(l => l.Autor)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

                entity.Property(l => l.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);

                entity.Property(l => l.DataPublicacao)
                .IsRequired();

                // RELACIONAMENTO
                // Todo livro tem uma categoria : Livro - Categoria
                // 1 - N -> Um livro pode ter muitas categorias e uma categoria pode ter muitos Livros
                entity.HasOne(l => l.Categoria)
                .WithMany(c => c.Livros)
                .HasForeignKey(l => l.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // CATEGORIA E ASSINATURA

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.CategoriaId);

                entity.Property(c => c.NomeCategoria)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Assinatura>(entity =>
            {
                entity.HasKey(a => a.AssinaturaId);

                entity.Property(a => a.DataInicio)
                .IsRequired();

                entity.Property(a => a.DataFim)
                .IsRequired();

                entity.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                // Assinatura - Usuario
                entity.HasOne(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
