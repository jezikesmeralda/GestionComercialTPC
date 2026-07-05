using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Productos : PaginaBase
    {
        public List<Producto> ListaProductos { get; set; }
        public List<Producto> ListaProductosInactivos { get; set; } = new List<Producto>();
        ProductoNegocio negocio = new ProductoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarMarcas();

                if (Session["mensaje"] != null)
                {
                    MostrarMensaje(Session["mensaje"].ToString(), esError: false);
                    Session.Remove("mensaje");
                }

                if (Request.QueryString["eliminar"] != null)
                {
                    int idEliminar = int.Parse(Request.QueryString["eliminar"]);

                    try
                    {
                        negocio.Baja(idEliminar);
                        Session["mensaje"] = "Producto eliminado correctamente.";
                    }
                    catch (Exception ex)
                    {
                        Session["mensaje"] = ex.Message;
                    }
                    Response.Redirect("Productos.aspx");
                    return;
                }
                if (Request.QueryString["reactivar"] != null)
                {
                    int idReactivar = int.Parse(Request.QueryString["reactivar"]);

                    try
                    {
                        negocio.Reactivar(idReactivar);
                        Session["mensaje"] = "Producto reactivado correctamente.";
                    }
                    catch (Exception ex)
                    {
                        Session["mensaje"] = ex.Message;
                    }

                    Response.Redirect("Productos.aspx?verInactivos=1");
                    return;
                }

                if (Request.QueryString["verInactivos"] == "1")
                {
                    pnlInactivos.Visible = true;
                    btnVerInactivos.Text = "Ocultar Productos Inactivos";
                }
                ListaProductos = negocio.Listar();
            }
            else
            {
                string busqueda = txtBuscar.Text.Trim().ToLower();
                List<Producto> todos = negocio.Listar();
                List<Producto> filtrados = new List<Producto>();

                foreach (Producto p in todos)
                {
                    if (string.IsNullOrWhiteSpace(busqueda) || p.NombreProducto.ToLower().Contains(busqueda))
                        filtrados.Add(p);
                }

                ListaProductos = filtrados;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (pnlInactivos.Visible)
            {
                ListaProductosInactivos = negocio.ListarInactivos();
            }
        }
        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            ddlMarca.DataSource = negocio.Listar();
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();

            ddlMarca.Items.Insert(0, new ListItem("Todas", "0"));
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            ddlCategoria.DataSource = negocio.Listar();
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();

            ddlCategoria.Items.Insert(0, new ListItem("Todas", "0"));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscar.Text.Trim().ToLower();

            int idMarca = int.Parse(ddlMarca.SelectedValue);
            int idCategoria = int.Parse(ddlCategoria.SelectedValue);
            int stock = int.Parse(ddlStock.SelectedValue);

            List<Producto> todos = negocio.Listar();
            List<Producto> filtrados = new List<Producto>();

            foreach (Producto p in todos)
            {
                bool coincideNombre =
                    string.IsNullOrWhiteSpace(busqueda)
                    || p.NombreProducto.ToLower().Contains(busqueda);

                bool coincideMarca =
                    idMarca == 0 || p.Marca.Id == idMarca;

                bool coincideCategoria =
                    idCategoria == 0 || p.Categoria.Id == idCategoria;

                bool coincideStock = false;

                switch (stock)
                {
                    case 0:
                        coincideStock = true;
                        break;

                    case 1:
                        coincideStock = p.StockActual > 10;
                        break;

                    case 2:
                        coincideStock = p.StockActual > 0 && p.StockActual <= 10;
                        break;

                    case 3:
                        coincideStock = p.StockActual == 0;
                        break;
                }

                if (coincideNombre && coincideMarca && coincideCategoria && coincideStock)
                {
                    filtrados.Add(p);
                }
            }

            ListaProductos = filtrados;
        }
        protected void btnVerInactivos_Click(object sender, EventArgs e)
        {
            pnlInactivos.Visible = !pnlInactivos.Visible;

            btnVerInactivos.Text = pnlInactivos.Visible ? "Ocultar Productos Inactivos" : "Ver Productos Inactivos";
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            pnlMensaje.CssClass = "alert alert-dismissible fade show mb-3 " + (esError ? "alert-danger" : "alert-success");
            litMensaje.Text = mensaje + " <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Cerrar\"></button>";
            pnlMensaje.Visible = true;
        }
    }
}