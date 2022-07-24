using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoTiendaOnline_U2.Models;
using ProyectoTiendaOnline_U2.Models.ViewModel;

namespace ProyectoTiendaOnline_U2.Controllers
{
    public class MetodosController : Controller
    {
        // GET: Metodos
        public ActionResult Index()
        {
            List<ListMetodosViewModel> listMetodos;
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                listMetodos = (from m in db.modo_pago
                               select new ListMetodosViewModel
                               {
                                   id_modopago = m.id_modopago,
                                   nombre = m.nombre
                               }).ToList();
            }
            return View(listMetodos);
        }
    }
}