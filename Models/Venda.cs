using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bendecido.Models;

    [Table("Venda")]
    public class Venda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }
        public Cliente? Client { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public decimal Valor { get; set; }
        public DateOnly DataCompra { get; set; } 

        [Required]
        public string? Endereco { get; set; }

        [Required]
        public int Num { get; set; }
        
        public string? Complemento { get; set; }

        [Required]
        public DateOnly DataEntrega { get; set; }

        [Required]
        public string? HoraEntrega { get; set; }

    }
