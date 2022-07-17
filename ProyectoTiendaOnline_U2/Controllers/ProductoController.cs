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
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            List<ListProductoViewModel> listaProductos;
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                listaProductos = (from p in db.producto
                                  from c in db.categoria
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
        public ActionResult obtenerImagen(int id)
        {
            sistema_ventasEntities db = new sistema_ventasEntities();


            producto model = db.producto.Find(id);

            byte[] byteImage = model.foto;
            System.IO.MemoryStream memoryStream = new MemoryStream(byteImage);

            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }
    }
}