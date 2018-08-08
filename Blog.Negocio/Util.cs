using System;
using Blog.Negocio.Enumeradores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Negocio
{
    public class Util : Controller
    {
        public Util()
        {
        }


        /// <summary>
        /// Limpa a memsagem da View
        /// </summary>
        public void LimparMensagem()
        {
            TempData.Remove("MensagemErro");
            TempData.Remove("MensagemSucesso");
            TempData.Remove("MensagemAlerta");
            TempData.Remove("MensagemInfo");
        }

        /// <summary>
        /// Monta os dados que serão utlizados na View de Mensagem
        /// </summary>
        /// <param name="Mensagem"></param>
        /// <param name="TipoMesangem"></param>
        /// <param name="CodigoErro"></param>
        public void ExibirMensagem(string Mensagem, ETipoMensagem TipoMesangem, int CodigoErro)
        {
            Response.StatusCode = CodigoErro;

            switch (TipoMesangem)
            {
                case ETipoMensagem.Erro:
                    TempData["MensagemErro"] = Mensagem;
                    TempData.Keep("MensagemErro");
                    break;
                case ETipoMensagem.Sucesso:
                    TempData["MensagemSucesso"] = Mensagem;
                    TempData.Keep("MensagemSucesso");
                    break;
                case ETipoMensagem.Alerta:
                    TempData["MensagemAlerta"] = Mensagem;
                    TempData.Keep("MensagemAlerta");
                    break;
                case ETipoMensagem.Informacao:
                    TempData["MensagemInfo"] = Mensagem;
                    TempData.Keep("MensagemInfo");
                    break;
            }
            TempData["ExibirMensagem"] = "S";
        }
    }
}
