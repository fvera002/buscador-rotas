﻿using Desafio.Bexs.Domain.Dtos;
using Desafio.Bexs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Bexs.Domain.Interfaces
{
    public interface IRotaRepository
    {
        IEnumerable<RotaDto> BuscarTodas();
        void Inserir(RotaDto rota);
    }
}