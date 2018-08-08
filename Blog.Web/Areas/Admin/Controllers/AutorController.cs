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
    public class AutorController : Util
    {
        // GET: /<controller>/
        public IActionResult Index()
        {

            TblAutor autor = new TblAutor();

            return View(autor);
        }

        private readonly IHostingEnvironment _hostingEnvironment;

        public AutorController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Listar(TblAutor Autor, int? numPagina)
        {

            AutorNegocio autorNegocio = new AutorNegocio();
            List<TblAutor> Autores = new List<TblAutor>();

            Autores = autorNegocio.ListarAutores();

            //Retorna a lista páginada na tela
            return PartialView("PartialLista", Autores);
        }

        public ActionResult Salvar(TblAutor autor, IFormFile Foto, string ConfirmarEmail)
        {
            try
            {
                AutorNegocio autorNegocio = new AutorNegocio();

                if (Foto != null)
                {
                    if (!string.IsNullOrEmpty(Foto.FileName))
                    {
                        string Caminho = _hostingEnvironment.WebRootPath + "/images/Fotos/" + Foto.FileName;

                        using (var stream = new FileStream(Caminho, FileMode.Create))
                        {
                            Foto.CopyToAsync(stream);
                        }

                        autor.Foto = Foto.FileName;
                    }
                }


                #region Validação dos campos (Servidor)

                // Validação do Nome
                if (autor.Nome == null)
                {
                    ExibirMensagem("O Nome é obrigatório", ETipoMensagem.Alerta, 99);
                    return PartialView("_Mensagem");
                }

                // Validação da Sobrenome
                if (autor.SobreNome == null)
                {
                    ExibirMensagem("O Sobrenome é obrigatório", ETipoMensagem.Alerta, 99);
                    return PartialView("_Mensagem");
                }

                // Validação do Email
                if (autor.Email == null)
                {
                    ExibirMensagem("O Email é obrigatório", ETipoMensagem.Alerta, 99);
                    return PartialView("_Mensagem");
                }

                // Validação dos emails informados
                if (autor.Email != ConfirmarEmail)
                {
                    ExibirMensagem("Os emails informados não são iguais", ETipoMensagem.Alerta, 99);
                    return PartialView("_Mensagem");
                }

                // Validação do Resumo
                if (autor.Resumo == null)
                {
                    ExibirMensagem("O Resumo é obrigatório", ETipoMensagem.Alerta, 99);
                    return PartialView("_Mensagem");
                }

                #endregion

                //Data de Cadastro
                autor.DataCadastro = DateTime.Now;

                if (autor.Id > 0)
                {
                    autorNegocio.Atualizar(autor);
                    ExibirMensagem("Autor alterado com sucesso", ETipoMensagem.Sucesso, 200);
                }
                else
                {
                    autorNegocio.Inserir(autor);
                    ExibirMensagem("Autor cadastrado com sucesso", ETipoMensagem.Sucesso, 200);
                }

                return PartialView("_Mensagem");
            }
            catch (Exception ex)
            {
                ExibirMensagem(ex.Message, ETipoMensagem.Erro, 99);
                return PartialView("_Mensagem");
            }



        }

        public ActionResult Buscar(TblAutor autor)
        {

            try
            {

                AutorNegocio autorNegocio = new AutorNegocio();

                return PartialView("PartialForm", autorNegocio.Buscar(autor));

            }
            catch (Exception ex)
            {
                ExibirMensagem(ex.Message, ETipoMensagem.Erro, 99);
                return PartialView("_Mensagem");
            }
        }

        public ActionResult Excluir(TblAutor autor){

            try
            {
                AutorNegocio autorNegocio = new AutorNegocio();

                //Exclui o Autor informado
                autorNegocio.Excluir(autor);

                //Retorna a mesagem de sucesso
                ExibirMensagem("Autor excluído com sucesso", ETipoMensagem.Sucesso, 200);

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
