using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Service.DAL
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class DataBaseAccess
    {
        private SqlCommand SqlCommand;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pNomeProcedureExecutar">Nome da procedure a ser executado</param>
        /// <param name="pParametros">Parametros da procedure</param>
        public DataBaseAccess(string pNomeProcedureExecutar, List<SqlParameter> pParametros = null)
        {           
            SqlCommand = new SqlCommand()
            {
                Connection = new SqlConnection(StringConexao()),
                CommandType = CommandType.StoredProcedure,
                CommandText = pNomeProcedureExecutar,
            };

            foreach (var item in pParametros)
                SqlCommand.Parameters.Add(item);
        }

        private string StringConexao()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "den1.mssql8.gear.host",
                UserID = "dbtribuno",
                Password = "So7M_mo80cb~"              
            };

            return builder.ToString();
        }

        /// <summary>
        /// Método para executar uma procedure
        /// </summary>  
        internal async Task<bool> Executar()
        {
            SqlCommand.Connection.Open();
            try
            {
                var retorno = await SqlCommand.ExecuteReaderAsync();

                if (retorno != null && retorno.RecordsAffected > 0)
                    return true;
                else
                    return false;
            }
            finally
            {
                SqlCommand.Connection.Close();
            }
        }

        /// <summary>
        /// Método para Consultar o banco de dados 
        /// </summary>   
        /// <returns>Resultado da cosulta</returns>
        internal async Task<DataTable> Consultar()
        {
            SqlDataAdapter adapter = new SqlDataAdapter(SqlCommand);
            DataTable ds = new DataTable();

            await SqlCommand.Connection.OpenAsync();

            try
            {
                adapter.Fill(ds);
            }
            finally
            {
                SqlCommand.Connection.Close();
            }
            return ds;
        }
    }
}