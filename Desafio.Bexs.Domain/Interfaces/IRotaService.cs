using Desafio.Bexs.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Bexs.Domain.Interfaces
{
    public interface IRotaService
    {
        void CarregarTodasRotas();
        void CadastrarNovaRota(RotaDto rotaDto);

        MelhorRota BuscarMelhorNota(string aeroportoOrigemId, string aeroportoDestinoId);
    }
}
