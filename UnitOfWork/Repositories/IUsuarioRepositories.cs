using RabbitPublish.Model;

namespace RabbitPublish.Repositories
{
    public interface IUsuarioRepositories
    {
        Task Add(Usuario usuario);
        Task<Usuario> Get(Guid id);
        Task Delete(Guid id);
        Task<IEnumerable<Usuario>> GetAll();
    }
}
