using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace GestionComercialWeb
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            dgvMarca.DataSource = negocio.Listar();
            dgvMarca.DataBind();

            CategoriaNegocio negocioCategoria = new CategoriaNegocio();
            dgvCategoria.DataSource = negocioCategoria.Listar();
            dgvCategoria.DataBind();
        }
    }
}