using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace GestionComercialWeb
{

    public partial class Productos : System.Web.UI.Page
    {
        public List<Producto> ListaProductos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductoNegocio negocioProducto = new ProductoNegocio();
            negocioProducto.Listar();
            ListaProductos = negocioProducto.Listar();

           /*MarcaNegocio negocioMarca = new MarcaNegocio();
            dgvMarca.DataSource = negocioMarca.Listar();
            dgvMarca.DataBind();

            CategoriaNegocio negocioCategoria = new CategoriaNegocio();
            dgvCategoria.DataSource = negocioCategoria.Listar();
            dgvCategoria.DataBind();*/
        }
    }
}