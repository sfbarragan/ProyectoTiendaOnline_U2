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
        public ActionResult Eliminar(int id)
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                var oMetodo = db.modo_pago.Find(id);
                db.modo_pago.Remove(oMetodo);
                db.SaveChanges();
            }
            return Redirect("~/Metodos");

        }
        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(MetodosViewModel metodosView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using(sistema_ventasEntities db = new sistema_ventasEntities())
                    {
                        var oMetodo = new modo_pago();
                        oMetodo.nombre = metodosView.nombre;
                        db.modo_pago.Add(oMetodo);
                        db.SaveChanges();
                    }
                    return Redirect("~/Metodos");
                }
                return View(metodosView);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
      
    }
}