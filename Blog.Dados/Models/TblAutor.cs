using System;
using System.Collections.Generic;

namespace Blog.Dados.Models
{
    public partial class TblAutor
    {
        public TblAutor()
        {
            TblPost = new HashSet<TblPost>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Resumo { get; set; }
        public string Foto { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string SobreNome { get; set; }

        public ICollection<TblPost> TblPost { get; set; }
    }
}
