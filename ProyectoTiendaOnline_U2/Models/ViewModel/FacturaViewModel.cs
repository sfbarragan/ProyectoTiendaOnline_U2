using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTiendaOnline_U2.Models.ViewModel
{
    public class FacturaViewModel
    {
        public int id_factura { get; set; }
        public int id_cliente { get; set; }
        public int id_modopago { get; set; }
        public DateTime fecha { get; set; }
        public string numeroFactura { get; set; }
    }
}