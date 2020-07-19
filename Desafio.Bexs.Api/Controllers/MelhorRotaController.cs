using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Bex.Api.Filters;
using Desafio.Bexs.Domain.Dtos;
using Desafio.Bexs.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Bexs.Api.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class MelhorRotaController : ControllerBase
    {
        private readonly IRotaService _rotaService;

        public MelhorRotaController(IRotaService rotaService)
        {
            _rotaService = rotaService;
        }

        [HttpGet]
        public MelhorRotaDto BuscarMelhorRota([FromQuery, Required] string aeroportoOrigemId, [FromQuery, Required] string aeroportoDestinoId)
        {
            return _rotaService.BuscarMelhorNota(aeroportoOrigemId, aeroportoDestinoId);
        }
    }
}