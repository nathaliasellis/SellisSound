using Microsoft.Data.SqlClient;
using SellisSound.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Artista
{
    public int Id { get; set; }
    public string Nome { get; }
    public string Bio;
    public List<Album> albuns = new List<Album>();
    public List<int> notas = new();
    public double Media
    {
        get
        {
            if (notas.Count > 0) return notas.Average();
            else return 0;

        }
    }
    public Artista(string nome)
    {
        Nome = nome;
    }

   


    public void ExibirAlbunsDoArtista()
    {
        foreach (var a in albuns)
        {
            Console.WriteLine($"{a.Nome} ({a.DuracaoTotal} seg.)");
        }
    }

    public void ExibirAvaliacoesDoArtista()
    {
        foreach (var a in notas)
        {
            Console.WriteLine($"{a}");
        }

        Console.WriteLine("\n A média de {0} é de: {1}", this.Nome, this.Media);
    }
    public void ExibirDetalhesDoArtista()
    {
        Console.WriteLine(this.Nome);
        Console.WriteLine("\n Álbuns: ");
        ExibirAlbunsDoArtista();
        Console.WriteLine("\n");
        ExibirAvaliacoesDoArtista();
    }

    public void AdicionarAlbum(Album album)
    {
        albuns.Add(album);
    }

    public void AvaliarArtista(int nota)
    {
        notas.Add(nota);
    }

    public static Artista BuscarArtista(String nome)
    {
        using var connection = new SellisSoundContext().ObterConexao();
        connection.Open();

        string sql = "SELECT * FROM Artistas where Nome = @Nome";

        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("Nome", nome);


        var commandReader = command.ExecuteReader();
        commandReader.Read();

        string nomeArtista = Convert.ToString(commandReader["Nome"]);
        int IDArtista = Convert.ToInt32(commandReader["Id"]);

        Artista artista = new(nomeArtista) { Id = IDArtista };

        return artista;


    }

    ~Artista()
    {
        Console.WriteLine("Artista foi destruído");
    }
}

