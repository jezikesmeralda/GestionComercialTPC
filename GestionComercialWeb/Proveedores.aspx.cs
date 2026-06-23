using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Proveedores : System.Web.UI.Page
    {
        ProveedorNegocio negocio = new ProveedorNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();

            }
        }
        private void CargarProveedores()
        {

            gvProveedores.DataSource = negocio.Listar();
            gvProveedores.DataBind();
        }

        protected void gvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null) return;

            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                negocio.Baja(id); // O .Eliminar(id) según tu negocio
                CargarProveedores();
            }

            if (e.CommandName == "Editar")
            {
                Response.Redirect("ProveedoresForm.aspx?id=" + id);
            }
        }
        protected void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProveedoresForm.aspx");
        }
    }
}

