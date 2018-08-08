using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Dados.Models;
using Blog.Dados;

namespace Blog.Negocio
{
    public class CategoriaNegocio
    {
        public CategoriaNegocio()
        {
        }

        public List<TblCategoria> ListarCategorias(){

            CategoriaDados categoriaDados = new CategoriaDados();

            return categoriaDados.ListarCategorias();

        } 

        public void Inserir(TblCategoria categoria){

            CategoriaDados categoriaDados = new CategoriaDados();

            try
            {
                categoriaDados.Inserir(categoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Atualizar(TblCategoria categoria)
        {

            CategoriaDados categoriaDados = new CategoriaDados();

            categoriaDados.Ataulizar(categoria);

        }

        public TblCategoria Buscar(TblCategoria categoria){
            
            CategoriaDados categoriaDados = new CategoriaDados();

            return categoriaDados.Buscar(categoria);
        }

        public void Excluir(TblCategoria categoria)
        {

            CategoriaDados categoriaDados = new CategoriaDados();

            categoriaDados.Excluir(categoria);

        }       

    }
}
