using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bendecido.Models
{
    [Table("VendaSalgado")]
    public class VendaSalgado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("VendaId")]        
        public int VendaId { get; set; }
        public Venda? Vend { get; set; }

        [Required]
        [ForeignKey("ClienteId")]        
        public int ClienteId { get; set; }
        public Cliente? Client { get; set; }

        [Required]
        [ForeignKey("EstoqueId")]        
        public int EstoqueId { get; set; }
        public Estoque? Estoq { get; set; }
        
        [Required]
        public int Quantidade { get; set; }
    }
}