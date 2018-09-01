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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;
using System.Net;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Util
    {
        // GET: /<controller>/
        public IActionResult Index()
        {

            TblPost post = new TblPost();

            //Guarda os dados para montar os combos
            this.ListarAutores();
            this.ListarCategorias();

            return View(post);
        }

        private readonly IHostingEnvironment _hostingEnvironment;

        public PostController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Listar(TblPost Post, int? numPagina)
        {

            PostNegocio postNegocio = new PostNegocio();
            List<TblPost> Posts = new List<TblPost>();

            Posts = postNegocio.ListarPosts();

            //Retorna a lista páginada na tela
            return PartialView("PartialLista", Posts);
        }

        public ActionResult Salvar(TblPost post, string ConteudoHTML)
        {
            try
            {
                PostNegocio postNegocio = new PostNegocio();

                #region Validação dos campos (Servidor)

                // Validação do Nome
                //if (post.Nome == null)
                //{
                //    ExibirMensagem("O Nome é obrigatório", ETipoMensagem.Alerta, 99);
                //    return PartialView("_Mensagem");
                //}

                //// Validação da Sobrenome
                //if (post.SobreNome == null)
                //{
                //    ExibirMensagem("O Sobrenome é obrigatório", ETipoMensagem.Alerta, 99);
                //    return PartialView("_Mensagem");
                //}

                //// Validação do Email
                //if (post.Email == null)
                //{
                //    ExibirMensagem("O Email é obrigatório", ETipoMensagem.Alerta, 99);
                //    return PartialView("_Mensagem");
                //}

                //// Validação dos emails informados
                //if (post.Email != ConfirmarEmail)
                //{
                //    ExibirMensagem("Os emails informados não são iguais", ETipoMensagem.Alerta, 99);
                //    return PartialView("_Mensagem");
                //}

                //// Validação do Resumo
                //if (post.Resumo == null)
                //{
                //    ExibirMensagem("O Resumo é obrigatório", ETipoMensagem.Alerta, 99);
                //    return PartialView("_Mensagem");
                //}

                #endregion

                //Data de Cadastro
                post.DataCadastro = DateTime.Now;

                string teste = string.Empty;

                //HttpUtility.HtmlDecode(post.Conteudo, teste);

                post.Conteudo = WebUtility.HtmlDecode(ConteudoHTML);

                if (post.Id > 0)
                {
                    postNegocio.Atualizar(post);
                    ExibirMensagem("Post alterado com sucesso", ETipoMensagem.Sucesso, 200);
                }
                else
                {
                    postNegocio.Inserir(post);
                    ExibirMensagem("Post cadastrado com sucesso", ETipoMensagem.Sucesso, 200);
                }

                return PartialView("_Mensagem");
            }
            catch (Exception ex)
            {
                ExibirMensagem(ex.Message, ETipoMensagem.Erro, 99);
                return PartialView("_Mensagem");
            }

        }

        public ActionResult Buscar(TblPost post)
        {

            try
            {

                this.ListarAutores();
                this.ListarCategorias();

                PostNegocio postNegocio = new PostNegocio();

                return PartialView("PartialForm", postNegocio.Buscar(post));

            }
            catch (Exception ex)
            {
                ExibirMensagem(ex.Message, ETipoMensagem.Erro, 99);
                return PartialView("_Mensagem");
            }
        }

        public ActionResult Excluir(TblPost post){

            try
            {
                PostNegocio postNegocio = new PostNegocio();

                //Exclui o Post informado
                postNegocio.Excluir(post);

                //Retorna a mesagem de sucesso
                ExibirMensagem("Post excluído com sucesso", ETipoMensagem.Sucesso, 200);

                return PartialView("_Mensagem");
            }
            catch (Exception ex)
            {

                //Mensagem de erro do sistema
                ExibirMensagem(ex.Message, ETipoMensagem.Erro, 99);
                return PartialView("_Mensagem"); ;
            }

        }

        public void ListarAutores(){

            if (TempData["tdAutores"] != null){
                TempData["tdAutores"] = JsonConvert.DeserializeObject<List<TblAutor>>(TempData["tdAutores"].ToString());

            }else{
                AutorNegocio autorNegocio = new AutorNegocio();

                TempData["tdAutores"] = JsonConvert.SerializeObject(autorNegocio.ListarAutores());
            }

            TempData.Keep();

            ViewBag.Autores = JsonConvert.DeserializeObject<List<TblAutor>>(TempData["tdAutores"].ToString());

        }

        private void ListarCategorias()
        {
            if (TempData["tdCategorias"] != null)
            {
                TempData["tdCategorias"] = JsonConvert.DeserializeObject<List<TblAutor>>(TempData["tdCategorias"].ToString());
            }
            else
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                TempData["tdCategorias"] = JsonConvert.SerializeObject(categoriaNegocio.ListarCategorias());
            }

            TempData.Keep();

            ViewBag.Categorias = JsonConvert.DeserializeObject<List<TblAutor>>(TempData["tdCategorias"].ToString());

        }

    }
}
