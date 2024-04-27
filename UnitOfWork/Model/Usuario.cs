namespace RabbitPublish.Model
{
    public class Usuario 
    {
        public Usuario(string name, int quantidadeUser )
        {
            Name = name;
            QuantidadeUser = quantidadeUser;
            
            validarQuantidade();
        }
        public Guid Id {get;set;}
        public string Name { get; private set; }  
        public string? Password { get;private set;}
        public int QuantidadeUser { get; private set;}
        public virtual Endereco Endereco { get; private set;}
        private void validarQuantidade()
        {
            if (QuantidadeUser > 4)
                throw new InvalidOperationException("erro");
        }
        private void GeneratePassword()
        {
            var alfabetoMinusculo = new char[26];
            var alfabetoMaiusculo = new char[26];

            alfabetoMaiusculo = Enumerable.Range('a', alfabetoMaiusculo.Length)
                .Select(x => (char)x)
                .ToArray();

            alfabetoMaiusculo = Enumerable.Range('A', alfabetoMaiusculo.Length)
                .Select(x => (char)x)
                .ToArray();
            
            var especiais = new char[] { '-', '@', '_', '*', '&' };

            var list = new string(new string(alfabetoMaiusculo.ToString()) + new string(alfabetoMinusculo.ToString()) + especiais);

            Password = new string(Enumerable.Range(1, 10)
                                            .Select(_ => list[Random.Shared.Next(0, list.Length)])
                                            .ToArray());  
        }
        public override string ToString()
        {
            return $"Id:{Id}\nNome:{Name}\n============\n";
        }
    }
}
