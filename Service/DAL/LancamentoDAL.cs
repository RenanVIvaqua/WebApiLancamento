using System;
using Service.DML;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Service.DAL
{
    /// <summary>
    /// Classe para consultar o objeto Lacamento no banco de dados
    /// </summary>
    public class LancamentoDAL
    {
        private const string ProcedureCadastrarLancamento = "PrcCadastarLancamento";

        private const string ProcedureConsultarLancamento = "PrcConsultarLancamento";

        /// <summary>
        /// Método para inserir lançamento
        /// </summary>
        /// <param name="pLancamento">Objeto Lançamento</param>
        /// <returns>Retorna id do lançamento inserido</returns>
        public async Task<bool> InserirLancamento(Lancamento pLancamento)
        {
            List<SqlParameter> parametrosProcedure = new List<SqlParameter>();

            parametrosProcedure.Add(new SqlParameter("Valor", pLancamento.Valor));
            parametrosProcedure.Add(new SqlParameter("Descricao ", pLancamento.Descricao));
            parametrosProcedure.Add(new SqlParameter("Conta", pLancamento.Conta));
            parametrosProcedure.Add(new SqlParameter("Tipo", pLancamento.Tipo.ToString()));            
            parametrosProcedure.Add(new SqlParameter("Data", pLancamento.Data));

            return await new DataBaseAccess(ProcedureCadastrarLancamento, parametrosProcedure).Executar();
        }

        /// <summary>
        /// Método para consultar todos os lançamento na data
        /// </summary>
        /// <param name="pData">Data de Lançamento</param>
        /// <returns>Lista de lançamentos</returns>
        public async Task<List<Lancamento>> ConsultarLancamento(DateTime pData)
        {
            List<SqlParameter> parametrosProcedure = new List<SqlParameter>();
            parametrosProcedure.Add(new SqlParameter("Data", pData));

            var retorno = await new DataBaseAccess(ProcedureConsultarLancamento, parametrosProcedure).Consultar();

            var listLancamentos = new List<Lancamento>();

            for (int i = 0; i < retorno.Rows.Count; i++)
            {
                var lancamento = new Lancamento();

                if (retorno.Rows[i]["id"] != DBNull.Value)
                    lancamento.Id = Convert.ToInt32(retorno.Rows[i]["id"]);

                if (retorno.Rows[i]["valor"] != DBNull.Value)
                    lancamento.Valor = Convert.ToDecimal(retorno.Rows[i]["valor"]);

                if (retorno.Rows[i]["descricao"] != DBNull.Value)
                    lancamento.Descricao = Convert.ToString(retorno.Rows[i]["descricao"]);

                if (retorno.Rows[i]["conta"] != DBNull.Value)
                    lancamento.Conta = Convert.ToString(retorno.Rows[i]["conta"]);

                if (retorno.Rows[i]["tipo"] != DBNull.Value)                
                   lancamento.Tipo = Enum.TryParse<TipoConta>(retorno.Rows[i]["tipo"].ToString(), false, out var tipo) ? (char)tipo: (char)TipoConta.N;
                
                if (retorno.Rows[i]["data"] != DBNull.Value)
                    lancamento.Data = Convert.ToDateTime(retorno.Rows[i]["data"]);

                listLancamentos.Add(lancamento);
            }
            return listLancamentos;
        }
    }
}