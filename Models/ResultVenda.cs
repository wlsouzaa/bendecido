using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bendecido.Models;
[Table("ResultVenda")]
[Keyless]
public class ResultVenda
{
    
    public String? Nome { get; set; }
    public int Quantidade { get; set; }
    public Decimal Valor { get; set; }
}