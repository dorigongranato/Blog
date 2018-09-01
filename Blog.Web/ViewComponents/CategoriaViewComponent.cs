using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using Blog.Negocio;
using Blog.Dados.Models;

namespace Blog.Web.ViewComponents
{
    public class CategoriaViewComponent : ViewComponent
    {
        public CategoriaViewComponent(){
            
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            return View( categoriaNegocio.ListarCategorias());
        }
    }
}
