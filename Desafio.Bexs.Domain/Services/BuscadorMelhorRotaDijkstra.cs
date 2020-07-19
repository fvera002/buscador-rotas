using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Desafio.Bexs.Domain.Interfaces;
using Desafio.Bexs.Domain.Models;
using Desafio.Bexs.Domain.Dtos;
using Desafio.Bexs.Domain.Helpers;

namespace Desafio.Bexs.Domain.Services
{
    public class BuscadorMelhorRotaDijkstra : IBuscadorMelhorRota
    {
        public MelhorRota BuscarMelhorRota(Aeroporto origem, Aeroporto destino)
        {
            var controle = new ControleDijkstraHelper();

            controle.AtualizarPreco(origem, new MelhorPreco(null, 0));
            controle.AgendarVerificacao(origem);

            while (controle.AguardaVerificacao)
            {
                var aeroportoAtual = controle.ProximoAeroportoParaVerificar();
                var precoAtual = controle.BuscarMelhorPreco(aeroportoAtual);
                controle.Verificar(aeroportoAtual);

                foreach (var conexaoInfo in aeroportoAtual.Conexoes)
                {
                    if (!controle.FoiVerificado(conexaoInfo.Aeroporto))
                    {
                        controle.AgendarVerificacao(conexaoInfo.Aeroporto);
                    }

                    var precoConexao = controle.BuscarMelhorPreco(conexaoInfo.Aeroporto);
                    var precoProvavelAtual = precoAtual.Valor + conexaoInfo.Preco;
                    
                    if (precoConexao.Valor > precoProvavelAtual)
                    {
                        controle.AtualizarPreco(conexaoInfo.Aeroporto, new MelhorPreco(aeroportoAtual, precoProvavelAtual));
                    }
                }
            }

            return controle.PossuiRotaValida(destino)
                ? controle.ComputarMelhorRotaParaOrigem(destino)
                : null;
        }
    }
}
