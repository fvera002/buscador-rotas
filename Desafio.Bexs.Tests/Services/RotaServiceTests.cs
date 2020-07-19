using AutoFixture;
using Desafio.Bexs.Domain.Dtos;
using Desafio.Bexs.Domain.Interfaces;
using Desafio.Bexs.Domain.Models;
using Desafio.Bexs.Domain.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Bexs.Tests.Services
{
    public class RotaServiceTests
    {
        private Mock<IRotaRepository> rotaRepository;
        private Mock<IAeroportoRepository> aeroportoRepository;
        private Mock<IBuscadorMelhorRota> buscadorMelhorRota;
        private RotaService rotaService;
        private Fixture fixture;
        private List<RotaDto> rotas;

        [SetUp]
        public void Setup()
        {
            rotaRepository = new Mock<IRotaRepository>();
            aeroportoRepository = new Mock<IAeroportoRepository>();
            buscadorMelhorRota = new Mock<IBuscadorMelhorRota>();
            rotas = CriarRotasTeste();
            fixture = new Fixture();
            rotaService = new RotaService(rotaRepository.Object, aeroportoRepository.Object, buscadorMelhorRota.Object);
        }

        private List<RotaDto> CriarRotasTeste()
        {
            return new List<RotaDto>
            {
                new RotaDto{ AeroportoOrigemId = "GRU", AeroportoDestinoId = "BRC", Preco=10 },
                new RotaDto{ AeroportoOrigemId = "BRC", AeroportoDestinoId = "SCL", Preco=5 },
                new RotaDto{ AeroportoOrigemId = "GRU", AeroportoDestinoId = "CDG", Preco=75 },
                new RotaDto{ AeroportoOrigemId = "GRU", AeroportoDestinoId = "SCL", Preco=20 },
                new RotaDto{ AeroportoOrigemId = "GRU", AeroportoDestinoId = "ORL", Preco=56 },
                new RotaDto{ AeroportoOrigemId = "ORL", AeroportoDestinoId = "CDG", Preco=5 },
                new RotaDto{ AeroportoOrigemId = "SCL", AeroportoDestinoId = "ORL", Preco=20 },
            };
        }

        [TearDown]
        public void TearDown()
        {
            rotaRepository.VerifyAll();
            aeroportoRepository.VerifyAll();
            buscadorMelhorRota.VerifyAll();
        }

        [Test]
        public void DeveCarregarTodasRotas()
        {
            rotaRepository.Setup(x => x.BuscarTodas()).Returns(rotas);
            aeroportoRepository.Setup(x => x.Inserir(It.IsAny<string>()));
            aeroportoRepository.Setup(x => x.Buscar(It.IsAny<string>())).Returns(fixture.Create<Aeroporto>());

            rotaService.CarregarTodasRotas();

            aeroportoRepository.Verify(x => x.Inserir(It.IsAny<string>()), Times.Exactly(14));
            aeroportoRepository.Verify(x => x.Buscar(It.IsAny<string>()), Times.Exactly(14));
        }

        [Test]
        public void DeveCadastrarNovaRotaValida()
        {
            var rotaDto = fixture.Create<RotaDto>();

            rotaRepository.Setup(x => x.Inserir(rotaDto));
            aeroportoRepository.Setup(x => x.Inserir(It.IsAny<string>()));
            aeroportoRepository.Setup(x => x.Buscar(It.IsAny<string>())).Returns(fixture.Create<Aeroporto>());

            rotaService.CadastrarNovaRota(rotaDto);

            aeroportoRepository.Verify(x => x.Inserir(It.IsAny<string>()), Times.Exactly(2));
            aeroportoRepository.Verify(x => x.Buscar(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void DeveLancarArgumentExceptionAoBuscarMelhorRotaComAeroportoInvalido()
        {
            aeroportoRepository.Setup(x => x.Buscar(It.IsAny<string>()));

            rotaService.Invoking(x=> x.BuscarMelhorNota("GRU", "LAX")).Should().Throw<ArgumentException>("route not found");

            aeroportoRepository.Verify(x => x.Buscar(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void DeveLancarArgumentExceptionAoBuscarMelhorRotaComAeroportoValidoESemRotaParaoDestino()
        {
            var melhorRota = fixture.Build<MelhorRotaDto>()
                .With(x => x.Aeroportos, new List<string>())
                .Create();
            aeroportoRepository.Setup(x => x.Buscar(It.IsAny<string>())).Returns(fixture.Create<Aeroporto>());
            buscadorMelhorRota.Setup(x => x.BuscarMelhorRota(It.IsAny<Aeroporto>(), It.IsAny<Aeroporto>())).Returns(melhorRota);

            rotaService.Invoking(x => x.BuscarMelhorNota("GRU", "LAX")).Should().Throw<ArgumentException>("route not found");

            aeroportoRepository.Verify(x => x.Buscar(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void DeveBuscarMelhorRotaValida()
        {
            var melhorRota = fixture.Create<MelhorRotaDto>(); 

            aeroportoRepository.Setup(x => x.Buscar(It.IsAny<string>())).Returns(fixture.Create<Aeroporto>());
            buscadorMelhorRota.Setup(x => x.BuscarMelhorRota(It.IsAny<Aeroporto>(), It.IsAny<Aeroporto>())).Returns(melhorRota);

            var resultado = rotaService.BuscarMelhorNota("GRU", "LAX");

            resultado.Should().NotBeNull();
            resultado.Descricao.Should().BeEquivalentTo($"best route: {string.Join('-', melhorRota.Aeroportos)} > ${melhorRota.PrecoTotalRota}");
            aeroportoRepository.Verify(x => x.Buscar(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
