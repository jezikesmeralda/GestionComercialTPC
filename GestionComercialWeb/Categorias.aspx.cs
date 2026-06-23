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
    public partial class Categorias : System.Web.UI.Page
    {
        CategoriaNegocio negocio = new CategoriaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();

            }
        }
        private void cargarGrilla()
        {
            gvCategorias.DataSource = negocio.Listar();
            gvCategorias.DataBind();
        }


        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                MarcaNegocio negocio = new MarcaNegocio();

                negocio.Baja(id);

                cargarGrilla();
            }

            if (e.CommandName == "Editar")
            {
                Response.Redirect("CategoriasForm.aspx?id=" + id);
            }
        }
        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoriasForm.aspx");
        }
    }
}
