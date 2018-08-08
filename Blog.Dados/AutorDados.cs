using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Dados.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Dados
{
    public class AutorDados
    {
        public AutorDados()
        {
        }

        public List<TblAutor> ListarAutores(){
            
            List<TblAutor> Autores = new List<TblAutor>();

            using (ContextBlog con = new ContextBlog()){
                Autores = con.TblAutor.ToList();
            }

            return Autores;
        } 

        public void Inserir(TblAutor autor){

            try
            {
                using (ContextBlog con = new ContextBlog())
                {
                    con.TblAutor.Add(autor);
                    con.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Ataulizar(TblAutor autor){

            try
            {
                using (ContextBlog con = new ContextBlog())
                {
                    
                    con.Entry(autor).State = EntityState.Modified;

                    if(string.IsNullOrEmpty(autor.Foto))
                        con.Entry(autor).Property(p => p.Foto).IsModified = false;

                    con.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public TblAutor Buscar(TblAutor autor){

            try
            {

                TblAutor autorRetorno = new TblAutor();

                using(ContextBlog con = new ContextBlog()){
                    autorRetorno = con.TblAutor.Where(p => p.Id == autor.Id).FirstOrDefault();
                }

                return autorRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Excluir(TblAutor autor){
            
            try
            {
                using (ContextBlog con = new ContextBlog())
                {
                    con.Entry(autor).State = autor.Id == 0 ?
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
