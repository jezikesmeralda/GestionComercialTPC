using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace GestionComercialWeb
{
    public partial class Ventas : PaginaBase
    {
        private ProductoNegocio prodNegocio = new ProductoNegocio();
        private VentaNegocio ventaNegocio = new VentaNegocio();
        private ClienteNegocio clienteNegocio = new ClienteNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
            if (Session["Carrito"] == null)
            {
                Session["Carrito"] = new List<DetalleVenta>();
            }
            ActualizarGrillaYTotal();
        }
        private void CargarClientes()
        {
            try
            {
                ddlCliente.DataSource = clienteNegocio.Listar()
                    .Select(x => new
                    {
                        x.Id,
                        Nombre = x.Nombre + " " + x.Apellido
                    });

                ddlCliente.DataTextField = "Nombre";
                ddlCliente.DataValueField = "Id";
                ddlCliente.DataBind();

                ddlCliente.Items.Insert(0, new ListItem("Seleccione un Cliente...", "0"));
            }
            catch (Exception)
            {
            }
        }
        

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscarProducto.Text.Trim();
            if (string.IsNullOrEmpty(busqueda)) return;

            Producto encontrado = prodNegocio.BusquedaNombre(busqueda);

            if (encontrado != null)
            {
                Session["ProductoSeleccionado"] = encontrado;

                lblProductoEncontrado.Text = encontrado.NombreProducto;
                lblStockActual.Text = encontrado.StockActual.ToString();

                lblPrecioVenta.Text = encontrado.PrecioVenta.ToString("F2");
            }
            else
            {
                Session["ProductoSeleccionado"] = null;
                lblProductoEncontrado.Text = "Producto no encontrado";
                lblStockActual.Text = "-";
                lblPrecioVenta.Text = "-";
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Session["ProductoSeleccionado"] == null) return;
            if (string.IsNullOrEmpty(txtCantidad.Text) || int.Parse(txtCantidad.Text) <= 0) return;

            Producto prod = (Producto)Session["ProductoSeleccionado"];
            int cantidad = int.Parse(txtCantidad.Text);

            if (cantidad > prod.StockActual)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('¡Stock insuficiente!');", true);
                return;
            }
            List<DetalleVenta> listaTemporal = (List<DetalleVenta>)Session["Carrito"];

            DetalleVenta nuevoDetalle = new DetalleVenta();
            nuevoDetalle.Producto = prod;
            nuevoDetalle.Cantidad = cantidad;
            nuevoDetalle.PrecioUnitario = prod.PrecioVenta; 


            listaTemporal.Add(nuevoDetalle);

            Session["Carrito"] = listaTemporal;

            LimpiarBuscadorProducto();

            ActualizarGrillaYTotal();
        }
    

protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            
            List<DetalleVenta> listaTemporal = (List<DetalleVenta>)Session["Carrito"];

           
            
            if (ddlCliente.SelectedValue == "0" || listaTemporal == null || listaTemporal.Count == 0) return;

            try
            {
                Venta nuevaVenta = new Venta();

                nuevaVenta.Cliente = new Cliente { Id = int.Parse(ddlCliente.SelectedValue) };

                nuevaVenta.Vendedor = new Usuario { Id = 1 };
                nuevaVenta.Detalles = listaTemporal;

                
                decimal totalFinal = 0;
                foreach (var item in listaTemporal) totalFinal += item.Subtotal;
                nuevaVenta.Total = totalFinal;

                string numeroFactura = ventaNegocio.Alta(nuevaVenta);
                
                Session["Carrito"] = null;
                
               Response.Redirect("Ventas.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("ERROR: " + ex.Message);
                
            }
        }

        private void ActualizarGrillaYTotal()
        {
            List<DetalleVenta> listaTemporal = (List<DetalleVenta>)Session["Carrito"];

            var datosParaGrilla = new List<object>();
            decimal acumuladorTotal = 0;

            if (listaTemporal != null)
            {
                foreach (var item in listaTemporal)
                {
                    datosParaGrilla.Add(new{Producto = item.Producto.NombreProducto, Cantidad = item.Cantidad, Precio = item.PrecioUnitario, Subtotal = item.Subtotal});
                    acumuladorTotal += item.Subtotal;
                }
            }

            gvDetalleVenta.DataSource = datosParaGrilla;
            gvDetalleVenta.DataBind();

            lblTotal.Text = acumuladorTotal.ToString("F2");
        }

        private void LimpiarBuscadorProducto()
        {
            Session["ProductoSeleccionado"] = null;
            txtBuscarProducto.Text = "";
            txtCantidad.Text = "";
            lblProductoEncontrado.Text = "Ninguno";
            lblStockActual.Text = "-";
            lblPrecioVenta.Text = "-";
        }
    }

}
