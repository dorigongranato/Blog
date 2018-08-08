using System;
using System.Collections.Generic;

namespace Blog.Dados.Models
{
    public partial class TblComentario
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Comentario { get; set; }
        public DateTime DataCadastro { get; set; }

        public TblPost Post { get; set; }
    }
}
