using Bendecido.Data;
using Bendecido.Models;
using Bendecido.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CrudEstoques.Controller
{
    [ApiController]

    public class CrudEstoque : ControllerBase
    {
        [HttpGet("/estoque")]
        public List<Estoque> GetEstoque([FromServices] AppDbContext context) => context.Estoques.ToList();

        [HttpGet("/estoque/{id:int}")]

        public async Task<IActionResult> GetByIdEstoqueAsync(
                [FromRoute] int id,
                [FromServices] AppDbContext context)
        {
            var estoque = await context
                    .Estoques
                    .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(estoque);
        }

        [HttpGet("/estoque/salgado/{salgado}")]

        public async Task<List<Estoque>> GetByStringEstoqueAsync(
                [FromRoute] String salgado,
                [FromServices] AppDbContext context)
        {
           
            var estoque = await context
                .Estoques
                .AsNoTracking() 
                .Where(m => EF.Functions.Like(m.Salgado, "%" + salgado + "%"))
                .ToListAsync();

                return estoque;
        }

        [HttpPost("/estoque")]

        public async Task<IActionResult> PostEstoqueAsync(
            [FromBody] Estoque model,
            [FromServices] AppDbContext context)
        {
            var estoque = new Estoque
                {
                    Salgado = model.Salgado,
                    Quantidade = model.Quantidade
                };
                await context.Estoques.AddAsync(estoque);
                await context.SaveChangesAsync();
            
            return Created($"estoque/{estoque.Id}",model);
        }

        [HttpPut("/estoque/{id:int}")]

        public async Task<ActionResult<Estoque>> PutByIdEstoqueAsync(
            [FromRoute] int id,
            [FromBody] Estoque model,
            [FromServices] AppDbContext context)
        {

            var estoque = await context
                .Estoques
                .FirstOrDefaultAsync(x => x.Id == id); 
            
            if (estoque == null)
                return NotFound();
            
            estoque.Salgado = model.Salgado;
            estoque.Quantidade = model.Quantidade;

            context.Estoques.Update(estoque);
            await context.SaveChangesAsync();

            return Created($"estoque/{estoque.Id}",model);
        }

        [HttpDelete("/estoque/{id:int}")]

        public async Task<IActionResult> DeleteByIdEstoqueAsync(
            [FromRoute] int id,
            [FromBody] EditorEstoqueViewModel model,
            [FromServices] AppDbContext context)
        {

            var estoque = await context
                .Estoques
                .FirstOrDefaultAsync(x => x.Id == id); 
            
            if (estoque == null)
                return NotFound( new ResultViewModel<Estoque>("Conteúdo não encontrado"));
            
            estoque.Salgado = model.Salgado;
            estoque.Quantidade = model.Quantidade;

            context.Estoques.Remove(estoque);
            await context.SaveChangesAsync();

            return Created($"estoque/{estoque.Id}",model);
        }

    }
}