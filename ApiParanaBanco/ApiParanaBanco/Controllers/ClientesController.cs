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

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
