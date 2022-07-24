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
        [Display(Name="Nombre Método")]
        public string nombre { get; set; }

    }
}