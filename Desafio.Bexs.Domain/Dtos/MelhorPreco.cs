using Desafio.Bexs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Bexs.Domain.Dtos
{
    public class MelhorPreco
    {
        public Aeroporto AeroportoOrigem { get; }
        public int Valor { get; }

        public MelhorPreco(Aeroporto aeroportoOrigem, int preco)
        {
            AeroportoOrigem = aeroportoOrigem;
            Valor = preco;
        }
    }
}
