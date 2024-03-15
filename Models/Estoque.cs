using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bendecido.Models
{
    [Table("Estoque")]
    public class Estoque
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Salgado { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}