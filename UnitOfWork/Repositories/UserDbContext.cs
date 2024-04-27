using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitPublish.Model;

namespace RabbitPublish.Repositories
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> @base) : base(@base)
        {
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(x => x.Id);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                string connString = "Server=172.17.36.84;User Id=sa;Password=MinhaSenhaFacil@123;TrustServerCertificate=True";
               optionsBuilder.UseSqlServer(connString); 
            }
        }

    }
}
