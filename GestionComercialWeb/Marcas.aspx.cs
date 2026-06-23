using Dominio;
using Negocio;
using System;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Marcas : System.Web.UI.Page
    {
        MarcaNegocio negocio = new MarcaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();

            
            }
        }
    
        private void cargarGrilla()
        {
            gvMarcas.DataSource = negocio.Listar();
            gvMarcas.DataBind();
        }


        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Response.Redirect("MarcasForm.aspx?id=" + id);
            }
        }
        protected void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("MarcasForm.aspx");
        }
    }
}
