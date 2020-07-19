using System.Collections.Generic;
using System.Linq;
using Desafio.Bexs.Domain.Models;
using Desafio.Bexs.Domain.Dtos;
using System.Reflection;

namespace Desafio.Bexs.Domain.Helpers
{
    public class ControleDijkstraHelper
    {
        private readonly List<Aeroporto> _aeroportosVerificados = new List<Aeroporto>();
        private readonly Dictionary<Aeroporto, MelhorPrecoDto> _precosPorRota =new Dictionary<Aeroporto, MelhorPrecoDto>();
        private readonly List<Aeroporto> _aeroportosAguardandoVerificacao = new List<Aeroporto>();

        public void Verificar(Aeroporto aeroporto)
        {
            if (!_aeroportosVerificados.Contains(aeroporto))
            {
                _aeroportosVerificados.Add((aeroporto));
            }
        }

        public bool FoiVerificado(Aeroporto aeroporto)
        {
            return _aeroportosVerificados.Contains(aeroporto);
        }

        public void AtualizarPreco(Aeroporto aeroporto, MelhorPrecoDto novoPreco)
        {
            if (!_precosPorRota.ContainsKey(aeroporto))
            {
                _precosPorRota.Add(aeroporto, novoPreco);
            }
            else
            {
                _precosPorRota[aeroporto] = novoPreco;
            }
        }

        public MelhorPrecoDto BuscarMelhorPreco(Aeroporto aeroporto)
        {
            MelhorPrecoDto result;
            if (!_precosPorRota.ContainsKey(aeroporto))
            {
                result = new MelhorPrecoDto(null, int.MaxValue);
                _precosPorRota.Add(aeroporto, result);
            }
            else
            {
                result = _precosPorRota[aeroporto];
            }
            return result;
        }

        public void AgendarVerificacao(Aeroporto aeroporto)
        {
            _aeroportosAguardandoVerificacao.Add(aeroporto);
        }

        public bool AguardaVerificacao => _aeroportosAguardandoVerificacao.Any();

        public Aeroporto ProximoAeroportoParaVerificar()
        {
            var ordered = from n in _aeroportosAguardandoVerificacao
                          orderby BuscarMelhorPreco(n).Valor
                          select n;

            var result = ordered.First();
            _aeroportosAguardandoVerificacao.Remove(result);
            return result;
        }

        public bool PossuiRotaValida(Aeroporto aeroporto)
        {
            return BuscarMelhorPreco(aeroporto).AeroportoOrigem != null;
        }

        public MelhorRotaDto ComputarMelhorRotaParaOrigem(Aeroporto aeroporto)
        {
            var n = aeroporto;
            var melhorRota = new MelhorRotaDto();
            while (n != null)
            {
                var preco = BuscarMelhorPreco(n);

                if(melhorRota.PrecoTotalRota == 0)
                {
                    melhorRota.PrecoTotalRota = preco.Valor;
                }
                melhorRota.Aeroportos.Add(n.Id);
                n = preco.AeroportoOrigem;
            }
            melhorRota.Aeroportos.Reverse();
            return melhorRota;
        }
    }
}
