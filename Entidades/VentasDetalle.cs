using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Proyecto_Final.Entidades{
    public class VentasDetalle{
        [Key]
        public int Id { get; set; }
        public int VentaId{ get; set; } 
        public int UsuarioId { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }


        
    }
}