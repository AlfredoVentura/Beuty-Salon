using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

public class PessoaDAL
{
    private const string ConnectionString = "Data Source=DESKTOP-LUGRJRA;Initial Catalog=salao;Integrated Security=True;TrustServerCertificate=True;Encrypt=False";

    // Método de Inserção (CRUD C) - Específico para Cliente
    public int InserirCliente(Cliente novoCliente)
    {
        // A query insere na tabela Pessoa e usa 'CLIENTE' como discriminador.
        string sql = "INSERT INTO Pessoa (Nome, Telefone, Endereco, TipoPessoa) " +
                     "VALUES (@Nome, @Telefone, @Endereco, 'CLIENTE'); SELECT SCOPE_IDENTITY();";

        using (SqlConnection cn = new SqlConnection(ConnectionString))
        using (SqlCommand cm = new SqlCommand(sql, cn))
        {
            // Parâmetros dos atributos comuns e específicos de Cliente
            cm.Parameters.AddWithValue("@Nome", novoCliente.Nome);
            cm.Parameters.AddWithValue("@Telefone", novoCliente.Telefone ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@Endereco", novoCliente.Endereco ?? (object)DBNull.Value);

            try
            {
                cn.Open();
                object id = cm.ExecuteScalar();
                return id != null ? Convert.ToInt32(id) : 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro SQL na Inserção: " + ex.Message);
                throw; // Lança a exceção para que o Forms possa tratá-la
            }
        }
    }

    // Método de Pesquisa (CRUD R) - Específico para Cliente
    public List<Cliente> PesquisarClientes()
    {
        List<Cliente> listaClientes = new List<Cliente>();
        // Filtra pelo discriminador 'CLIENTE' e busca todos os campos relevantes.
        string sql = "SELECT Id, Nome, Telefone, Endereco FROM Pessoa WHERE TipoPessoa = 'CLIENTE'";

        using (SqlConnection cn = new SqlConnection(ConnectionString))
        using (SqlCommand cm = new SqlCommand(sql, cn))
        {
            try
            {
                cn.Open();
                using (SqlDataReader reader = cm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente();

                        // Mapeamento dos atributos comuns (herdados)
                        cliente.Id = (int)reader["Id"];
                        cliente.Nome = reader["Nome"].ToString();

                        // Mapeamento dos atributos específicos de Cliente
                        cliente.Telefone = reader["Telefone"] == DBNull.Value ? string.Empty : reader["Telefone"].ToString();
                        cliente.Endereco = reader["Endereco"] == DBNull.Value ? string.Empty : reader["Endereco"].ToString();

                        listaClientes.Add(cliente);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro SQL na Pesquisa: " + ex.Message);
                throw;
            }
        }
        return listaClientes;
    }

    // Método de Alteração (CRUD U)
    public int AtualizarCliente(Cliente cliente)
    {
        // Atualiza campos comuns e específicos de Cliente na tabela Pessoa
        string sql = "UPDATE Pessoa SET Nome = @Nome, Telefone = @Telefone, Endereco = @Endereco " +
                     "WHERE Id = @Id AND TipoPessoa = 'CLIENTE'";

        using (SqlConnection cn = new SqlConnection(ConnectionString))
        using (SqlCommand cm = new SqlCommand(sql, cn))
        {
            cm.Parameters.AddWithValue("@Nome", cliente.Nome);
            cm.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@Endereco", cliente.Endereco ?? (object)DBNull.Value);
            cm.Parameters.AddWithValue("@Id", cliente.Id);

            cn.Open();
            return cm.ExecuteNonQuery();
        }
    }

    // Método de Deleção (CRUD D)
    public int ExcluirCliente(int clienteId)
    {
        // Deleta o registro garantindo que o tipo é 'CLIENTE' (boa prática de segurança)
        string sql = "DELETE FROM Pessoa WHERE Id = @Id AND TipoPessoa = 'CLIENTE'";

        using (SqlConnection cn = new SqlConnection(ConnectionString))
        using (SqlCommand cm = new SqlCommand(sql, cn))
        {
            cm.Parameters.AddWithValue("@Id", clienteId);

            cn.Open();
            return cm.ExecuteNonQuery();
        }
    }
}