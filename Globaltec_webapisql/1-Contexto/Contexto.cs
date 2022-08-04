using Globaltec_webapisql._0_Models;
using Microsoft.EntityFrameworkCore;


namespace Globaltec_webapisql._1_Contexto
{
    public class Contexto : DbContext

    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet <Produto> Produto { get; set; }

        public DbSet<Fornecedor> Fornecedor { get; set; }




    }
}
