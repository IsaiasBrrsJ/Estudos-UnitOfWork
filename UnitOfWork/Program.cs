using RabbitPublish.Model;
using RabbitPublish.Unit;
using RabbitPublish.Repositories;
using RabbitPublish.Repositories.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using UnitOfWork;

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
                      options.UseSqlServer(ConnectionStrings.GetConnectionString());
                  });

              }).UseConsoleLifetime();

            var host = builder.Build();
            host.Start();
        
            int count = 1;
            var unitOfWorkUser = host.Services
            .GetRequiredService<AbstractUnitOfWork<IUsuarioRepositories>>();
            var unitOfWorkEndereco = host.Services
            .GetRequiredService<AbstractUnitOfWork<IEnderecoRepositories>>();

            while (count <= 4)
            {
                Console.Clear();
                Console.WriteLine("Quantidade maxima : 4");
                Console.Write("Digite seu nome nome: ");
                var nome = Console.ReadLine();

                var usuario = new Usuario(nome!, count);

                await unitOfWorkUser.BegginTransaction();
                
                
                await unitOfWorkUser.Repositorie.Add(usuario);
                await unitOfWorkUser.CompleteTask();

                var endereco = Endereco();
                endereco.AddUser(usuario);

                unitOfWorkEndereco.Repositorie.Add(endereco);
                await unitOfWorkEndereco.CompleteTask();

                await unitOfWorkUser.CommitTransaction();

                var repo = await unitOfWorkUser.Repositorie.GetAll();
                foreach (var item in repo) { Console.WriteLine(item.ToString()); }

                Console.ReadKey();
                count++;
            }

        }
        static Endereco Endereco()
        {
            Console.Write("Rua Numero bairro : ");
            var endereco = Console.ReadLine().Split(' ');

            return new Endereco(endereco[0], endereco[1], endereco[2]);
        }

    }

}
