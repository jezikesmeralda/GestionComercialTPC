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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarMarcas();

                if (Request.QueryString["eliminar"] != null)
                {
                    int idEliminar = int.Parse(Request.QueryString["eliminar"]);
                    new ProductoNegocio().Baja(idEliminar);
                    Response.Redirect("Productos.aspx");
                    
                    return;
                }

                ListaProductos = new ProductoNegocio().Listar();
            }
            else
            {
                string busqueda = txtBuscar.Text.Trim().ToLower();
                List<Producto> todos = new ProductoNegocio().Listar();
                List<Producto> filtrados = new List<Producto>();

                foreach (Producto p in todos)
                {
                    if (string.IsNullOrWhiteSpace(busqueda) || p.NombreProducto.ToLower().Contains(busqueda))
                        filtrados.Add(p);
                }

                ListaProductos = filtrados;
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

            List<Producto> todos = new ProductoNegocio().Listar();
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
    }
}