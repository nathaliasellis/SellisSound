using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellisSound
{
    internal interface IAvaliavel
    {
        double Media {  get; }
        public void Avaliar(Avaliacao a);
    }
}
