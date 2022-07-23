using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoTiendaOnline_U2.Models.ViewModel
{
    public class ProductoViewModel
    {
        public int id_producto { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string nombre_producto { get; set; }

        [Required]
        [Display(Name = "precio")]
        public decimal precio { get; set; }

        [Required]
        [Display(Name = "stock")]
        public int stock { get; set; }

        public byte[] foto { get; set; }

        [Display(Name = "id_categoria")]
        public int id_categoria { get; set; }

        
        [StringLength(100)]
        [Display(Name = "estado")]
        public string estado { get; set; }



        
        [StringLength(100)]
        [Display(Name = "categoria")]
        public string categoria { get; set; }

    }


}