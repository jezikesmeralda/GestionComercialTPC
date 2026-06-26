using Dominio;
using Negocio;
using System;

namespace GestionComercialWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
                Response.Redirect("Inicio.aspx");

            if (Session["mensaje"] != null)
            {
                lblError.Text = Session["mensaje"].ToString();
                lblError.CssClass = "text-success d-block mb-3";
                lblError.Visible = true;
                Session.Remove("mensaje");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                Usuario usuario = new UsuarioNegocio().ValidarLogin(userName, password);

                if (usuario != null)
                {
                    Session["usuario"] = usuario;
                    Response.Redirect("Inicio.aspx");
                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }
    }
}