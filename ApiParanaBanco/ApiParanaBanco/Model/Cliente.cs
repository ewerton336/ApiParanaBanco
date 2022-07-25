

namespace ApiParanaBanco.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public void ValidarNome(Cliente cliente)
        {
            if (cliente.Nome.Length < 3)
            {
                throw new Exception("Nome inválido!");
            }
        }

        public void ValidarEmail(Cliente cliente)
        {
            if (!cliente.Email.Contains('@'))
            {
                throw new Exception("E-mail inválido!");
            }
        }

    }

  
}
