using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Album
{
    public string Nome { get; }
	public List<Musica> musicas = new List<Musica>();
    public int DuracaoTotal => musicas.Sum(m => m.Duracao);
    public string Descricao => "${Nome} ({DuracaoTotal}";
	public Artista artista {  get; }
    public Album(string nome, Artista artista)
	{
		Nome = nome;
	}

	public void AddMusicaAoAlbum(Musica musica)
	{
		musicas.Add(musica);
	}

	public void ExibirMusicasDoAlbum()
	{
		foreach(var musica in musicas)
		{
            Console.WriteLine($"{musica.Nome} ({musica.duracao}");
		}
	}



}

