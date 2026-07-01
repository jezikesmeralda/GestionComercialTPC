using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Ventas : PaginaBase
    {
        private ProductoNegocio prodNegocio = new ProductoNegocio();
        private VentaNegocio ventaNegocio = new VentaNegocio();
        private ClienteNegocio clienteNegocio = new ClienteNegocio();

        private Producto ProductoSeleccionado
        {
            get { return (Producto)Session["ProductoSeleccionado"]; }
            set { Session["ProductoSeleccionado"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();

                if (UsuarioActual.Rol == Rol.Administrador)
                    btnHistorial.Visible = true;
            }

            if (Session["Carrito"] == null)
                Session["Carrito"] = new List<DetalleVenta>();

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
            if (string.IsNullOrEmpty(busqueda))
            {
                MostrarError(lblErrorProducto,"Ingrese un nombre de producto para buscar.");
                return;
            }

            List<Producto> productos = prodNegocio.BusquedaNombre(busqueda);

            Session["ProductosBuscados"] = productos;

            gvProductos.DataSource = productos;
            gvProductos.DataBind();

            if (productos.Count == 0)
                MostrarError(lblErrorProducto,"No se encontraron productos.");
            else
                OcultarError(lblErrorProducto);
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int fila = Convert.ToInt32(e.CommandArgument);
                List<Producto> productos = (List<Producto>)Session["ProductosBuscados"];
                ProductoSeleccionado = productos[fila];
                OcultarError(lblErrorProducto);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            bool errores = false;
            if (Session["ProductoSeleccionado"] == null)
            {
                MostrarError(lblErrorProducto, "Debe buscar y seleccionar un producto antes de agregarlo.");
                errores = true;
            }

            int cantidad;
            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
            {
                MostrarError(lblErrorCantidad, "Ingrese una cantidad válida (mayor a 0).");
                errores = true;
            }

            if (ddlCliente.SelectedValue == "0")
            {
                MostrarError(lblErrorCliente, "Debe seleccionar un cliente.");
                errores = true;
            }
            if (errores)
                return;
            Producto prod = ProductoSeleccionado;

            if (cantidad > prod.StockActual)
            {
                MostrarError(lblErrorAgregar, "Stock insuficiente. Stock disponible: " + prod.StockActual + ".");
                return;
            }

            List<DetalleVenta> listaTemporal = (List<DetalleVenta>)Session["Carrito"];
            if (listaTemporal == null)
                listaTemporal = new List<DetalleVenta>();

            DetalleVenta nuevoDetalle = new DetalleVenta();
            nuevoDetalle.Producto = prod;
            nuevoDetalle.Cantidad = cantidad;
            nuevoDetalle.PrecioUnitario = prod.PrecioVenta;

            listaTemporal.Add(nuevoDetalle);

            Session["Carrito"] = listaTemporal;
            OcultarError(lblErrorProducto);
            LimpiarBuscadorProducto();
            ActualizarGrillaYTotal();
        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            List<DetalleVenta> listaTemporal = (List<DetalleVenta>)Session["Carrito"];
            bool errores = false;

            if (ddlCliente.SelectedValue == "0")
            {
                MostrarError(lblErrorCliente, "Debe seleccionar un cliente.");
                errores = true;
            }

            if (listaTemporal == null || listaTemporal.Count == 0)
            {
                MostrarError(lblErrorProducto, "Debe agregar al menos un producto.");
                errores = true; ;
            }

            if (errores)
                return;

            try
            {
                Venta nuevaVenta = new Venta();
                nuevaVenta.Cliente = new Cliente { Id = int.Parse(ddlCliente.SelectedValue) };
                nuevaVenta.Vendedor = new Usuario { Id = UsuarioActual.Id };
                nuevaVenta.Detalles = listaTemporal;

                decimal totalFinal = 0;
                foreach (var item in listaTemporal) totalFinal += item.Subtotal;
                nuevaVenta.Total = totalFinal;

                Venta ventaGuardada = ventaNegocio.Alta(nuevaVenta);

                Session["Carrito"] = null;

                Response.Redirect("Factura.aspx?id=" + ventaGuardada.Id);
            }
            catch (Exception ex)
            {
                MostrarError(lblErrorProducto, ex.Message);
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
                    datosParaGrilla.Add(new
                    {
                        Producto = item.Producto.NombreProducto,
                        Cantidad = item.Cantidad,
                        Precio = item.PrecioUnitario,
                        Subtotal = item.Subtotal
                    });
                    acumuladorTotal += item.Subtotal;
                }
            }

            gvDetalleVenta.DataSource = datosParaGrilla;
            gvDetalleVenta.DataBind();

            lblTotal.Text = acumuladorTotal.ToString("F2");
        }

        private void LimpiarBuscadorProducto()
        {
            ProductoSeleccionado = null;
            Session["ProductosBuscados"] = null;
            txtBuscarProducto.Text = "";
            txtCantidad.Text = "";
            gvProductos.DataSource = null;
            gvProductos.DataBind();
        }

        private void MostrarError(System.Web.UI.WebControls.Label label, string mensaje)
        {
            label.Text = mensaje;
            label.Visible = true;
        }

        private void OcultarError(System.Web.UI.WebControls.Label label)
        {
            label.Text = "";
            label.Visible = false;
        }
    }
}