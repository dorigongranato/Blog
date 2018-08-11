using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Negocio;
using Blog.Dados.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Blog.Negocio.Enumeradores;


namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Util
    {
        // GET: /<controller>/
        public IActionResult Index()
        {

            TblCategoria categoria = new TblCategoria();

            return View(categoria);
        }

        private readonly IHostingEnvironment _hostingEnvironment;

        public CategoriaController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Listar(TblCategoria categoria, int? numPagina)
        {

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<TblCategoria> categorias = new List<TblCategoria>();

            categorias = categoriaNegocio.ListarCategorias();

            //Retorna a lista páginada na tela
            return PartialView("PartialLista", categorias);
        }

        public ActionResult Salvar(TblCategoria categoria)
        {
            try
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                #region Validação dos campos (Servidor)

                // Validação do Nome
                if (categoria.Nome == null)
                {
                    ExibirMensagem("A categoria é obrigatório", ETipoMensagem.Alerta, 99);
                    return PartialView("_Mensagem");
                }


                #endregion

                //Data de Cadastro
                categoria.DataCadastro = DateTime.Now;

                if (categoria.Id > 0)
                {
                    categoriaNegocio.Atualizar(categoria);
                    ExibirMensagem("Categoria alterado com sucesso", ETipoMensagem.Sucesso, 200);
                }
                else
                {
                    categoriaNegocio.Inserir(categoria);
                    ExibirMensagem("Categoria cadastrado com sucesso", ETipoMensagem.Sucesso, 200);
                }

                return PartialView("_Mensagem");
            }
            catch (Exception ex)
            {
                ExibirMensagem(ex.Message, ETipoMensagem.Erro, 99);
                return PartialView("_Mensagem");
            }



        }

        public ActionResult Buscar(TblCategoria categoria)
        {

            try
            {

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                return PartialView("PartialForm", categoriaNegocio.Buscar(categoria));

            }
            catch (Exception ex)
            {
                ExibirMensagem(ex.Message, ETipoMensagem.Erro, 99);
                return PartialView("_Mensagem");
            }
        }

        public ActionResult Excluir(TblCategoria categoria){

            try
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                //Exclui o categoria informado
                categoriaNegocio.Excluir(categoria);

                //Retorna a mesagem de sucesso
                ExibirMensagem("Categoria excluído com sucesso", ETipoMensagem.Sucesso, 200);

                return PartialView("_Mensagem");
            }
            catch (Exception ex)
            {

                //Mensagem de erro do sistema
                ExibirMensagem(ex.Message, ETipoMensagem.Erro, 99);
                return PartialView("_Mensagem"); ;
            }

        }

    }
}
