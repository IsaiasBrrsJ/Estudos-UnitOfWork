using RabbitPublish.Model;
using RabbitPublish.Unit;
using RabbitPublish.Repositories;
using RabbitPublish.Repositories.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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

                   services.AddScoped(typeof(AbstractUnitOfWork<>), typeof(UnitOfWork<>))
                   .AddScoped<IUsuarioRepositories, UsuarioRepositories>()
                   .AddScoped<IEnderecoRepositories, EnderecoRepositories>()
                   .AddDbContext<UserDbContext>(options =>
                   {
                       options.UseSqlServer("Server=172.17.36.84;User Id=sa;Password=MinhaSenhaFacil@123;TrustServerCertificate=True");
                   });
               }).UseConsoleLifetime();

            var host = builder.Build();

            int count = 1;
                var usuarioServ  = host.Services.GetRequiredService<AbstractUnitOfWork<IUsuarioRepositories>>();
                var enderecoServ = host.Services.GetRequiredService<AbstractUnitOfWork<IEnderecoRepositories>>();
            
                while (count <= 4)
                {
                    Console.Clear();
                    Console.WriteLine("Quantidade maxima : 4");
                    Console.Write("Digite um nome: ");
                    var nome = Console.ReadLine();

                    var usuario = new Usuario(nome!, count);

                     await usuarioServ.BegginTransaction();
                     await usuarioServ.Repositorie.Add(usuario);
                     await usuarioServ.CompleteTask();
                  

                    var endereco = new Endereco("Tico", "33333", "CTO");
                    endereco.AddUser(usuario);
                    enderecoServ.Repositorie.Add(endereco);
                    await enderecoServ.CompleteTask();



                    await usuarioServ.CommitTransaction();

                    var repo =  await usuarioServ.Repositorie.GetAll();
                    foreach(var item in repo) { Console.WriteLine(item.ToString()); }   
                    

                    Console.ReadKey();
                    count++;
                }
                
        }

    }
  
}
