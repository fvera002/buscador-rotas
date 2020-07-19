using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Bexs.Domain.Dtos;
using Microsoft.AspNetCore.Http.Extensions;

namespace Desafio.Bex.Api.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, $"Erro ao executar: {context.HttpContext.Request.GetEncodedUrl()}");

            if (context.Exception is ArgumentException)
            {
                var errorResponse = new ErroDto { Erro = 400, Messagem = context.Exception.Message };
                context.Result = new ObjectResult(errorResponse) { StatusCode = StatusCodes.Status400BadRequest };
            }
            else
            {
                var errorResponse = new ErroDto { Erro = 500, Messagem = context.Exception.Message };
                context.Result = new ObjectResult(errorResponse) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
