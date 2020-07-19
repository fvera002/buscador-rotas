using Desafio.Bexs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Bexs.Domain.Dtos
{
    public class MelhorPrecoDto
    {
        public Aeroporto AeroportoOrigem { get; }
        public int Valor { get; }

        public MelhorPrecoDto(Aeroporto aeroportoOrigem, int preco)
        {
            AeroportoOrigem = aeroportoOrigem;
            Valor = preco;
        }
    }
}
