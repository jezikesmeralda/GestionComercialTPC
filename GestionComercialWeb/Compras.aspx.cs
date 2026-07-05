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
                if (UsuarioActual.Rol == Rol.Administrador)
                    btnHistorial.Visible = true;

                List<Dominio.DetalleCompra> lista = (List<Dominio.DetalleCompra>)Session["DetalleCompra"];
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

            ddlProveedor.Items.Insert(0, new ListItem("Seleccione un proveedor...", "0"));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
                string busqueda = txtBuscarProducto.Text.Trim();

                if (string.IsNullOrEmpty(busqueda))
                {
                    MostrarError(lblErrorBusqueda, "Ingrese un nombre de producto para buscar.");
                    return;
                }

                List<Producto> productos = new ProductoNegocio().BusquedaNombre(busqueda);
                Session["ProductosBuscados"] = productos;

                pnlResultados.Visible = productos.Count > 0;
                gvProductos.DataSource = productos;
                gvProductos.DataBind();

                pnlProductoSeleccionado.Visible = false;
                ProductoSeleccionado = null;

                if (productos.Count == 0)
                    MostrarError(lblErrorBusqueda, "No se encontraron productos con ese nombre.");
                else
                    OcultarError(lblErrorBusqueda);
           
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Seleccionar") return;
            
                int fila;
                if (!int.TryParse(e.CommandArgument.ToString(), out fila))
                    return;

                List<Producto> productos = (List<Producto>)Session["ProductosBuscados"];
                if (productos == null || fila < 0 || fila >= productos.Count)
                    return;
            ProductoSeleccionado = productos[fila];

            lblProductoSeleccionado.Text = ProductoSeleccionado.NombreProducto;
            lblStockSeleccionado.Text = ProductoSeleccionado.StockActual.ToString();
            pnlProductoSeleccionado.Visible = true;

            txtPrecio.Text = ProductoSeleccionado.PrecioCosto.ToString("N2");
            pnlResultados.Visible = false;
            OcultarError(lblErrorBusqueda);
            
        }
        protected void btnCancelarSeleccion_Click(object sender, EventArgs e)
        {
            ProductoSeleccionado = null;
            pnlProductoSeleccionado.Visible = false;
            txtPrecio.Text = "";

            
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

            List<Dominio.DetalleCompra> lista = (List<Dominio.DetalleCompra>)Session["DetalleCompra"] ?? new List<Dominio.DetalleCompra>();
           
            lista.Add(new Dominio.DetalleCompra
            {
                Producto = ProductoSeleccionado,
                Cantidad = cantidad,
                PrecioUnitario = precio,
                Subtotal = cantidad * precio
            });

            Session["DetalleCompra"] = lista;

            OcultarError(lblErrorAgregar);
            OcultarError(lblErrorCantidad);
            OcultarError(lblErrorPrecioCompra);

            ProductoSeleccionado = null;
            Session["ProductosBuscados"] = null;
            txtBuscarProducto.Text = "";
            txtCantidad.Text = "";
            txtPrecio.Text = "";
            pnlResultados.Visible = false;
            pnlProductoSeleccionado.Visible = false;
            gvProductos.DataSource = null;
            gvProductos.DataBind();
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
                decimal total = detalles.Sum(x => x.Subtotal);
                string nombreProveedor = ddlProveedor.SelectedItem.Text;

                Compra compra = new Compra
                {
                    Proveedor = new Proveedor { Id = int.Parse(ddlProveedor.SelectedValue) },
                    FechaCompra = DateTime.Now,
                    Detalles = detalles,
                    Total = total
                };

                new ComprasNegocio().Alta(compra);

                Session["DetalleCompra"] = new List<Dominio.DetalleCompra>();
                Session["ProductosBuscados"] = null;
                ProductoSeleccionado = null;

                txtBuscarProducto.Text = "";
                txtCantidad.Text = "";
                txtPrecio.Text = "";
                pnlResultados.Visible = false;
                pnlProductoSeleccionado.Visible = false;
                gvProductos.DataSource = null;
                gvProductos.DataBind();
                gvDetalle.DataSource = null;
                gvDetalle.DataBind();
                lblTotal.Text = "0";
                ddlProveedor.SelectedIndex = 0;

                lblExitoProveedor.Text = nombreProveedor;
                lblExitoTotal.Text = total.ToString("C2");
                pnlExito.Visible = true;

                OcultarError(lblError);
                OcultarError(lblErrorProveedor);
            }
            catch (Exception ex)
            {
                MostrarError(lblError, "Error al registrar la compra: " + ex.Message);
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