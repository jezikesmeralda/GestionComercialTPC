using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Compras : PaginaBase
    {
        private Producto ProductoSeleccionado
        {
            get { return (Producto)Session["ProductoSeleccionado"]; }
            set { Session["ProductoSeleccionado"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["DetalleCompra"] = new List<DetalleCompra>();
                CargarProveedores();
            }
        }

        private void CargarProveedores()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();

            ddlProveedor.DataSource = negocio.Listar();
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "Id";
            ddlProveedor.DataBind();

            ddlProveedor.Items.Insert(0, new ListItem("Todas", "0"));
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscarProducto.Text.Trim();

            if (string.IsNullOrEmpty(busqueda))
            {
                MostrarError("Ingrese un nombre de producto para buscar.");
                return;
            }

            ProductoNegocio negocio = new ProductoNegocio();

            Producto producto = negocio.BusquedaNombre(busqueda);

            if (producto != null)
            {
                ProductoSeleccionado = producto;

                lblProductoEncontrado.Text = producto.NombreProducto;
                lblStockActual.Text = producto.StockActual.ToString();
                lblUltimoCosto.Text = producto.PrecioCosto.ToString("N2");
            }
            else
            {
                ProductoSeleccionado = null;
                lblProductoEncontrado.Text = "Producto no encontrado";
                lblStockActual.Text = "-";
                lblUltimoCosto.Text = "-";
                MostrarError("No se encontró ningún producto con ese nombre.");
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ProductoSeleccionado == null)
            {
                MostrarError("Debe buscar y seleccionar un producto antes de agregarlo.");
                return;
            }
            int cantidad;
            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
            {
                MostrarError("Ingrese una cantidad válida (mayor a 0).");
                return;
            }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio) || precio <= 0)
            {
                MostrarError("Ingrese un precio de compra válido (mayor a 0).");
                return;
            }

            List<DetalleCompra> lista = (List<DetalleCompra>)Session["DetalleCompra"];
            if (lista == null)
            {
                lista = new List<DetalleCompra>();
            }
            DetalleCompra detalle = new DetalleCompra();

            detalle.Producto = ProductoSeleccionado;
            detalle.Cantidad = cantidad;
            detalle.PrecioUnitario = precio;
            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

            lista.Add(detalle);
            Session["DetalleCompra"] = lista;

            OcultarError();
            CargarGrilla();
        }
    

    private void CargarGrilla()
        {
            List<DetalleCompra> lista = (List<DetalleCompra>)Session["DetalleCompra"];

            gvDetalle.DataSource = lista.Select(x => new
                {
                    Producto = x.Producto.NombreProducto, x.Cantidad, x.PrecioUnitario, x.Subtotal
                });

            gvDetalle.DataBind();

            lblTotal.Text = lista.Sum(x => x.Subtotal).ToString("N2");
        }
        protected void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            if (ddlProveedor.SelectedValue == "0")
            {
                MostrarError("Debe seleccionar un proveedor.");
                return;
            }
            List<DetalleCompra> detalles = (List<DetalleCompra>)Session["DetalleCompra"];

            if (detalles == null || detalles.Count == 0)
            {
                MostrarError("Debe agregar al menos un producto a la compra.");
                return;
            }

            try
            {

                Compra compra = new Compra();

            compra.Proveedor = new Dominio.Proveedor();
            compra.Proveedor.Id = int.Parse(ddlProveedor.SelectedValue);

            compra.FechaCompra = DateTime.Now;

            compra.Detalles = detalles;

            compra.Total = detalles.Sum(x => x.Subtotal);

            ComprasNegocio negocio = new ComprasNegocio();

            negocio.Alta(compra);

            Session["DetalleCompra"] = new List<DetalleCompra>();

            Response.Redirect("Compras.aspx");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }
        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }

        private void OcultarError()
        {
            lblError.Visible = false;
        }
    }

}

