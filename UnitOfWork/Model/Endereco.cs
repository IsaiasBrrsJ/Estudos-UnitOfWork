using System.Text.RegularExpressions;

namespace RabbitPublish.Model
{
    public class Endereco
    {
        public Endereco(string rua, string numero, string bairro)
        {
            Rua = rua;
            Bairro = bairro;

            Numero = numero;
            ValidaNumeroDaCasa();

          
        }
        public Guid Id { get; private set; } = default!;
        public string Rua { get; private set; } = default!;
        public string Numero { get; private set; } = default!;
        public string Bairro { get; private set; } = default!;
        public Guid UsuarioId { get; private set; } = default!;
        public virtual Usuario Usuario { get; private set; } = default!;
        public void AddUser(Usuario usuario)
        {
            ValidarUsuario(usuario);
            Usuario = usuario;
        }
        private void ValidaNumeroDaCasa()
        {
            if (!Regex.IsMatch(Numero, @"\b[0-9]{5}\b"))
                throw new InvalidOperationException("Número da casa inválido");
        }
        private void ValidarUsuario(Usuario usuario)
        {
            if (usuario is Usuario == false)
               throw new InvalidOperationException("Informe um usuário válido");
        }
    }
}
