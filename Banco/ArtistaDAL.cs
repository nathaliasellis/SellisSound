using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellisSound.Banco
{
    internal class ArtistaDAL
    {
       
        public IEnumerable<Artista> listarArtistas()
        {
            var listaArtistas = new List<Artista>();
            //using var connection = new SellisSoundContext().ObterConexao();
            using var context = new SellisSoundContext();
            return context.artistas.ToList();

            /*connection.Open();

            string sql = "SELECT * FROM Artistas";
            SqlCommand command = new SqlCommand(sql, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string nome = Convert.ToString(reader["Nome"]);
                int id = Convert.ToInt32(reader["ID"]);
                Artista artista = new Artista(nome) { Id = id };
                listaArtistas.Add(artista);
            }
            return listaArtistas;*/
        }

        public void AdicionarArtista(Artista artista)
        {
            using var context = new SellisSoundContext();
            context.artistas.Add(artista);
            context.SaveChanges();
            /*using var connection = new SellisSoundContext().ObterConexao();
            connection.Open();

            string sql = "INSERT INTO Artistas (Nome, Bio) VALUES (@nome, @bio)";
            SqlCommand command = new SqlCommand(sql, connection);

            // Referenciar os parametros
            command.Parameters.AddWithValue("Nome", artista.Nome);
            command.Parameters.AddWithValue("Bio", artista.Bio);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine("Linhas executadas: {0}",retorno);*/
        }

        public void AtualizarArtista(Artista artista, int id, string nomeAlterado)
        {
            using var connection = new SellisSoundContext().ObterConexao();
            connection.Open();

            string SQL = "UPDATE Artistas SET Nome = @Nome where ID = @id";
            SqlCommand command = new SqlCommand(@SQL, connection);

            //Referenciar ParÂmetros
            command.Parameters.AddWithValue("Nome", nomeAlterado);
            command.Parameters.AddWithValue("ID", artista.Id);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine("Linhas alteradas: {0}", retorno);
        }

        public void DeletarArtista(Artista artista, int ID)
        {
            using var connection = new SellisSoundContext().ObterConexao();
            connection.Open();

            string sql = "DELETE FROM Artistas where ID = @ID";
            SqlCommand command = new SqlCommand(sql,connection);

            command.Parameters.AddWithValue("ID", artista.Id);
            
        }
    }
}
