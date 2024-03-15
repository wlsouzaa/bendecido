using Bendecido.Data;
using Bendecido.Models;
using Bendecido.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CrudClientes.Controller
{
    [ApiController]

    public class CrudCliente : ControllerBase
    {
    [HttpGet("/cliente")]
        public List<Cliente> GetClientes([FromServices] AppDbContext context) => context.Clientes.ToList();

        [HttpGet("/cliente/telefone/{telefone}")]

        public async Task<List<Cliente>> GetByTelefoneClienteAsync(
                [FromRoute] String telefone,
                
                [FromServices] AppDbContext context)
        {
           
            var cliente = await context
                .Clientes
                .AsNoTracking() 
                .Where(m => EF.Functions.Like(m.Telefone, "%" + telefone + "%"))
                .ToListAsync();

                return cliente;
        }

        [HttpGet("/clientes/nome/{nome}")]

        public async Task<List<Cliente>> GetByNomeClienteAsync(
                [FromRoute] String nome,
                
                [FromServices] AppDbContext context)
        {
           
            var cliente = await context
                .Clientes
                .AsNoTracking() 
                .Where(m => EF.Functions.Like(m.Nome, "%" + nome + "%"))
                .ToListAsync();

                return cliente;
        }

        [HttpPost("/cliente")]

        public async Task<IActionResult> PostClienteAsync(
            [FromBody] Cliente model,
            [FromServices] AppDbContext context)
        {
            var cliente = new Cliente
                {
                    Nome = model.Nome,
                    Telefone = model.Telefone
                };
                await context.Clientes.AddAsync(cliente);
                await context.SaveChangesAsync();
            
            return Created($"cliente/{cliente.Id}",model);
        }

        [HttpPut("/cliente/{id:int}")]

        public async Task<IActionResult> PutByIdClienteAsync(
            [FromRoute] int id,
            [FromBody] EditorClienteViewModel model,
            [FromServices] AppDbContext context)
        {

            var cliente = await context
                .Clientes
                .FirstOrDefaultAsync(x => x.Id == id); 
            
            if (cliente == null)
                return NotFound(new ResultViewModel<Cliente>("Objeto não encontrado"));
            
            cliente.Nome = model.Nome;
            cliente.Telefone = model.Telefone;

            context.Clientes.Update(cliente);
            await context.SaveChangesAsync();

            return Created($"cliente/{cliente.Id}",model);
        }

        [HttpDelete("/cliente/{id:int}")]

        public async Task<IActionResult> DeleteByIdClienteAsync(
            [FromRoute] int id,
            [FromBody] EditorClienteViewModel model,
            [FromServices] AppDbContext context)
        {

            var cliente = await context
                .Clientes
                .FirstOrDefaultAsync(x => x.Id == id); 
            
            if (cliente == null)
                return NotFound(new ResultViewModel<Cliente>("Objeto não encontrado"));
            
            cliente.Nome = model.Nome;
            cliente.Telefone = model.Telefone;

            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();

            return Created($"cliente/{cliente.Id}",model);
        }
    }
}