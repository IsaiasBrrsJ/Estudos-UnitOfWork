using Microsoft.EntityFrameworkCore;
using RabbitPublish.Model;

namespace RabbitPublish.Repositories.Persistence
{
    public class UsuarioRepositories : IUsuarioRepositories
    {
        private UserDbContext _userDbContext;



        public UsuarioRepositories(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task Add(Usuario usuario)
        {
          await _userDbContext.AddAsync(usuario); 
        }

        public async Task Delete(Guid id)
        {
            var user = await _userDbContext.Usuario.FindAsync(id);
            _userDbContext.Usuario.Remove(user!);
        }

        public async Task<Usuario> Get(Guid id)
        => await _userDbContext.Usuario.FindAsync(id);

        public async Task<IEnumerable<Usuario>> GetAll()
        => await _userDbContext.Usuario.ToListAsync();
    }
}
