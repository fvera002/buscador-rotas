using Desafio.Bexs.Domain.Dtos;
using Desafio.Bexs.Domain.Interfaces;
using Desafio.Bexs.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Desafio.Bexs.Data.Repositories
{
    public class RotaRepository : IRotaRepository
    {
        private readonly string _arquivoRotas;

        public RotaRepository(string arquivoRotas)
        {
            _arquivoRotas = arquivoRotas;
        }

        public IEnumerable<RotaDto> BuscarTodas()
        {
            var linhas = File.ReadAllLines(_arquivoRotas);

            foreach (var item in linhas)
            {
                var campos = item.Split(',');
                yield return new RotaDto 
                { 
                    AeroportoOrigemId = campos[0], 
                    AeroportoDestinoId = campos[1], 
                    Preco = Int32.Parse(campos[2]) 
                };
            }
        }

        public void Inserir(RotaDto rota)
        {
            File.AppendAllText(_arquivoRotas, $"{rota.AeroportoOrigemId},{rota.AeroportoDestinoId},{rota.Preco}");
        }
    }
}
