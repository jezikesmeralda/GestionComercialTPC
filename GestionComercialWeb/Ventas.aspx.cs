using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
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
                pnlCuotas.Visible = false;
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

            pnlResultados.Visible = productos.Count > 0;
            gvProductos.DataSource = productos;
            gvProductos.DataBind();
            pnlProductoSeleccionado.Visible = false;
            ProductoSeleccionado = null;


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
                lblProductoSeleccionado.Text = ProductoSeleccionado.NombreProducto;
                lblStockSeleccionado.Text = ProductoSeleccionado.StockActual.ToString();
                lblPrecioSeleccionado.Text = ProductoSeleccionado.PrecioVenta.ToString("C2");
                pnlProductoSeleccionado.Visible = true;

                
                pnlResultados.Visible = false;

                OcultarError(lblErrorProducto);
            }
        }
        protected void btnCancelarSeleccion_Click(object sender, EventArgs e)
        {
          
            ProductoSeleccionado = null;
            pnlProductoSeleccionado.Visible = false;

            List<Producto> productos = (List<Producto>)Session["ProductosBuscados"];
            if (productos != null && productos.Count > 0)
            {
                pnlResultados.Visible = true;
                gvProductos.DataSource = productos;
                gvProductos.DataBind();
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

            List<DetalleVenta> listaTemporal = (List<DetalleVenta>)Session["Carrito"] ?? new List<DetalleVenta>();
            listaTemporal.Add(new DetalleVenta
            {
                Producto = prod,
                Cantidad = cantidad,
                PrecioUnitario = prod.PrecioVenta
            });

            Session["Carrito"] = listaTemporal;
            OcultarError(lblErrorProducto);
            OcultarError(lblErrorCantidad);
            OcultarError(lblErrorAgregar);
            LimpiarBuscadorProducto();
            ActualizarGrillaYTotal();
            CalcularInteres();
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
                Venta nuevaVenta = new Venta
                {
                    Cliente = new Cliente { Id = int.Parse(ddlCliente.SelectedValue) },
                    Vendedor = new Usuario { Id = UsuarioActual.Id },
                    Detalles = listaTemporal,
                    Total = listaTemporal.Sum(x => x.Subtotal),
                    MedioPago = ddlMedioPago.SelectedValue,
                    Cuotas = ddlMedioPago.SelectedValue == "Credito"
                ? int.Parse(ddlCuotas.SelectedValue)
                : 1
                };
                nuevaVenta.Interes = ObtenerPorcentaje(nuevaVenta.Cuotas);
                nuevaVenta.TotalConInteres = nuevaVenta.Total + (nuevaVenta.Total * nuevaVenta.Interes / 100);

                Venta ventaGuardada = ventaNegocio.Alta(nuevaVenta);
                Session["Carrito"] = null;
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
    "window.location='" + ResolveUrl("~/Factura.aspx?id=" + ventaGuardada.Id) + "';", true);
            }
            catch (Exception ex)
            {
                MostrarError(lblError, ex.Message);
            }
        }
        protected void ddlMedioPago_Changed(object sender, EventArgs e)
        {
            pnlCuotas.Visible = ddlMedioPago.SelectedValue == "Credito";
            pnlResumenCuotas.Visible = ddlMedioPago.SelectedValue == "Credito";
            if (ddlMedioPago.SelectedValue == "Credito")
                CalcularInteres();
        }

        protected void ddlCuotas_Changed(object sender, EventArgs e)
        {
            lblDebug.Text = "Session TotalCarrito: " + Session["TotalCarrito"] + " | Carrito count: " + ((List<DetalleVenta>)Session["Carrito"]).Count;
            CalcularInteres();

        }

        private void CalcularInteres()
        {
            if (ddlMedioPago.SelectedValue != "Credito") return;

            decimal total = Session["TotalCarrito"] != null ? (decimal)Session["TotalCarrito"] : 0;

            int cuotas = int.Parse(ddlCuotas.SelectedValue);
            decimal porcentaje = ObtenerPorcentaje(cuotas);
            decimal montoInteres = total * porcentaje / 100m;
            decimal totalConInteres = total + montoInteres;
            decimal cuotaMensual = cuotas > 0 ? totalConInteres / cuotas : 0;

            lblInteres.Text = porcentaje.ToString("N0");
            lblMontoInteres.Text = montoInteres.ToString("N2");
            lblTotalConInteres.Text = totalConInteres.ToString("N2");
            lblCuotaMensual.Text = cuotaMensual.ToString("N2");
            pnlResumenCuotas.Visible = true;

            
        }
        private decimal ObtenerPorcentaje(int cuotas)
        {
            if (cuotas == 3) return 10;
            if (cuotas == 6) return 20;
            if (cuotas == 12) return 40;
            return 0;
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
            Session["TotalCarrito"] = acumuladorTotal; // añado esto
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