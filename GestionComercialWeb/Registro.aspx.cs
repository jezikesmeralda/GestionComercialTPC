using Dominio;
using Negocio;
using System;

namespace GestionComercialWeb
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
                Response.Redirect("Inicio.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string userName = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmar = txtConfirmarPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                lblError.Text = "El nombre de usuario es obligatorio.";
                lblError.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "La contraseña es obligatoria.";
                lblError.Visible = true;
                return;
            }

            if (password != confirmar)
            {
                lblError.Text = "Las contraseñas no coinciden.";
                lblError.Visible = true;
                return;
            }

            try
            {
                Usuario usuario = new Usuario();
                usuario.UserName = userName;
                usuario.Password = password;
                usuario.Rol = Rol.Vendedor;
                usuario.Activo = true;

                new UsuarioNegocio().Alta(usuario);

                Session["mensaje"] = "Cuenta creada exitosamente. Por favor, inicie sesión.";
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY") || ex.Message.Contains("duplicate key"))
                    lblError.Text = "Ya existe un usuario con ese nombre. Por favor, elegí otro.";
                else
                    lblError.Text = ex.Message;

                lblError.Visible = true;
            }
        }
    }
}