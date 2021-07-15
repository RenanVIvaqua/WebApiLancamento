using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCadastroLancamento.Model;
using Service.BLL;
using Service.DML;
using Microsoft.AspNetCore.Http;

namespace WebApiCadastroLancamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentosController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(LancamentoModel lancamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(!Enum.TryParse<TipoConta>(lancamento.Tipo.ToUpper(), out TipoConta tipo)) 
                        return StatusCode(StatusCodes.Status500InternalServerError, "Campo tipo só é permitido 'D' ou 'C'");

                    bool retorno = await new BoLancamento().CadastrarLancamento(ConverterModel(lancamento, tipo));

                    if(retorno)
                        return Ok("Gravação realizado com sucesso!");
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError, "Não foi possível realizar a gravação!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
                }
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private Lancamento ConverterModel(LancamentoModel pLancamentoModel, TipoConta pTipo)
        {
            return new Lancamento()
            {
                Conta = pLancamentoModel.Conta,
                Descricao = pLancamentoModel.Descricao,
                Tipo = (char)pTipo,
                Data = pLancamentoModel.Data,
                Valor = pLancamentoModel.Valor
            };
        }
    }
}
