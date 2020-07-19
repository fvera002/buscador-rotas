using System;

namespace Desafio.Bexs.Domain.Models
{
    public class Rota
    {
        public string Id { get; }
        public int Preco { get; }
        public Aeroporto AeroportoOrigem { get; }
        public Aeroporto AeroportoDestino { get; }

        private Rota(int preco, Aeroporto aeroportoOrigem, Aeroporto aeroportoDestino)
        {
            if (preco <= 0)
            {
                throw new ArgumentException("Preço da rota deve ser positivo");
            }

            Id = aeroportoOrigem.Id + aeroportoDestino.Id;
            Preco = preco;
            AeroportoOrigem = aeroportoOrigem;
            aeroportoOrigem.AdicionarRota(this);
            AeroportoDestino = aeroportoDestino;
            aeroportoDestino.AdicionarRota(this);
        }

        public static Rota Criar(int preco, Aeroporto aeroportoOrigem, Aeroporto aeroportoDestino)
        {
            return new Rota(preco, aeroportoOrigem, aeroportoDestino);
        }
    }
}
