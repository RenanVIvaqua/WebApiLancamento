using System;
using Service.DML;
using Service.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Service.BLL
{
    /// <summary>
    /// Classe de negócio Lançamentos
    /// </summary>
    public class BoLancamento
    {
        /// <summary>
        /// Método para cadastrar um Lançamento
        /// </summary>
        /// <param name="pLancamento">Objeto Lançamento</param>
        public async Task<bool> CadastrarLancamento(Lancamento pLancamento)
        {
            return await new LancamentoDAL().InserirLancamento(pLancamento);
        }

        /// <summary>
        /// Método para consultar lançamento
        /// </summary>
        /// <param name="pData">Data do lançamento</param>
        public async Task<Totalizador> ConsultarLancamento(DateTime pData)
        {
            var retorno = await new LancamentoDAL().ConsultarLancamento(pData);
            return GerarTotalizador(retorno);
        }

        private Totalizador GerarTotalizador(List<Lancamento> pLancamentos) 
        {
            Dictionary<string, decimal> total = new Dictionary<string, decimal>();

            foreach (var item in pLancamentos) 
            {
                var valor = (from x in pLancamentos where x.Conta == item.Conta select x.Valor).Sum();

                if(item.Conta != null && !total.ContainsKey(item.Conta))
                    total.Add(item.Conta, valor);
            }

            total.Add("Saldo", (from x in total select x.Value).Sum());

            var totalizador = new Totalizador()
            {
                Totalizadores = total,
                Lancamentos = pLancamentos
            };

            return totalizador;
        }
    }
}