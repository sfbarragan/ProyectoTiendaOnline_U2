using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoTiendaOnline_U2.Models.ViewModels;
using ProyectoTiendaOnline_U2.Models.ViewModel;
using ProyectoTiendaOnline_U2.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Helpers;

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

        public ActionResult Nuevo()
        {
            List<CategoriasViewModel> lst = null;
            using (Models.sistema_ventasEntities db = new Models.sistema_ventasEntities())
            {
                 lst = (
                        from d in db.categoria
                        select new CategoriasViewModel
                        {
                            id_categoria = d.id_categoria,
                            nombre_categoria = d.nombre_categoria,
                            descripcion = d.descripcion
                        }
                    ).ToList();
            }
            

            List<SelectListItem> items = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    
                
                    Text = d.nombre_categoria.ToString(),
                    Value = d.id_categoria.ToString(),
                    Selected = false
                };
            } );

            ViewBag.items = items;

            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(ProductoViewModel productoModel, string categorias, string estados)
        {
            try
            {
                //Validar el Modelo
                if (ModelState.IsValid)
                {

                    HttpPostedFileBase FileBase = Request.Files[0];

                    WebImage image = new WebImage(FileBase.InputStream);

                    productoModel.foto = image.GetBytes();

                    //Conexion a la base de datos y paso de datos del modelo a un objeto tipo cliente
                    using (sistema_ventasEntities db = new sistema_ventasEntities())
                    {
                        var oProducto = new producto();
                        oProducto.nombre_producto = productoModel.nombre_producto;
                        oProducto.precio = productoModel.precio;
                        oProducto.stock = productoModel.stock;
                        oProducto.foto = productoModel.foto;
                        oProducto.id_categoria = int.Parse(categorias);
                        oProducto.estado = estados;

                        //Almacenar en la base de datos el objeto cliente
                        db.producto.Add(oProducto);
                        db.SaveChanges();
                    }
                    return Redirect("~/Producto");
                }
                return View(productoModel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SelectListItem> ObtenerCategoria()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Hogar",
                    Value = "1"
                },
                new SelectListItem()
                {
                    Text = "Laptop",
                    Value = "2"
                },
                new SelectListItem()
                {
                    Text = "Computador de Escritorio",
                    Value = "3"
                },
                new SelectListItem()
                {
                    Text = "Accesorios",
                    Value = "4"
                }
            };



        }

        public List<SelectListItem> ObtenerEstado()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Y",
                    Value = "Y"
                },
                new SelectListItem()
                {
                    Text = "N",
                    Value = "N"
                }
            };
        }
    }
}