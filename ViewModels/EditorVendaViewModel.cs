using System.ComponentModel.DataAnnotations;

namespace Bendecido.ViewModels
{
    public class EditorVendaViewModel
    {
        
        public int Id { get; set; }

        public int ClienteId { get; set; }
        
        public int Quantidade { get; set; }
        
        public decimal Valor { get; set; }
               
        public string? Endereco { get; set; }
        
        public int Num { get; set; }
        
        public string? Complemento { get; set; }
        
        public DateOnly DataEntrega { get; set; } 
        
        public string? HoraEntrega { get; set; }

    }
}