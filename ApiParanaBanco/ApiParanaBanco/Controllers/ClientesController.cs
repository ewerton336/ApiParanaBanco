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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientesController>
        [HttpPost]
        public IActionResult Post([FromBody] Cliente value)
        {
            try
            {
                if (Clientes.FirstOrDefault(c => c.Email == value.Email) != null)
                {
                    return BadRequest("e-mail já cadastrado!");
                }
                value.Id = Clientes.Count > 0 ? Clientes.Max(c => c.Id) + 1 : 1;
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
