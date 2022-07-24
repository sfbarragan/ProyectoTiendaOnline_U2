using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTiendaOnline_U2.Models.ViewModel
{
    public class ListMetodosViewModel
    {
        
        public int id_modopago { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
    }
}