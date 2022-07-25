using ProyectoTiendaOnline_U2.Models;
using ProyectoTiendaOnline_U2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoTiendaOnline_U2.Controllers
{
    public class CarritoController : Controller
    {
        private sistema_ventasEntities objetoEntities;
        private List<CarritoViewModel> listaCarritoViewModel;
        public CarritoController()
        {
            objetoEntities = new sistema_ventasEntities();
            listaCarritoViewModel = new List<CarritoViewModel>();
        }
        // GET: Carrito
        [HttpPost]
        public JsonResult Index(int id_producto)
        {
            CarritoViewModel oCarritoViewModel = new CarritoViewModel();
            producto oProducto = objetoEntities.producto.Single(model => model.id_producto == id_producto);
            if (Session["CartCounter"] != null)
            {
                listaCarritoViewModel = Session["CartItem"] as List<CarritoViewModel>;
            }
            if (listaCarritoViewModel.Any(model => model.Id_producto == id_producto))
            {
                oCarritoViewModel.Cantidad += + 1;
                oCarritoViewModel.Total = oCarritoViewModel.Cantidad * oCarritoViewModel.PrecioUnitario;
            }
            else
            {
                oCarritoViewModel.Id_producto = id_producto;
                oCarritoViewModel.Foto = oProducto.foto;
                oCarritoViewModel.Nombre = oProducto.nombre_producto;
                oCarritoViewModel.Cantidad = 1;
                oCarritoViewModel.Total = oProducto.precio;
                oCarritoViewModel.PrecioUnitario = oProducto.precio;

                listaCarritoViewModel.Add(oCarritoViewModel);
            }

            Session["CartCounter"] = listaCarritoViewModel.Count;
            Session["CartItem"] = listaCarritoViewModel;

            return Json(new { Success = true, Counter = listaCarritoViewModel.Count }, JsonRequestBehavior.AllowGet);
        }

        private int Existe(int id)
        {
            List<CarritoViewModel> cart = (List<CarritoViewModel>)Session["CartItem"]; // Put all the items from the "Session["cart"] into the list cart

            for (int i = 0; i < cart.Count; i++)

                if (cart[i].Id_producto == id) // If the cart contains the product those ID is provided 

                    return i; // Then return the number of Products in the cart

            return -1; // Else return -1
        }

        public ActionResult Eliminar(int id_productoCarrito)
        {
                int index = Existe(id_productoCarrito); // index = Product ID from in the Cart only

                List<CarritoViewModel> cart = (List<CarritoViewModel>)Session["CartItem"];

                cart.RemoveAt(index); // Remove product based on the Product ID provided.

                Session["CartItem"] = cart; // Update Session["cart"]

                listaCarritoViewModel = Session["CartItem"] as List<CarritoViewModel>;

                return Redirect("~/Carrito/CarritoCompra");

        }
        public ActionResult CarritoCompra()
        {
            listaCarritoViewModel = Session["CartItem"] as List<CarritoViewModel>;
            return View(listaCarritoViewModel);
        }
        [HttpPost]
        public ActionResult AñadirFactura()
        {
            int Id_Factura = 0;
            decimal IVA = 0;
            listaCarritoViewModel = Session["CartItem"] as List<CarritoViewModel>;
            factura oFactura = new factura()
            {
                id_cliente = 1,
                id_modopago = 1,
                fecha = DateTime.Now,
                numeroFactura = String.Format("{0:ddmmyyyyHHmmss}", DateTime.Now)
            };
            objetoEntities.factura.Add(oFactura);
            objetoEntities.SaveChanges();
            Id_Factura = oFactura.id_factura;
            foreach (var producto in listaCarritoViewModel)
            {
                detalle oDetalleFactura = new detalle();
                IVA = producto.Total * (decimal)0.12;
                oDetalleFactura.precio = producto.Total;
                oDetalleFactura.id_producto = producto.Id_producto;
                oDetalleFactura.id_factura = Id_Factura;
                oDetalleFactura.IVA = IVA;
                oDetalleFactura.total = producto.Total + IVA;
                oDetalleFactura.cantidad = producto.Cantidad;
                oDetalleFactura.precioUnitario = producto.PrecioUnitario;

                objetoEntities.detalle.Add(oDetalleFactura);
                objetoEntities.SaveChanges();
            }
            Session["CartItem"] = null;
            Session["CartCounter"] = null;
            return Redirect("/Home/Index");
        }
    }
}