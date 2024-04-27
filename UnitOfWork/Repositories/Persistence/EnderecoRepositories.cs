using RabbitPublish.Model;

namespace RabbitPublish.Repositories.Persistence
{
    public class EnderecoRepositories : IEnderecoRepositories
    {
        private UserDbContext _userDbContext;
        public EnderecoRepositories(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public void Add(Endereco endereco)
        {
            _userDbContext.Enderecos.Add(endereco);
        }

        public void Delete(Guid id)
        {
            var endereco = _userDbContext.Enderecos.Find(id);
        
           _userDbContext.Enderecos.Remove(endereco!);
        }

        public Endereco Get(Guid id)
        {
            return _userDbContext.Enderecos.Find(id)!;   
        }

        public IEnumerable<Endereco> GetAll()
        {
            return _userDbContext.Enderecos.ToList();
        }
    }
}
