using System.ComponentModel.DataAnnotations;

namespace Bendecido.ViewModels
{
    public class EditorVendaSalgadoViewModel
    {
        public int VendaId { get; set; }
        
        public int ClienteId { get; set; }
       
        public int EstoqueId { get; set; }
        
        public int Quantidade { get; set; }    

    }
}