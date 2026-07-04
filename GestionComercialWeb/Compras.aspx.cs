using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Compras : PaginaBase
    {
        private Dominio.Producto ProductoSeleccionado
        {
            get { return (Dominio.Producto)Session["ProductoSeleccionado"]; }
            set { Session["ProductoSeleccionado"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UsuarioActual.Rol == Rol.Administrador)
                    btnHistorial.Visible = true;

                Session["DetalleCompra"] = new List<Dominio.DetalleCompra>();
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
            try
            {
                string busqueda = txtBuscarProducto.Text.Trim();

                if (string.IsNullOrEmpty(busqueda))
                {
                    MostrarError(lblErrorBusqueda, "Ingrese un nombre de producto para buscar.");
                    return;
                }

                ProductoNegocio negocio = new ProductoNegocio();
                List<Dominio.Producto> productos = negocio.BusquedaNombre(busqueda);

                Session["ProductosBuscados"] = productos;

                gvProductos.DataSource = productos;
                gvProductos.DataBind();

                if (productos.Count == 0)
                    MostrarError(lblErrorBusqueda, "No se encontraron productos.");
                else
                    OcultarError(lblErrorBusqueda);
            }
            catch (Exception ex)
            {
                MostrarError(lblErrorBusqueda, ex.Message);
            }
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int fila;
                if (!int.TryParse(e.CommandArgument.ToString(), out fila))
                    return;

                List<Dominio.Producto> productos = (List<Dominio.Producto>)Session["ProductosBuscados"];
                if (productos == null || fila < 0 || fila >= productos.Count)
                    return;

                ProductoSeleccionado = productos[fila];
                txtPrecio.Text = ProductoSeleccionado.PrecioCosto.ToString("N2");
                OcultarError(lblErrorBusqueda);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ProductoSeleccionado == null)
            {
                MostrarError(lblErrorBusqueda, "Debe buscar y seleccionar un producto antes de agregarlo.");
                return;
            }

            int cantidad;
            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
            {
                MostrarError(lblErrorCantidad, "Ingrese una cantidad válida (mayor a 0).");
                return;
            }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio) || precio <= 0)
            {
                MostrarError(lblErrorPrecioCompra, "Ingrese un precio de compra válido (mayor a 0).");
                return;
            }

            List<Dominio.DetalleCompra> lista = (List<Dominio.DetalleCompra>)Session["DetalleCompra"];
            if (lista == null)
                lista = new List<Dominio.DetalleCompra>();

            Dominio.DetalleCompra detalle = new Dominio.DetalleCompra();
            detalle.Producto = ProductoSeleccionado;
            detalle.Cantidad = cantidad;
            detalle.PrecioUnitario = precio;
            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

            lista.Add(detalle);
            Session["DetalleCompra"] = lista;

            OcultarError(lblErrorAgregar);
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            List<Dominio.DetalleCompra> lista = (List<Dominio.DetalleCompra>)Session["DetalleCompra"];

            gvDetalle.DataSource = lista.Select(x => new
            {
                Producto = x.Producto.NombreProducto,
                x.Cantidad,
                x.PrecioUnitario,
                x.Subtotal
            });

            gvDetalle.DataBind();

            lblTotal.Text = lista.Sum(x => x.Subtotal).ToString("N2");
        }

        protected void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            if (ddlProveedor.SelectedValue == "0")
            {
                MostrarError(lblErrorProveedor, "Debe seleccionar un proveedor.");
                return;
            }

            List<Dominio.DetalleCompra> detalles = (List<Dominio.DetalleCompra>)Session["DetalleCompra"];

            if (detalles == null || detalles.Count == 0)
            {
                MostrarError(lblErrorAgregar, "Debe agregar al menos un producto a la compra.");
                return;
            }

            try
            {
                Dominio.Compra compra = new Dominio.Compra();
                compra.Proveedor = new Dominio.Proveedor();
                compra.Proveedor.Id = int.Parse(ddlProveedor.SelectedValue);
                compra.FechaCompra = DateTime.Now;
                compra.Detalles = detalles;
                compra.Total = detalles.Sum(x => x.Subtotal);

                new ComprasNegocio().Alta(compra);

                Session["DetalleCompra"] = new List<Dominio.DetalleCompra>();
                Response.Redirect("Compras.aspx");
            }
            catch (Exception ex)
            {
                MostrarError(lblErrorAgregar, ex.Message);
            }
        }

        private void MostrarError(Label label, string mensaje)
        {
            label.Text = mensaje;
            label.Visible = true;
        }

        private void OcultarError(Label label)
        {
            label.Text = "";
            label.Visible = false;
        }
    }
}