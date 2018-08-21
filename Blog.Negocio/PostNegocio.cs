using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Dados.Models;
using Blog.Dados;

namespace Blog.Negocio
{
    public class PostNegocio
    {
        public PostNegocio()
        {
        }

        public List<TblPost> ListarPosts(){

            PostDados categoriaDados = new PostDados();

            return categoriaDados.ListarPosts();

        } 

        public void Inserir(TblPost categoria){

            PostDados categoriaDados = new PostDados();

            try
            {
                categoriaDados.Inserir(categoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Atualizar(TblPost categoria)
        {

            PostDados categoriaDados = new PostDados();

            categoriaDados.Ataulizar(categoria);

        }

        public TblPost Buscar(TblPost categoria){
            
            PostDados categoriaDados = new PostDados();

            return categoriaDados.Buscar(categoria);
        }

        public void Excluir(TblPost categoria)
        {

            PostDados categoriaDados = new PostDados();

            categoriaDados.Excluir(categoria);

        }       

    }
}
