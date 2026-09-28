using aula17082026.Components.Pages.Processo;
using aula17082026.Configs;
using aula17082026.Models;

namespace aula17082026.DAO
{


    public class ProcessoDAO
    {
        private readonly Conexao _conexao;

        public ProcessoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<Processo> Listar()
        {
            try
            {
                var lista = new List<Processo>();

                // Buscando a Conexão com o banco de dados
                using var con = _conexao.GetConnection();


                string sql = "SELECT * FROM processos";
                using var comando = con.CreateCommand();
                comando.CommandText = sql;

                using var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    var processo = new Processo();
                    processo.Id = leitor.GetInt32("id_pro");
                    processo.Numero = leitor.GetString("numero_pro");
                    processo.Interessado = leitor.GetString("interessado_pro");
                    processo.Assunto = leitor.GetString("assunto_pro");
                    processo.Descricao = leitor.GetString("descricao_pro");
                    processo.Situacao = leitor.GetString("situacao_pro");
                    processo.Data = DateOnly.FromDateTime(leitor.GetDateTime(leitor.GetOrdinal("data_pro")));
                    //processo.Data = ["data_pro"];
                    lista.Add(processo);
                }


                return lista;
            }
            catch
            {
                throw;
            }
        }
        

        public void Inserir(Processo processo)
        {
            try
            {
                using var con = _conexao.GetConnection();
                string sql = @"INSERT INTO processos
                (numero_pro, data_pro, interessado_pro, assunto_pro, descricao_pro, situacao_pro)
                VALUES
                (@Numero, @Data, @Interessado, @Assunto, @Descricao, @Situacao)";
               
                using var comando = con.CreateCommand();
                comando.CommandText = sql;
                comando.Parameters.AddWithValue("@Numero",
                processo.Numero);

                comando.Parameters.AddWithValue("@Data", processo.Data!.Value.ToDateTime(TimeOnly.MinValue));
                comando.Parameters.AddWithValue("@Interessado", processo.Interessado);
           
                comando.Parameters.AddWithValue("@Assunto", processo.Assunto);
                comando.Parameters.AddWithValue("@Descricao", processo.Descricao);
               
                comando.Parameters.AddWithValue("@Situacao", processo.Situacao);
               
                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


    }
}
