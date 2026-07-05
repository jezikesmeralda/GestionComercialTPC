using Dominio;
using Negocio;
using GestionComercialWeb.Services;
using System;

namespace GestionComercialWeb
{
    public partial class UsuariosForm : PaginaBase
    {
        UsuarioNegocio negocio = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsuarioActual.Rol != Rol.Administrador)
                Response.Redirect("Inicio.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    Usuario usuario = negocio.Listar().Find(u => u.Id == id);

                    if (usuario != null)
                    {
                        hdnId.Value = usuario.Id.ToString();
                        lblTitulo.Text = "Editar Usuario";
                        txtUserName.Text = usuario.UserName;
                        txtEmail.Text = usuario.Email;
                        ddlRol.SelectedValue = ((int)usuario.Rol).ToString();
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                lblError.Text = "El nombre de usuario es obligatorio.";
                lblError.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblError.Text = "El email es obligatorio.";
                lblError.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text) && hdnId.Value == "0")
            {
                lblError.Text = "La contraseña es obligatoria.";
                lblError.Visible = true;
                return;
            }

            try
            {
                Usuario usuario = new Usuario();
                usuario.UserName = txtUserName.Text.Trim();
                usuario.Email = txtEmail.Text.Trim();
                usuario.Rol = (Rol)int.Parse(ddlRol.SelectedValue);
                usuario.Activo = true;

                if (hdnId.Value == "0")
                {
                    usuario.Password = txtPassword.Text;
                    negocio.Alta(usuario);

                    EmailServices.EnviarCredenciales(usuario.Email, usuario.UserName, usuario.Password);

                    Session["mensaje"] = "Usuario creado y credenciales enviadas a " + usuario.Email + ".";
                }
                else
                {
                    usuario.Id = int.Parse(hdnId.Value);
                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                        usuario.Password = txtPassword.Text;
                    else
                        usuario.Password = negocio.Listar().Find(u => u.Id == usuario.Id).Password;

                    negocio.Modificar(usuario);
                    Session["mensaje"] = "Usuario modificado correctamente.";
                }

                Response.Redirect("Usuarios.aspx");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY") || ex.Message.Contains("duplicate key"))
                    lblError.Text = "Ya existe un usuario con ese nombre.";
                else
                    lblError.Text = ex.Message;

                lblError.Visible = true;
            }
        }
    }
}