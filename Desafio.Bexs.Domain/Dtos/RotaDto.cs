using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Bexs.Domain.Dtos
{
    public class RotaDto
    {
        public string Id { get; set; }
        public int Preco { get; set; }
        public string AeroportoOrigemId { get; set; }
        public string AeroportoDestinoId { get; set; }
    }
}
