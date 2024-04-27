namespace RabbitPublish.Model
{
    public class Usuario 
    {
        public Usuario(string name, int quantidadeUser)
        {
           
            Name = name;
            QuantidadeUser = quantidadeUser;

            validarQuantidade();
        }

        public Guid Id {get;set;}
        public string Name { get; private set; }  

        public string? Password { get;private set;}
        public int QuantidadeUser { get; private set;}
        public Endereco Endereco { get; private set;}
        private void validarQuantidade()
        {
            if (QuantidadeUser > 4)
                throw new InvalidOperationException("erro");
        }

        private void GeneratePassword()
        {
            var alfabetoMinusculo = new char[26];
            var alfabetoMaiusculo = new char[26];
            var especiais = new char[] { '-', '@', '_', '*', '&' };

        }

        public override string ToString()
        {
            return $"Id:{Id}\nNome:{Name}\n============\n";
        }
    }
}
