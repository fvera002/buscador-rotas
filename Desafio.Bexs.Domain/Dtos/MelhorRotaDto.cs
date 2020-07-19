using Desafio.Bexs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desafio.Bexs.Domain.Dtos
{
    public class MelhorRotaDto
    {
        public List<string> Aeroportos { get; set; } = new List<string>();

        public int PrecoTotalRota { get; set; }

        public string Descricao { get; set; }
    }
}
