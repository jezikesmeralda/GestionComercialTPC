using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Compras : System.Web.UI.Page
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

                ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                ddlProveedor.DataSource = proveedorNegocio.Listar();
                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataValueField = "Id";
                ddlProveedor.DataBind();
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();

            Producto producto = negocio.BusquedaNombre(txtBuscarProducto.Text);

            if (producto != null)
            {
                ProductoSeleccionado = producto;

                lblProductoEncontrado.Text = producto.NombreProducto;
                lblStockActual.Text = producto.StockActual.ToString();
                lblUltimoCosto.Text = producto.PrecioCosto.ToString("N2");
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ProductoSeleccionado == null)
                return;

            List<DetalleCompra> lista =
                (List<DetalleCompra>)Session["DetalleCompra"];

            DetalleCompra detalle = new DetalleCompra();

            detalle.Producto = ProductoSeleccionado;
            detalle.Cantidad = int.Parse(txtCantidad.Text);
            detalle.PrecioUnitario = decimal.Parse(txtPrecio.Text);

            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

            lista.Add(detalle);

            Session["DetalleCompra"] = lista;

            CargarGrilla();
        }
    

    private void CargarGrilla()
        {
            List<DetalleCompra> lista =
                (List<DetalleCompra>)Session["DetalleCompra"];

            gvDetalle.DataSource =
                lista.Select(x => new
                {
                    Producto = x.Producto.NombreProducto, x.Cantidad, x.PrecioUnitario, x.Subtotal
                });

            gvDetalle.DataBind();

            lblTotal.Text =
                lista.Sum(x => x.Subtotal).ToString("N2");
        }
        protected void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            List<DetalleCompra> detalles = (List<DetalleCompra>)Session["DetalleCompra"];

            if (detalles.Count == 0)
                return;

            Compra compra = new Compra();

            compra.Proveedor = new Proveedor();
            compra.Proveedor.Id = int.Parse(ddlProveedor.SelectedValue);

            compra.FechaCompra = DateTime.Now;

            compra.Detalles = detalles;

            compra.Total = detalles.Sum(x => x.Subtotal);

            ComprasNegocio negocio = new ComprasNegocio();

            negocio.Alta(compra);

            Session["DetalleCompra"] = new List<DetalleCompra>();

            Response.Redirect("Compras.aspx");
        }
    }

}

