

namespace ApiParanaBanco.Model
{
    public class Cliente
    {
        /// <summary>
        /// Id do Cliente (Gerado automaticamente)
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Nome completo do Cliente
        /// </summary>
        /// <example>Ewerton Guimarães</example>
        public string Nome { get; set; }
        /// <summary>
        /// Email do cliente
        /// </summary>
        /// <example>ewertonguimaraes2@gmail.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Validar nome do cliente (verificar se tem pelo menos 3 caracteres)
        /// </summary>
        /// <param name="cliente">Nome do Cliente</param>
        /// <exception cref="Exception">Não atendeu a expectativa</exception>
        public void ValidarNome(Cliente cliente)
        {
            if (cliente.Nome.Length < 3)
            {
                throw new Exception("Nome inválido!");
            }
        }

        /// <summary>
        /// Validar email do cliente
        /// </summary>
        /// <param name="cliente">Email do Cliente</param>
        /// <exception cref="Exception">Não atendeu a expectativa</exception>
        public void ValidarEmail(Cliente cliente)
        {
            if (!cliente.Email.Contains('@'))
            {
                throw new Exception("E-mail inválido!");
            }
        }

    }

  
}
