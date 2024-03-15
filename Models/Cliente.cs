using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bendecido.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        public string? Telefone { get; set; }
    }
}