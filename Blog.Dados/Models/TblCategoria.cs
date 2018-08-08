using System;
using System.Collections.Generic;

namespace Blog.Dados.Models
{
    public partial class TblCategoria
    {
        public TblCategoria()
        {
            TblPost = new HashSet<TblPost>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataCadastro { get; set; }

        public ICollection<TblPost> TblPost { get; set; }
    }
}
