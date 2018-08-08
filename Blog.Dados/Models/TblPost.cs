using System;
using System.Collections.Generic;

namespace Blog.Dados.Models
{
    public partial class TblPost
    {
        public TblPost()
        {
            TblComentario = new HashSet<TblComentario>();
        }

        public int Id { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string FotoDestaque { get; set; }

        public TblAutor Autor { get; set; }
        public TblCategoria Categoria { get; set; }
        public ICollection<TblComentario> TblComentario { get; set; }
    }
}
