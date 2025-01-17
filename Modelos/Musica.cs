using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Musica
{
    public string Nome { get; }
    public Artista Artista { get; }
    public Album Album { get; }
    public int duracao;
    public int Duracao
    {
        get => duracao;
        set
        {
            if (duracao > 700)
            {
                Console.WriteLine("A duração da musica não pode ser maior do que 12 minutos");
            }
            else
            {
                duracao = value;
            }
        }
    }

    public Musica(string nome, Artista artista, Album album)
    {
        Nome = nome;
        Artista = artista;
        Album = album;
    }

    public void ExibirFichaTecnica()
        {
            Console.WriteLine("\nMusica/Artista/Album: {0} ({1}) - {2} ({3})\n",Nome, Duracao, Artista, Album);
            
        }

    public void TocarMusica()
    {
        Console.WriteLine("Tocando musica ...");
    }

    public void PausarMusica()
    {
        Console.WriteLine("Pause");
    }

    }

