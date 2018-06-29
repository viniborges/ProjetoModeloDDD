
using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace ProjetoModeloDDD.Infra.Data.Contexto
{
    //Contexto do banco de dados do Entity
    public class ProjetoModeloContext : DbContext
    {
        public ProjetoModeloContext() : base("ProjetoModeloDDD") { }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//Remove delação em cascata quando tiver uma relação 1->n
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();//Remove delação em cascata quando tiver uma relação n->n

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar")); //Configura propriedade do tipo string como varchar ao invés de nvarchar

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100)); //Caso não informa o tamanho do tipo varchar, por padrão cria com tamanho de 100

        }

        public override int SaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if(entry.State == EntityState.Added)
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("DataCadastro").IsModified = false; //se for update, não muda o valor, uma vez que é data cadastra e o campo nunca vai mudar
            }
            return base.SaveChanges();
        }

    }
}
