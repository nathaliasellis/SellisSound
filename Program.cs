// See https://aka.ms/new-console-template for more information


using SellisSound;
using System.ComponentModel.Design;
using OpenAI;
using System.Data;
using OpenAI.Chat;
using OpenAI.RealtimeConversation;
using SellisSound.Banco;

public class Program
{
    public static List<Artista> artistas= new List<Artista>();
    public static Menu menu = new();

    public static void Main()
    {
        try
        {
            var artistaDAL = new ArtistaDAL();
            //  artistaDAL.AdicionarArtista(new Artista("Foo Fighters"));
             

           // artistaDAL.AtualizarArtista(artistaAlterar, artistaAlterar.Id, nomeAlterado);

            var ListaArtistas = artistaDAL.listarArtistas();
            foreach (var artista in ListaArtistas)
            {
                Console.WriteLine("Nome: {0}, ID: {1}",artista.Nome, artista.Id);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        //var client = new OpenAI.OpenAIClient("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");
       /* ChatClient client = new("gpt-3.5-turbo", "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");


        ChatCompletion msg = client.CompleteChat("Qual a capital do brasil? ");

        Console.WriteLine(msg.Content[0].Text);
       */

        VoltarAoMenu(0);
    }

    public static void RedirecionarOpcaoEscolhida(int op)
    {
        switch (op)
        {
            case 1:
                Console.Clear();
                CriarArtista();
                MontarTitulo("Deseja adicionar mais algum artista? Digite 1 para sim, ou digite 0 para voltar ao menu inicial");
                int opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    RedirecionarOpcaoEscolhida(1);
                }
                else if (opc == 0)
                {
                    Console.Clear();
                    VoltarAoMenu(0);
                }
                break;
            case 2:
                Console.Clear();
                MontarTitulo("Para qual dos artistas abaixo você deseja adicionar um álbum:");
                ExibirArtistas();

                string nomeArtista = Console.ReadLine();
                Artista artista = artistas.FirstOrDefault(a => a.Nome.Equals(nomeArtista, StringComparison.OrdinalIgnoreCase));
                if (artista != null)
                {
                    Console.WriteLine("\n");
                    MontarTitulo("Informe o nome do album.");
                    string nomeAlbum = Console.ReadLine();
                    Album album = new Album(nomeAlbum, artista);
                    artista.AdicionarAlbum(album);

                    Console.WriteLine("\nO álbum {0}, de {1}, foi criado com sucesso!\n", nomeAlbum, nomeArtista);
                }
                else
                {
                    Console.WriteLine("\nArtista não encontrado\n");
                }

                VoltarAoMenu(1);

                break;
            case 3://Adicionar musica
                Console.Clear();
                MontarTitulo("Para qual dos artistas abaixo você deseja adicionar uma música?");
                ExibirArtistas();
                string nomeArtistaMusica = Console.ReadLine();
                Artista artistaMusica = artistas.FirstOrDefault(a => a.Nome.Equals(nomeArtistaMusica, StringComparison.OrdinalIgnoreCase));
                if(artistaMusica != null)
                {
                    MontarTitulo("Para qual album do artista você deseja adicionar uma musica?");
                    artistaMusica.ExibirAlbunsDoArtista();
                    string albumEscolhido = Console.ReadLine();
                    //Album album = artistaMusica.albuns.FirstOrDefault(b => b.Nome.Equals(albumEscolhido, StringComparison.OrdinalIgnoreCase));
                    if(artistaMusica.albuns.Any(a => a.Nome.Equals(albumEscolhido))) //// (album ! = null)
                    {
                        Album album = artistaMusica.albuns.Find(b => b.Equals(albumEscolhido));
                        Console.WriteLine("\n");
                        MontarTitulo("Informe o nome da música: ");
                        string Nomemusica = Console.ReadLine();
                        Musica musica = new(Nomemusica, artistaMusica, album);
                        Console.WriteLine("\n");
                        MontarTitulo("Informe a duração da música: ");
                        musica.Duracao = int.Parse(Console.ReadLine());
                        album.AddMusicaAoAlbum(musica);
                        Console.WriteLine("\n");
                    }
                    else 
                    {
                        Console.WriteLine("\nÁlbum não encontrado!\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nArtista não encontrado!\n");
                }
                VoltarAoMenu(1);
                break;
            case 4: //Exibir lista de artistas
                ExibirArtistas();
                VoltarAoMenu(1);
                break;
            case 5:
                Console.Clear();
                MontarTitulo("De qual dos artistas você deseja ver os detalhes?");
                ExibirArtistas();
                string nomeArtistaDetalhes = Console.ReadLine();
                Artista artistaDetalhes = artistas.FirstOrDefault(a => a.Nome.Equals(nomeArtistaDetalhes, StringComparison.OrdinalIgnoreCase));
                if(artistaDetalhes != null)
                {
                    artistaDetalhes.ExibirDetalhesDoArtista();
                }
                else
                {
                    Console.WriteLine("\nArtista não encontrado!\n");
                }
                break;

            case 6:
                Console.Clear();
                MontarTitulo("De qual dos artistas você deseja ver os albuns?");
                ExibirArtistas();
                string nome = Console.ReadLine();
                Console.Clear();
                Artista artistaAlbum = artistas.FirstOrDefault(a => a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
                if(artistaAlbum != null)
                {
                    MontarTitulo("Albuns do artista");
                    artistaAlbum.ExibirAlbunsDoArtista();
                }
                else
                {
                    Console.WriteLine("\nArtista não encontrado\n");
                }

                VoltarAoMenu(1);

                break;
            case 7:
                Console.Clear();
                MontarTitulo("Para qual artista você deseja adicionar uma nota? ");
                ExibirArtistas();
                string nomeArtistaNota = Console.ReadLine();
                Artista artistaNota = artistas.FirstOrDefault(a => a.Nome.Equals(nomeArtistaNota, StringComparison.OrdinalIgnoreCase));
                if(artistaNota != null)
                {
                    Console.WriteLine("\n{0}", nomeArtistaNota);
                    MontarTitulo("Qual nota você deseja dar ao artista {0}?");
                    int nota = int.Parse(Console.ReadLine());
                    artistaNota.AvaliarArtista(nota);
                }
                else
                {
                    Console.WriteLine("\nArtista inexistente.\n");
                }

                VoltarAoMenu(1); 

                break;
            case 8:
                MontarTitulo("Digite o artista que deseja alterar");
                string nomeArtistaAlterar = Console.ReadLine();
                var artistaRetornado = Artista.BuscarArtista(nomeArtistaAlterar);

                if (artistaRetornado != null)
                {
                    ArtistaDAL artistaDAL = new ArtistaDAL();
                    Console.Write("Informe o nome alterado: ");
                    string nomeAlterado = Console.ReadLine();
                    artistaDAL.AtualizarArtista(artistaRetornado, artistaRetornado.Id, nomeAlterado);
                }
                else
                {
                    MontarTitulo("Artista não encontrado");
                }
                break;
            case 9:
                MontarTitulo("Informe qual artista você deseja excluir: ");
                string nomeArtistaExcluir = Console.ReadLine();
                var artistaExcluir = Artista.BuscarArtista(nomeArtistaExcluir);

                if (artistaExcluir != null)
                {
                    ArtistaDAL artistaDal =  new ArtistaDAL();
                    artistaDal.DeletarArtista(artistaExcluir, artistaExcluir.Id);
                    MontarTitulo("Artista excluido com sucesso");
                }
                else
                {
                    MontarTitulo("Artista não encontrado");
                }
                break;
            default:
                Console.WriteLine("\nOpção Inválida\n");
                VoltarAoMenu(1);
                break;
        }
    }
    
    public static void ExibirArtistas()
    {
        Console.WriteLine("********************");
        foreach (Artista artista in artistas)
        {
            Console.WriteLine($"      {artista.Nome}     ");
        }
        try
        {
            var artistaDAL = new ArtistaDAL();
            var ListaArtistas = artistaDAL.listarArtistas();
            foreach (var artista in ListaArtistas)
            {
                Console.WriteLine(artista.Nome);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        Console.WriteLine("********************");
    }

    public static Artista CriarArtista()
    {   
        MontarTitulo("Digite o nome do artista a ser adicionado:");
        string nomeArtista = Console.ReadLine()!;
       /* var artistaDAL = new ArtistaDAL();
        artistaDAL.AdicionarArtista(new Artista(nomeArtista));
       */

        Artista artista = new Artista(nomeArtista);
        using var context = new SellisSoundContext();
        context.artistas.Add(artista);
        context.SaveChanges();
        artistas.Add(artista);
        Console.WriteLine($"\nArtista {artista.Nome} adicionado com sucesso!");

        return artista;
    }

    public static void VoltarAoMenu(int o)
    {
        if (o == 0)
        {
            menu.Exibir();
            Console.Write("Digite a opção desejada: ");
            int op = int.Parse(Console.ReadLine());
            RedirecionarOpcaoEscolhida(op);
        }
        else
        {
            MontarTitulo("Digite 1 para voltar ao menu inicial ou qualquer tecla para sair");
            int opc = int.Parse(Console.ReadLine());
            if (opc == 1)
            {
                Console.Clear();
                menu.Exibir();
                Console.Write("Digite a opção desejada: ");
                int op = int.Parse(Console.ReadLine());
                RedirecionarOpcaoEscolhida(op);
            }
        }
    }

    public static void MontarTitulo(string titulo)
    {
        int tamanho;
        tamanho = titulo.Length;
        string asterisc = string.Empty.PadLeft(tamanho, '*');
        Console.WriteLine(asterisc);
        Console.WriteLine(titulo);
        Console.WriteLine(asterisc);
    }
}
