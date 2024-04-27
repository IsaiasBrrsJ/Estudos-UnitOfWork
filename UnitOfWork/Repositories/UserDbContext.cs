using Microsoft.EntityFrameworkCore;
using RabbitPublish.Model;

namespace RabbitPublish.Repositories
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(x => x.Id);


            modelBuilder.Entity<Endereco>()
                 .HasNoKey();
                 
        }
       
       
    }
}
