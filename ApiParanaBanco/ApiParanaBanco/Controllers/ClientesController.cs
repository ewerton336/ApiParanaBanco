using Microsoft.AspNetCore.Mvc;
using ApiParanaBanco.Model;

namespace ApiParanaBanco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private static List<Cliente> cliente = new();
        public static List<Cliente> Clientes { get => cliente; set => cliente = value; }

        /// <summary>
        /// Obter todos os clientes
        /// </summary>
        /// <returns>JSON com lista de Clientes cadastrados</returns>
        // GET: api/<ClientesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Clientes);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro ao obter clientes: " + ex.Message);
            }
        }

        /// <summary>
        /// Obter dados do cliente por email
        /// </summary>
        /// <param name="email">email do cliente</param>
        /// <returns></returns>
        // GET api/<ClientesController>/5
        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            try
            {
                var result = Clientes.FirstOrDefault(c => c.Email == email);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("e-mail Não encontrado na base de dados!");
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Ocorreu um erro ao obter o cliente específico: " + ex.Message);
            }
        }

        // POST api/<ClientesController>
        /// <summary>
        /// Cadastro de novo Cliente
        /// </summary>
        /// <param name="value">JSON com nome do cliente e Email</param>
        /// <returns>Status 200 (OK) Cliente Cadastrado ou 400 (BadRequest) algum erro ocorreu</returns>
        [HttpPost]
        public IActionResult Post([FromBody] Cliente value)
        {
            try
            {
                //validação de dados
                value.ValidarEmail(value);
                value.ValidarNome(value);

                //verificar se já existe mesmo email já cadastrado
                if (Clientes.FirstOrDefault(c => c.Email == value.Email) != null)
                {
                    return BadRequest("e-mail já cadastrado!");
                }
                //obter último ID
                value.Id = Clientes.Count > 0 ? Clientes.Max(c => c.Id) + 1 : 1;
                //Cadastrar cliente na Lista
                Clientes.Add(value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar cliente: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualizar um cliente cadastrado na base de dados
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <param name="value">JSON com nome completo e email para atualizar</param>
        /// <returns></returns>
        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cliente value)
        {
            try
            {
                //validação de dados
                value.ValidarEmail(value);
                value.ValidarNome(value);

                //verificar se existe um cliente cadastrado pelo ID
                Cliente? clienteOld = Clientes.FirstOrDefault(c => c.Id == id);

                //verificar se o email que está tentando cadastrar já existe na base de dados
                if (Clientes.FirstOrDefault(c=>c.Email == value.Email) != null)
                {
                    return BadRequest("Já existe um cliente cadastrado com este Email.");
                }

                if (clienteOld != null)
                {
                    Clientes.Remove(clienteOld);
                    value.Id = id;
                    Clientes.Add(value);
                    return Ok();
                }
                else return BadRequest("Cliente não encontrado na base de dados.");
            }
            catch (Exception ex)
            {

                return BadRequest("Ocorreu um erro ao tentar atualizar cliente: " + ex.Message); ;
            }

        }

        /// <summary>
        /// Deletar Cliente
        /// </summary>
        /// <param name="email">E-mail cadastrado do cliente</param>
        /// <returns></returns>
        // DELETE api/<ClientesController>/5
        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            try
            {
                Cliente? clienteOld = Clientes.FirstOrDefault(c => c.Email == email);
                if (clienteOld != null)
                {
                    Clientes.Remove(clienteOld);
                    return Ok();
                }
                else return BadRequest("Cliente não encontrado na base de dados.");
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro ao tentar deletar cliente: " + ex.Message); ;
            }
        }
    }
}
