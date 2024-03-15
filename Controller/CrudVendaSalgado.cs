using Bendecido.Data;
using Bendecido.Models;
using Bendecido.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace CrudVendaSalgado.Controller
{
    [ApiController]

    public class CrudVendaSalgado : ControllerBase
    {
        [HttpGet("/vendasalgado")]
        public List<VendaSalgado> GetVendaSalgado([FromServices] AppDbContext context)
        {
            var vendaSalgado = context
                .VendaSalgados
                .AsNoTracking()
                .Include(x => x.Client)
                .ToList();

            return vendaSalgado;
        }

        [HttpGet("/vendasalgado/venda/{vendaid:int}")]

        public async Task<IActionResult> GetByIdVendaSalgadoAsync(
                [FromRoute] int vendaid,
                [FromServices] AppDbContext context)
        {
            var vendasalgado = await context
                    .VendaSalgados
                    .AsNoTracking()
                    .Include(x=>x.Estoq)
                    .FirstOrDefaultAsync(x => x.VendaId == vendaid);
            
            if (vendasalgado == null)
                return NotFound(new ResultViewModel<VendaSalgado>("Não encontrado"));

            return Ok(new ResultViewModel<VendaSalgado>(vendasalgado));
        }

        [HttpGet("/vendasalgado/cliente/{cliente}")]

        public async Task<List<VendaSalgado>> GetByClienteIdVendaSalgadoAsync(
                [FromRoute] String cliente,
                [FromServices] AppDbContext context)
        {    
                   
            var vendasalgado = await context
                .VendaSalgados
                .AsNoTracking()
                .Include(x => x.Client)
                .Include(x => x.Estoq)
                .Where(x => EF.Functions.Like(x.Client!.Nome, "%" + cliente + "%"))
                .ToListAsync();

                return vendasalgado;           
        }

        [HttpPost("/vendasalgado")]

       public async Task<IActionResult> PostVendaSalgadoAsync(
           [FromBody] VendaSalgado model,
           [FromServices] AppDbContext context)
       {
           var vendasalgado = new VendaSalgado
               {
                   VendaId = model.VendaId,
                   ClienteId = model.ClienteId,
                   EstoqueId = model.EstoqueId,
                   Quantidade = model.Quantidade,
               };
               await context.VendaSalgados.AddAsync(vendasalgado);
               await context.SaveChangesAsync();

           
           return Created($"cliente/{vendasalgado.Id}", vendasalgado);
       }

       [HttpPut("/vendasalgado/{id:int}")]

       public async Task<IActionResult> PutByIdVendaSalgadoAsync(
           [FromRoute] int id,
           [FromBody] EditorVendaSalgadoViewModel model,
           [FromServices] AppDbContext context)
       {

           var vendasalgado = await context
               .VendaSalgados
               .FirstOrDefaultAsync(x => x.Id == id); 
           
           if (vendasalgado == null)
               return NotFound( new ResultViewModel<VendaSalgado>("Conteúdo não encontrado"));
           
           vendasalgado.VendaId = model.VendaId;
           vendasalgado.ClienteId = model.ClienteId;
           vendasalgado.EstoqueId = model.EstoqueId;
           vendasalgado.Quantidade = model.Quantidade;

           context.VendaSalgados.Update(vendasalgado);
           await context.SaveChangesAsync();

           return Ok(vendasalgado);
       }

       [HttpDelete("/vendasalgado/{id:int}")]

        public async Task<IActionResult> DeleteByIdVendaSalgadoAsync(
            [FromRoute] int id,
            [FromBody] VendaSalgado model,
            [FromServices] AppDbContext context)
        {

            var vendasalgado = await context
                .VendaSalgados
                .FirstOrDefaultAsync(x => x.Id == id); 
            
            if (vendasalgado == null)
               return NotFound( new ResultViewModel<VendaSalgado>("Conteúdo não encontrado"));
            
            vendasalgado.VendaId = model.VendaId;
            vendasalgado.ClienteId = model.ClienteId;
            vendasalgado.EstoqueId = model.EstoqueId;
            vendasalgado.Quantidade = model.Quantidade;

            context.VendaSalgados.Remove(vendasalgado);
            await context.SaveChangesAsync();

            return Created($"vendasalgado/{vendasalgado.Id}",model);
        }
    }
}