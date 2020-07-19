using Desafio.Bexs.Domain.Dtos;
using Desafio.Bexs.Domain.Interfaces;
using Desafio.Bexs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desafio.Bexs.Domain.Services
{
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;
        private readonly IAeroportoRepository _aeroportoRepository;
        private readonly IBuscadorMelhorRota _buscadorMelhorRota;

        public RotaService(IRotaRepository rotaRepository, IAeroportoRepository aeroportoRepository, IBuscadorMelhorRota buscadorMelhorRota)
        {
            _rotaRepository = rotaRepository;
            _aeroportoRepository = aeroportoRepository;
            _buscadorMelhorRota = buscadorMelhorRota;
        }

        public void CarregarTodasRotas()
        {
            var rotas = _rotaRepository.BuscarTodas();

            foreach (var item in rotas)
            {
                AdicionaAeroportosEConexao(item);
            }
        }

        private void AdicionaAeroportosEConexao(RotaDto item)
        {
            _aeroportoRepository.Inserir(item.AeroportoOrigemId);
            _aeroportoRepository.Inserir(item.AeroportoDestinoId);

            var aeroportoOrigem = _aeroportoRepository.Buscar(item.AeroportoOrigemId);
            var aeroportoDestino = _aeroportoRepository.Buscar(item.AeroportoDestinoId);

            aeroportoOrigem.AdicionarConexao(aeroportoDestino, item.Preco);
        }

        public void CadastrarNovaRota(RotaDto item)
        {
            AdicionaAeroportosEConexao(item);
            _rotaRepository.Inserir(item);
        }

        public MelhorRotaDto BuscarMelhorNota(string aeroportoOrigemId, string aeroportoDestinoId)
        {
            var aeroportoOrigem = _aeroportoRepository.Buscar(aeroportoOrigemId);
            var aeroportoDestino = _aeroportoRepository.Buscar(aeroportoDestinoId);
            const string mensagemRotaNaoEncontrada = "route not found";

            if (aeroportoOrigem == null || aeroportoDestino == null)
            {
                throw new ArgumentException(mensagemRotaNaoEncontrada);
            }

            var melhorRota = _buscadorMelhorRota.BuscarMelhorRota(aeroportoOrigem, aeroportoDestino);

            if (melhorRota.Aeroportos.Any())
            {
                melhorRota.Descricao = $"best route: {string.Join('-', melhorRota.Aeroportos)} > ${melhorRota.PrecoTotalRota}";
            }
            else
            {
                throw new ArgumentException(mensagemRotaNaoEncontrada);
            }

            return melhorRota;
        }
    }
}
