using Dominio;
using Negocio;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Usuarios : PaginaBase
    {
        UsuarioNegocio negocio = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsuarioActual.Rol != Rol.Administrador)
                Response.Redirect("Inicio.aspx");

            if (!IsPostBack)
            {
                if (Session["mensaje"] != null)
                {
                    lblMensaje.Text = Session["mensaje"].ToString();
                    lblMensaje.CssClass = "alert alert-success d-block mb-3";
                    lblMensaje.Visible = true;
                    Session.Remove("mensaje");
                }

                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            var lista = negocio.Listar();
            gvUsuarios.DataSource = lista.Select(u => new
            {
                u.Id,
                u.UserName,
                Email = u.Email ?? "-",
                RolNombre = u.Rol == Rol.Administrador ? "Administrador" : "Vendedor"
            }).ToList();
            gvUsuarios.DataBind();
        }

        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Evitamos que el admin se elimine a si mismo
                Button btnEliminar = (Button)e.Row.FindControl("btnEliminar");
                if (btnEliminar != null)
                {
                    int id = (int)gvUsuarios.DataKeys[e.Row.RowIndex].Value;
                    if (id == UsuarioActual.Id)
                        btnEliminar.Visible = false;
                }
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null) return;
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                negocio.Baja(id);
                CargarUsuarios();
            }

            if (e.CommandName == "Editar")
                Response.Redirect("UsuariosForm.aspx?id=" + id);
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsuariosForm.aspx");
        }
    }
}