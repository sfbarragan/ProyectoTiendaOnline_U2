using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTiendaOnline_U2.Models.ViewModel
{
    public class CarritoViewModel
    {
        public int Id_producto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public byte[] Foto { get; set; }
    }
}