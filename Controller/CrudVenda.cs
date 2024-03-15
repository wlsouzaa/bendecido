using Bendecido.Data;
using Bendecido.Models;
using Bendecido.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CrudVenda.Controller
{
    [ApiController]

    public class CrudVenda : ControllerBase
    {
        [HttpGet("/venda")]
        public List<Venda> GetVenda([FromServices] AppDbContext context)
        {
            var venda = context
                .Vendas
                .AsNoTracking()
                .Include(x => x.Client)
                .ToList();

            return venda;
        }

        [HttpGet("venda/cliente/{cliente}")]

        public async Task<List<ResultVenda>> GetSomaVendaAsync(
                [FromRoute] String cliente,
                [FromServices] AppDbContext context)
        {
            var venda = await context
                .ResultVendas
                .FromSql($"Consulta_Venda_Cliente {cliente}")
                .ToListAsync();

                return venda;
        }

        [HttpGet("/venda/dataentrega/{dataentrega}")]

        public async Task<List<Venda>> GetByDiaMesVendaAsync(
               [FromRoute] String dataentrega,               
               [FromServices] AppDbContext context)
       {
            
            var venda = await context
                .Vendas
                .AsNoTracking()
                .Include(x => x.Client)
                .Where(m => EF.Functions.Like(m.DataEntrega.ToString(), "%" + dataentrega + "%" ))
                .ToListAsync();

               return venda;
       }

       [HttpGet("/venda/datainiciofim/{startdate}/{enddate}")]

        public async Task<List<Venda>> GetEntreDatasVendaAsync(
               [FromRoute] DateOnly startdate, DateOnly enddate,              
               [FromServices] AppDbContext context)
       {           
            
            var venda = await context
               .Vendas
               .Where(p => p.DataEntrega >= startdate && p.DataEntrega <= enddate)
               .ToListAsync();

               return venda;
       }

        [HttpPost("/venda")]

       public async Task<IActionResult> PostVendaAsync(
           [FromBody] Venda model,
           [FromServices] AppDbContext context)
       {
        model.DataCompra = DateOnly.FromDateTime(DateTime.Now);
           var venda = new Venda
               {
                   ClienteId = model.ClienteId,
                   Quantidade = model.Quantidade,
                   Valor = model.Valor,
                   DataCompra = model.DataCompra,
                   Endereco = model.Endereco,
                   Num = model.Num,
                   Complemento = model.Complemento,
                   DataEntrega = model.DataEntrega,
                   HoraEntrega = model.HoraEntrega,
               };
               await context.Vendas.AddAsync(venda);
               await context.SaveChangesAsync();
           
           return Created($"cliente/{venda.Id}",model);
       }

       [HttpPut("/venda/{id:int}")]

       public async Task<IActionResult> PutByIdVendaAsync(
           [FromRoute] int id,
           [FromBody] EditorVendaViewModel model,
           [FromServices] AppDbContext context)
       {

           var venda = await context
               .Vendas
               .FirstOrDefaultAsync(x => x.Id == id); 
           
           if (venda == null)
               return NotFound( new ResultViewModel<Venda>("Conteúdo não encontrado"));
           
           venda.ClienteId = model.ClienteId;
           venda.Quantidade = model.Quantidade;
           venda.Valor = model.Valor;
           venda.Endereco = model.Endereco;
           venda.DataEntrega = model.DataEntrega;
           venda.HoraEntrega = model.HoraEntrega;

           context.Vendas.Update(venda);
           await context.SaveChangesAsync();

           return Created($"cliente/{venda.Id}",model);
       }

        [HttpDelete("/venda/{id:int}")]

        public async Task<ActionResult<Venda>> DeleteByIdVendaAsync(
           [FromRoute] int id,
           [FromBody] Venda model,
           [FromServices] AppDbContext context)
        {

           var venda = await context
               .Vendas
               .FirstOrDefaultAsync(x => x.Id == id); 
           
           if (venda == null)
               return NotFound( new ResultViewModel<Venda>("Conteúdo não encontrado"));
           
           venda.ClienteId = model.ClienteId;
           venda.Quantidade = model.Quantidade;
           venda.Valor = model.Valor;
           venda.DataCompra = model.DataCompra;
           venda.Endereco = model.Endereco;
           venda.DataEntrega = model.DataEntrega;
           venda.HoraEntrega = model.HoraEntrega;

           context.Vendas.Remove(venda);
           await context.SaveChangesAsync();

           return Created($"cliente/{venda.Id}",model);
        }
    }
}