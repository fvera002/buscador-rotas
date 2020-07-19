using Desafio.Bexs.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desafio.Bexs.Domain.Models
{
    public class Aeroporto
    {
        public string Id { get; }

        private readonly List<Rota> _rotas = new List<Rota>();

        public Aeroporto(string id)
        {
            Id = id;
        }
        public IEnumerable<Rota> Rotas => _rotas;

        public IEnumerable<ConexaoInfo> Conexoes =>
            from rota in Rotas
            select new ConexaoInfo(
                rota.AeroportoOrigem == this ? rota.AeroportoDestino : rota.AeroportoOrigem,
                rota.Preco
                );

        public void AdicionarRota(Rota rota)
        {
            _rotas.Add(rota);
        }

        public void AdicionarConexao(Aeroporto destino, int valorRota)
        {
            Rota.Criar(valorRota, this, destino);
        }
    }
}
