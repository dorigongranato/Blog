using System;
using System.Configuration;
using Blog.Dados.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;

namespace Blog.Dados.Models
{
    public partial class ContextBlog : DbContext
    {
        public ContextBlog()
        {
        }

        public ContextBlog(DbContextOptions<ContextBlog> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAutor> TblAutor { get; set; }
        public virtual DbSet<TblCategoria> TblCategoria { get; set; }
        public virtual DbSet<TblComentario> TblComentario { get; set; }
        public virtual DbSet<TblPost> TblPost { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                string usuario = ConfigurationManager.AppSettings["usuario"].ToString();
                string senha = ConfigurationManager.AppSettings["senha"].ToString();

                optionsBuilder.UseSqlServer("Server=tcp:srv-bd-doriga.database.windows.net,1433;Initial Catalog=BD-AZR-EUA;Persist Security Info=False;User ID=" + usuario + ";Password=" + senha + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAutor>(entity =>
            {
                entity.ToTable("tbl_autor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataCadastro)
                    .HasColumnName("data_cadastro")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Foto)
                    .IsRequired()
                    .HasColumnName("foto")
                    .HasMaxLength(50);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(50);

                entity.Property(e => e.SobreNome)
                    .IsRequired()
                    .HasColumnName("sobre_nome")
                    .HasMaxLength(50);

                entity.Property(e => e.Resumo)
                    .IsRequired()
                    .HasColumnName("resumo")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<TblCategoria>(entity =>
            {
                entity.ToTable("tbl_categoria");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataCadastro)
                    .HasColumnName("data_cadastro")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblComentario>(entity =>
            {
                entity.ToTable("tbl_comentario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comentario)
                    .IsRequired()
                    .HasColumnName("comentario")
                    .HasMaxLength(1000);

                entity.Property(e => e.DataCadastro)
                    .HasColumnName("data_cadastro")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(50);

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.TblComentario)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbl_comentario_tbl_post");
            });

            modelBuilder.Entity<TblPost>(entity =>
            {
                entity.ToTable("tbl_post");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AutorId).HasColumnName("autor_id");

                entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");

                entity.Property(e => e.Conteudo)
                    .IsRequired()
                    .HasColumnName("conteudo")
                    .HasColumnType("text");

                entity.Property(e => e.DataCadastro)
                    .HasColumnName("data_cadastro")
                    .HasColumnType("datetime");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Autor)
                    .WithMany(p => p.TblPost)
                    .HasForeignKey(d => d.AutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbl_post_tbl_autor");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.TblPost)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbl_post_tbl_categoria");
            });
        }
    }
}