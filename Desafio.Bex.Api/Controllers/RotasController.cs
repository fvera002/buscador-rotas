using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Bex.Api.Filters;
using Desafio.Bexs.Domain.Dtos;
using Desafio.Bexs.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Bex.Api.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class RotasController : ControllerBase
    {
        private readonly IRotaService _rotaService;

        public RotasController(IRotaService rotaService)
        {
            _rotaService = rotaService;
        }

        [HttpPost]
        public RotaDto CadastrarRota([FromBody] RotaDto rotaDto)
        {
            _rotaService.CadastrarNovaRota(rotaDto);
            return rotaDto;
        }

        [HttpGet]
        [Route("melhor-rota")]
        public MelhorRotaDto BuscarMelhorRota([FromQuery, Required] string aeroportoOrigemId, [FromQuery, Required] string aeroportoDestinoId)
        {
            return _rotaService.BuscarMelhorNota(aeroportoOrigemId, aeroportoDestinoId);
        }
    }
}