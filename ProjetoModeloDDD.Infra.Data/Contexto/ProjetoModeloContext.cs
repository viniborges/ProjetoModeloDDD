
using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity;

namespace ProjetoModeloDDD.Infra.Data.Contexto
{
    //Contexto do banco de dados do Entity
    public class ProjetoModeloContext : DbContext
    {
        public ProjetoModeloContext() : base("ProjetoModeloDDD") { }

        public DbSet<Cliente> Clientes { get; set; }

    }
}
