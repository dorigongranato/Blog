using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Dados.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Blog.Dados
{
    public class PostDados
    {
        public PostDados()
        {
        }

        public List<TblPost> ListarPosts(){
            
            List<TblPost> Posts = new List<TblPost>();


                using (ContextBlog con = new ContextBlog())
                {
                    Posts = con.TblPost.Include(m => m.Autor)
                               .Include(m => m.Categoria).ToList();
                }
           

            return Posts;
        } 

        public void Inserir(TblPost categoria){

            try
            {
                using (ContextBlog con = new ContextBlog())
                {
                    con.TblPost.Add(categoria);
                    con.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Ataulizar(TblPost categoria){

            try
            {
                using (ContextBlog con = new ContextBlog())
                {
                    
                    con.Entry(categoria).State = EntityState.Modified;

                    con.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public TblPost Buscar(TblPost categoria){

            try
            {

                TblPost categoriaRetorno = new TblPost();

                using(ContextBlog con = new ContextBlog()){
                    categoriaRetorno = con.TblPost.Where(p => p.Id == categoria.Id).FirstOrDefault();
                }

                return categoriaRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Excluir(TblPost categoria){
            
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
