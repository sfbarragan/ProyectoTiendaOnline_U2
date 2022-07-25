using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTiendaOnline_U2.Models.ViewModel
{
    public class DetalleFacturaViewModel
    {
        public int id_detalle { get; set; }
        public int id_factura { get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal total { get; set; }
        public decimal IVA { get; set; }
    }
}