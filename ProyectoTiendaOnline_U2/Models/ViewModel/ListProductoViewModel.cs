using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTiendaOnline_U2.Models.ViewModels
{
    public class ListProductoViewModel
    {
        public int id_producto { get; set; }
        public string nombre_producto { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public byte[] foto { get; set; }
        public int id_categoria { get; set; }
        public string estado { get; set; }
        public string categoria { get; set; }

        
    }
}
