using Desafio.Bexs.Domain.Models;
using Desafio.Bexs.Domain.Services;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Bexs.Tests.Services
{
    public class BuscadorMelhorRotaDijkstraTests
    {
        private BuscadorMelhorRotaDijkstra buscadorMelhorRotaDijkstra;
        private readonly Aeroporto _GRU = new Aeroporto("GRU");
        private readonly Aeroporto _BRC = new Aeroporto("BRC");
        private readonly Aeroporto _SCL = new Aeroporto("SCL");
        private readonly Aeroporto _CDG = new Aeroporto("CDG");
        private readonly Aeroporto _ORL = new Aeroporto("ORL");

        [SetUp]
        public void SetUp()
        {
            _GRU.AdicionarConexao(_BRC, 10);
            _BRC.AdicionarConexao(_SCL, 5);
            _GRU.AdicionarConexao(_CDG, 75);
            _GRU.AdicionarConexao(_SCL, 20);
            _GRU.AdicionarConexao(_ORL, 56);
            _ORL.AdicionarConexao(_CDG, 5);
            _SCL.AdicionarConexao(_ORL, 20);

            buscadorMelhorRotaDijkstra = new BuscadorMelhorRotaDijkstra();
        }


        [Test]
        public void DeveBuscarRotaDeGRUParaCDGCorretamente()
        {
            var melhorRota = buscadorMelhorRotaDijkstra.BuscarMelhorRota(_GRU, _CDG);

            melhorRota.Should().NotBeNull();
            melhorRota.Aeroportos.Should().BeEquivalentTo(new string[] { _GRU.Id, _BRC.Id, _SCL.Id, _ORL.Id, _CDG.Id });
            melhorRota.PrecoTotalRota.Should().Be(40);
        }

        [Test]
        public void DeveBuscarRotaDeBRCParaCDGCorretamente()
        {
            var melhorRota = buscadorMelhorRotaDijkstra.BuscarMelhorRota(_BRC, _CDG);

            melhorRota.Should().NotBeNull();
            melhorRota.Aeroportos.Should().BeEquivalentTo(new string[] { _BRC.Id, _SCL.Id, _ORL.Id, _CDG.Id });
            melhorRota.PrecoTotalRota.Should().Be(30);
        }

        [Test]
        public void DeveRetornarNuloAoBuscarRotaInexistente()
        {
            var LAX = new Aeroporto("LAX");
            var melhorRota = buscadorMelhorRotaDijkstra.BuscarMelhorRota(_BRC, LAX);

            melhorRota.Should().BeNull();
        }

        [Test]
        public void DeveBuscarRotaDeBRCParaLAXCorretamente()
        {
            var LAX = new Aeroporto("LAX");
            _SCL.AdicionarConexao(LAX, 150);
            var melhorRota = buscadorMelhorRotaDijkstra.BuscarMelhorRota(_BRC, LAX);

            melhorRota.Should().NotBeNull();
            melhorRota.Aeroportos.Should().BeEquivalentTo(new string[] { _BRC.Id, _SCL.Id, LAX.Id });
            melhorRota.PrecoTotalRota.Should().Be(155);
        }
    }
}
