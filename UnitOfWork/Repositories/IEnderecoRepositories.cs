using RabbitPublish.Model;

namespace RabbitPublish.Repositories
{
    public interface IEnderecoRepositories
    {
        void Add(Endereco endereco);
        Endereco Get(Guid id);
        void Delete(Guid id);
        IEnumerable<Endereco> GetAll();
    }
}
