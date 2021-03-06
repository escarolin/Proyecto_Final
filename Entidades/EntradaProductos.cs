using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Proyecto_Final.Entidades
{
    public class EntradaProductos
    {
        [Key]
        public int EntradaProductoId { get; set; }
        public int ProductoId { get; set; }
        public string NombreProvedor { get; set; }
        public double Cantidad { get; set; }
        public DateTime FechaEntrada { get; set; } = DateTime.Now;

        //覧覧覧覧覧覧覧覧覧覧覧覧覧夕 ForeingKeys ]覧覧覧覧覧覧覧覧覧覧覧覧覧�
        [ForeignKey("UsuarioId")]
        public Usuarios usuarios { get; set; }

        [ForeignKey("ProductoId")]
        public Productos productos { get; set; }
    }
}