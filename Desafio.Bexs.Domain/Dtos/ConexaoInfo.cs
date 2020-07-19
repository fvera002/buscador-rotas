using Desafio.Bexs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Bexs.Domain.Dtos
{
    public struct ConexaoInfo
    {
        public Aeroporto Aeroporto { get; }
        public int Preco { get; }

        public ConexaoInfo(Aeroporto aeroporto, int preco)
        {
            Aeroporto = aeroporto;
            Preco = preco;
        }
    }
}
