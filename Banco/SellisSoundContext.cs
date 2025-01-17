using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellisSound.Banco;
    internal class SellisSoundContext : DbContext
    {
        public DbSet<Artista> artistas {  get; set; }

        public string conectionString = "Data Source=(localdb)\\" +
            "MSSQLLocalDB;Initial Catalog=SellisSound;" +
            "Integrated Security=True;Encrypt=False;" +
            "Trust Server Certificate=False;" +
            "Application Intent=ReadWrite;Multi Subnet Failover=False";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conectionString);
        }

        public SqlConnection ObterConexao()
        {
            return new SqlConnection(conectionString);
        }

    

}
