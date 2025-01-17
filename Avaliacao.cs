using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellisSound
{
    internal class Avaliacao
    {
        public int Nota;

        public Avaliacao(int Nota) 
        { 
            this.Nota = Nota;
        }

        // Metodos static nao precisa instanciar um objeto da classe para serem acessados
        public static Avaliacao Parse(string txt)
        {
            int av = int.Parse(txt);
            return new Avaliacao(av);
        }
    }
}
