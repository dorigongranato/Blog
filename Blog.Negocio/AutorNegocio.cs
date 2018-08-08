using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Dados.Models;
using Blog.Dados;

namespace Blog.Negocio
{
    public class AutorNegocio
    {
        public AutorNegocio()
        {
        }

        public List<TblAutor> ListarAutores(){

            AutorDados autorDados = new AutorDados();

            return autorDados.ListarAutores();

        } 

        public void Inserir(TblAutor autor){

            AutorDados autorDados = new AutorDados();

            try
            {
                autorDados.Inserir(autor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Atualizar(TblAutor autor)
        {

            AutorDados autorDados = new AutorDados();

            autorDados.Ataulizar(autor);

        }

        public TblAutor Buscar(TblAutor autor){
            
            AutorDados autorDados = new AutorDados();

            return autorDados.Buscar(autor);
        }

        public void Excluir(TblAutor autor)
        {

            AutorDados autorDados = new AutorDados();

            autorDados.Excluir(autor);

        }       

    }
}
