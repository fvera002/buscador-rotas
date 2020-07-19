using Desafio.Bexs.Domain.Interfaces;
using Desafio.Bexs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Bexs.Data.Repositories
{
    public class AeroportoRepository : IAeroportoRepository
    {
        private static readonly Lazy<Dictionary<string, Aeroporto>> _aeroportos = new Lazy<Dictionary<string, Aeroporto>>(() => new Dictionary<string, Aeroporto>());

        public void Inserir(string id)
        {            
            if (!_aeroportos.Value.ContainsKey(id))
            {
                _aeroportos.Value.Add(id, new Aeroporto(id));
            }
        }

        public Aeroporto Buscar(string id)
        {
            if (_aeroportos.Value.ContainsKey(id))
            {
                return _aeroportos.Value[id];
            }
            return null;
        }
    }
}
