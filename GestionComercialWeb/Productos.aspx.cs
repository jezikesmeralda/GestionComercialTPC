using System;
using System.Collections.Generic;
using System.Web.UI;
using Negocio;
using Dominio;

namespace GestionComercialWeb
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Producto> ListaProductos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ListaProductos = new ProductoNegocio().Listar();

            dgvMarca.DataSource = new MarcaNegocio().Listar();
            dgvMarca.DataBind();

            dgvCategoria.DataSource = new CategoriaNegocio().Listar();
            dgvCategoria.DataBind();
        }
    }
}