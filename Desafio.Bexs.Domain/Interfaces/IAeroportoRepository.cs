using Desafio.Bexs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Bexs.Domain.Interfaces
{
    public interface IAeroportoRepository
    {
        void Inserir(string id);
        Aeroporto Buscar(string id);
    }
}
