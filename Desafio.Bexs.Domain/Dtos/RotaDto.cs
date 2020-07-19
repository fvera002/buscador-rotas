using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Desafio.Bexs.Domain.Dtos
{
    public class RotaDto
    {
        public string Id => AeroportoOrigemId + "-" + AeroportoDestinoId;
        public int Preco { get; set; }

        [StringLength(3)]
        public string AeroportoOrigemId { get; set; }

        [StringLength(3)]
        public string AeroportoDestinoId { get; set; }
    }
}
