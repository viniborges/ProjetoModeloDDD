
using System;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        public  string Nome { get; set; }

        public string Sobrenome { get; set; }

        public  string Email { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }


        public bool ClienteEspecial(Cliente cliente)
        {
            //Retorna true quando o cliente for especial
            return cliente.Ativo && DateTime.Now.Year - cliente.DataCadastro.Year >= 5;
        }
    }
}
