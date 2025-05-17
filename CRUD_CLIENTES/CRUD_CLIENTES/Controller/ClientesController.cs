using CRUD_CLIENTES.Database;
using CRUD_CLIENTES.DTO;
using CRUD_CLIENTES.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_CLIENTES.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesDbContext dbContext;

        public ClientesController(ClientesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<Cliente>> GetClientes()
        {
            try
            {
                List<Cliente> clientes = dbContext.Clientes.ToList();
                
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar clientes: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente(string id)
        {
            try
            {
                Cliente? cliente = dbContext.Clientes.FirstOrDefault(cliente => cliente.Id == id);

                if (cliente == null)
                    return NotFound("Cliente não localizado");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar clientee: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<Cliente> CreateCliente(ClienteDTO clienteDTO)
        {
            try
            {
                bool clienteValidador = dbContext.Clientes.Any(cliente => cliente.CPF == clienteDTO.CPF);

                if (clienteValidador)
                    return BadRequest("O CPF informado já está cadastrado");

                Cliente clienteNovo = new Cliente(clienteDTO.Nome,clienteDTO.Email, clienteDTO.CPF);

                dbContext.Clientes.Add(clienteNovo);

                dbContext.SaveChanges();

                return CreatedAtAction(nameof(CreateCliente), clienteNovo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir cliente: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCliente(string id, ClienteDTO clienteDTO)
        {
            try
            {
                Cliente? cliente = dbContext.Clientes.FirstOrDefault(cliente => cliente.Id == id);

                if (cliente == null)
                    return NotFound("Cliente não localizado");

                bool clienteValidador = dbContext.Clientes.Any(cliente => cliente.CPF == clienteDTO.CPF && cliente.Id != id);

                if (clienteValidador)
                    return BadRequest("CPF já cadastrado");
                
                cliente.Nome = clienteDTO.Nome;
                cliente.Email = clienteDTO.Email;
                cliente.CPF = clienteDTO.CPF;

                dbContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao editar cliente: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(string id)
        {
            try
            {
                Cliente? clienteValidador = dbContext.Clientes.FirstOrDefault(cliente => cliente.Id ==id);

                if (clienteValidador == null)
                    return NotFound("Não foi possivel deleter cliente");

                dbContext.Remove(clienteValidador);
                dbContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao remover cliente: {ex.Message}");
            }
        }
    }
}
