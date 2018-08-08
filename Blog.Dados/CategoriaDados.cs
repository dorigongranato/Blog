using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Dados.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Dados
{
    public class CategoriaDados
    {
        public CategoriaDados()
        {
        }

        public List<TblCategoria> ListarCategorias(){
            
            List<TblCategoria> Categorias = new List<TblCategoria>();

            using (ContextBlog con = new ContextBlog()){
                Categorias = con.TblCategoria.ToList();
            }

            return Categorias;
        } 

        public void Inserir(TblCategoria categoria){

            try
            {
                using (ContextBlog con = new ContextBlog())
                {
                    con.TblCategoria.Add(categoria);
                    con.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Ataulizar(TblCategoria categoria){

            try
            {
                using (ContextBlog con = new ContextBlog())
                {
                    
                    con.Entry(categoria).State = EntityState.Modified;

                    //if(string.IsNullOrEmpty(categoria.Foto))
                        //con.Entry(autor).Property(p => p.Foto).IsModified = false;

                    con.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public TblCategoria Buscar(TblCategoria categoria){

            try
            {

                TblCategoria categoriaRetorno = new TblCategoria();

                using(ContextBlog con = new ContextBlog()){
                    categoriaRetorno = con.TblCategoria.Where(p => p.Id == categoria.Id).FirstOrDefault();
                }

                return categoriaRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Excluir(TblCategoria categoria){
            
            try
            {
                using (ContextBlog con = new ContextBlog())
                {
                    con.Entry(categoria).State = categoria.Id == 0 ?
                        EntityState.Added :
                        EntityState.Deleted;

                    con.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
