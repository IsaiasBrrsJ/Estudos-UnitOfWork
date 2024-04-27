using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitPublish.Model;
using RabbitPublish.Unit;
using RabbitPublish.Repositories;
using RabbitPublish.Repositories.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitPublish
{
    public class Program
    {
        const string EXCHANGE = "curso-rabbitmq";
  
        static async Task Main(string[] args)
        {

            var builder = new HostBuilder()
              .ConfigureServices((hostContext, services) =>
              {

                  services.AddScoped(typeof(AbstractUnitOfWork<>), typeof(UnitOfWork<>));
                  services.AddDbContext<UserDbContext>(options =>
                  {
                      var connection = "Server=localhost;User Id=sa;Password=MinhaSenhaFacil@123;TrustServerCertificate=True";

                      options.UseSqlServer(connection);
                  });

                  services.AddScoped<IUsuarioRepositories, UsuarioRepositories>();
                  services.AddScoped<IEnderecoRepositories, EnderecoRepositories>();
              });

            var host = builder.Build();

            using (var scopeUser = host.Services.CreateScope())

            {
                int count = 1;
                var usuarioServ = scopeUser.ServiceProvider.GetRequiredService<AbstractUnitOfWork<IUsuarioRepositories>>();
            

                while (count <= 4)
                {
                    Console.Clear();
                    Console.WriteLine("Quantidade maxima : 4");
                    Console.Write("Digite um nome: ");
                    var nome = Console.ReadLine();

                    var usuario = new Usuario(nome!, count);

                    await usuarioServ.BegginTransaction();
                    await usuarioServ.Repositorie.Add(usuario);
                    var IdUser = await usuarioServ.CompleteTask();

                    var endereco = new Endereco("Tico", "33333", "CTO", usuario);

                    //enderecoServ.BegginTransaction();
                    //enderecoServ.Repositorie.Add(endereco);
                    //enderecoServ.CompleteTask();


                    //enderecoServ.CommitTransaction();
                    await usuarioServ.CommitTransaction();

                    var repo =  await usuarioServ.Repositorie.GetAll();
                  
                    Console.ReadKey();
                    count++;
                }
                
              
            }

        }

    }
  
}
