using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.BLL;
using Microsoft.AspNetCore.Http;

namespace WebApiConsultarLancamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentoController : ControllerBase
    {
        [HttpGet("{Data}",Name = "Data")]
        public async Task<IActionResult> Get(DateTime Data)
        {
            try
            {
                var retorno = await new BoLancamento().ConsultarLancamento(Data);
                return Ok(retorno);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        } 
    }
}
