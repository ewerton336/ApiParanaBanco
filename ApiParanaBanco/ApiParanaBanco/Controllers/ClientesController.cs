using Microsoft.AspNetCore.Mvc;
using ApiParanaBanco.Model;
using System.Net;

namespace ApiParanaBanco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClientesController : ControllerBase
    {
        private static List<Cliente> cliente = new();
        public static List<Cliente> Clientes { get => cliente; set => cliente = value; }

        /// <summary>
        /// Obter todos os clientes
        /// </summary>
        /// <remarks> Exemplo de requisição:
        /// 
        /// GET api/Clientes
        /// </remarks>
        /// <returns>JSON com lista de Clientes cadastrados</returns>
        /// <response code="200">Retorna a lista contendo os Clientes encontrados.</response>
        /// <response code="400">Ocorreu um erro ao obter clientes: (Detalhes do erro)</response>
        // GET: api/<ClientesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<Cliente>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
        /// <remarks> Exemplo de requisição:
        /// 
        /// GET api/Clientes/email%40dominio.com.br
        /// </remarks>
        /// <returns>JSON com os dados do cliente</returns>
        /// <response code="200">Retorna os dados do cliente com base no email informado</response>
        /// <response code="400">Ocorreu um erro ao obter o cliente específico: (Detalhes do erro)</response>
        // GET api/<ClientesController>/5
        [HttpGet("{email}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
        /// <remarks> Exemplo de requisição:
        /// 
        /// POST api/Clientes/
        /// </remarks>
        /// <returns>Status da requisição</returns>
        /// <response code="200">Cliente cadastrado com sucesso.</response>
        /// <response code="400">Erro ao cadastrar cliente: (Detalhes do erro)</response>
        /// <response code="401">Caso já tenha um email idêntico cadastrado: Já existe um cliente cadastrado com este Email.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
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
                    return Unauthorized("Já existe um cliente cadastrado com este Email.");
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
        /// <param name="id">Id do cliente a atualizar</param>
        /// <param name="value">JSON com nome completo e email para atualizar</param>
        /// <remarks> Exemplo de requisição:
        /// 
        /// PUT api/Clientes/
        /// </remarks>
        /// <returns>Status da requisição</returns>
        /// <response code="200">Cliente atualizado com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar atualizar cliente:  (Detalhes do erro)</response>
        /// <response code="401">Caso já tenha um email idêntico cadastrado: Já existe um cliente cadastrado com este Email.</response>
        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
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
                if (Clientes.FirstOrDefault(c => c.Email == value.Email) != null)
                {
                    return Unauthorized("Já existe um cliente cadastrado com este Email.");
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
        /// <remarks> Exemplo de requisição:
        /// 
        /// DELETE api/Clientes/
        /// </remarks>
        /// <returns>Status da requisição</returns>
        /// <response code="200">Cliente removido com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar deletar cliente: (Detalhes do erro)</response>
        // DELETE api/<ClientesController>/5
        [HttpDelete("{email}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
