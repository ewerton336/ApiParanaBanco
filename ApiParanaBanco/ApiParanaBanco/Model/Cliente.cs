namespace ApiParanaBanco.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome
        {
            get
            {
                return Nome;
            }
            set
            {
                //validar nome
                if (value.Length < 3)
                {
                    throw new Exception("Nome inválido!");
                }
            }
        }
        public string Email
        {
            get
            {
                return Email;
            }
            //validar email ao cadastrar
            set
            {
                if (!value.Contains('@') || !value.Contains('.'))
                {
                    throw new Exception("E-mail inválido!");
                }
            }
        }
    }
}
