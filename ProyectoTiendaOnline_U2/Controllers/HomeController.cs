using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoTiendaOnline_U2.Models.ViewModels;
using ProyectoTiendaOnline_U2.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProyectoTiendaOnline_U2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Catalogo()
        {
            List<ListProductoViewModel> listaProductos;
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                listaProductos = (from p in db.producto
                                  from c in db.categoria
                                  where (p.id_categoria.Equals(c.id_categoria))
                                  select new ListProductoViewModel
                                  {
                                      id_producto = p.id_producto,
                                      nombre_producto = p.nombre_producto,
                                      precio = p.precio,
                                      stock = p.stock,
                                      categoria = c.nombre_categoria,
                                      estado = p.estado
                                  }).ToList();


            }
            return View(listaProductos);
        }
        
    }
}