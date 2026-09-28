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

                    //processo.Data = leitor["data_pro];

                    lista.Add(processo);
                }


                return lista;
            } catch
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
                (numero_pro, data_pro, interessado_pro,
                assunto_pro, descricao_pro, situacao_pro)
                VALUES
                (@numero, @data, @interessado, @assunt
                o, @descricao, @situacao)";
                //Passo a passo — Tela de Cadastro de Processo 2
                using var comando = con.CreateCommand();
                comando.CommandText = sql;
                comando.Parameters.AddWithValue("@numero",
                processo.Numero);
                comando.Parameters.AddWithValue("@data", processo.Data!.Value.ToDateTime(TimeOnly.MinValue));
                comando.Parameters.AddWithValue("@interessado ", processo.Interessado);
           
                comando.Parameters.AddWithValue("@assunto",processo.Assunto);
                comando.Parameters.AddWithValue("@descricao", processo.Descricao);
               
                comando.Parameters.AddWithValue("@situacao", processo.Situacao);
               
                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

    }
}
