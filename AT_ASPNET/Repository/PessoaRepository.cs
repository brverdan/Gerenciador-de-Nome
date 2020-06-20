using AT_ASPNET.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AT_ASPNET.Repository
{
    public class PessoaRepository
    {
        private string ConnectionString { get; set; }

        public PessoaRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("PessoaConnection");
        }
        public void Save(Pessoa pessoa)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = @" INSERT INTO Pessoa(NomePessoa, SobrenomePessoa, DataDeAniversario)
                             VALUES (@P1, @P2, @P3)
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", pessoa.Nome);
                sqlCommand.Parameters.AddWithValue("P2", pessoa.Sobrenome);
                sqlCommand.Parameters.AddWithValue("P3", pessoa.DataDeAniversario);

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(Pessoa pessoa)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var sql = @" UPDATE Pessoa 
                             SET NomePessoa = @P1,
                             SobrenomePessoa = @P2,
                             DataDeAniversario = @P3
                             WHERE  Id = @P4 
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", pessoa.Nome);
                sqlCommand.Parameters.AddWithValue("P2", pessoa.Sobrenome);
                sqlCommand.Parameters.AddWithValue("P3", pessoa.DataDeAniversario);
                sqlCommand.Parameters.AddWithValue("P4", pessoa.Id);
                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var sql = @" DELETE FROM Pessoa
                             WHERE Id = @P1 
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", id);
                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<Pessoa> GetAll()
        {
            List<Pessoa> result = new List<Pessoa>();

            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var sql = @" SELECT Id, NomePessoa, SobrenomePessoa, DataDeAniversario FROM Pessoa";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Pessoa pessoa = new Pessoa()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["NomePessoa"].ToString(),
                        Sobrenome = reader["SobrenomePessoa"].ToString(),
                        DataDeAniversario = Convert.ToDateTime(reader["DataDeAniversario"])
                    };

                    result.Add(pessoa);
                }

                connection.Close();
            }

            return result;
        }

        public Pessoa GetById(int id)
        {
            List<Pessoa> result = new List<Pessoa>();

            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var sql = @" SELECT Id, NomePessoa, SobrenomePessoa, DataDeAniversario FROM Pessoa
                             WHERE Id = @P1
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", id);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Pessoa pessoa = new Pessoa()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["NomePessoa"].ToString(),
                        Sobrenome = reader["SobrenomePessoa"].ToString(),
                        DataDeAniversario = Convert.ToDateTime(reader["DataDeAniversario"])
                    };

                    result.Add(pessoa);
                }

                connection.Close();
            }

            return result.FirstOrDefault();
        }

        public List<Pessoa> GetByName(string name)
        {
            List<Pessoa> result = new List<Pessoa>();

            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var sql = @" SELECT Id, NomePessoa, SobrenomePessoa, DataDeAniversario FROM Pessoa
                             WHERE NomePessoa LIKE @P1 COLLATE SQL_Latin1_General_CP1_CI_AI OR SobrenomePessoa LIKE @P1 COLLATE SQL_Latin1_General_CP1_CI_AI
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", name);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Pessoa pessoa = new Pessoa()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["NomePessoa"].ToString(),
                        Sobrenome = reader["SobrenomePessoa"].ToString(),
                        DataDeAniversario = Convert.ToDateTime(reader["DataDeAniversario"])
                    };

                    result.Add(pessoa);
                }

                connection.Close();
            }

            return result;
        }

        public List<Pessoa> GetTodayBirthday()
        {
            List<Pessoa> result = new List<Pessoa>();
            DateTime date = DateTime.Today;

            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var sql = @" SELECT Id, NomePessoa, SobrenomePessoa, DataDeAniversario FROM Pessoa
                             WHERE MONTH(DataDeAniversario) = @P1 AND DAY(DataDeAniversario) = @P2
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", date.Month);
                sqlCommand.Parameters.AddWithValue("P2", date.Day);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Pessoa pessoa = new Pessoa()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["NomePessoa"].ToString(),
                        Sobrenome = reader["SobrenomePessoa"].ToString(),
                        DataDeAniversario = Convert.ToDateTime(reader["DataDeAniversario"])
                    };

                    result.Add(pessoa);
                }

                connection.Close();
            }

            return result;
        }

        public List<Pessoa> GetNextBirthday()
        {
            List<Pessoa> result = new List<Pessoa>();
            DateTime date = DateTime.Today;

            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var sql = @"SELECT Id, NomePessoa, SobrenomePessoa, DataDeAniversario 
                            FROM Pessoa
                            WHERE (MONTH(DataDeAniversario) <> @P1 OR MONTH(DataDeAniversario) = @P1) 
                            AND (DAY(DataDeAniversario) <> @P2 OR DAY(DataDeAniversario) = @P2)
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", date.Month);
                sqlCommand.Parameters.AddWithValue("P2", date.Day);
                sqlCommand.Parameters.AddWithValue("P3", date.Date);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Pessoa pessoa = new Pessoa()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["NomePessoa"].ToString(),
                        Sobrenome = reader["SobrenomePessoa"].ToString(),
                        DataDeAniversario = Convert.ToDateTime(reader["DataDeAniversario"])
                    };

                    result.Add(pessoa);
                }

                connection.Close();
            }

            return result.OrderBy(pessoa => pessoa.DiferencaAniversario()).ToList();
        }
    }
}