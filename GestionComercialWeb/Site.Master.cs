using Dominio;
using System;

namespace GestionComercialWeb
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                lblUsuario.Text = usuario.UserName;

                if (usuario.Rol != Rol.Administrador)
                {
                    lnkReportes.Visible = false;
                    lnkUsuarios.Visible = false;
                    lnkCompras.Visible = false;
                    lnkProveedores.Visible = false;
                    lnkMarcas.Visible = false;
                    lnkCategorias.Visible = false;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}