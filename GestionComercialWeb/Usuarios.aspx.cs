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
                    MostrarMensaje(Session["mensaje"].ToString(), esError: false);
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
                hfIdEliminar.Value = id.ToString();
                MostrarModal("modalEliminar");
                return;
            }

            if (e.CommandName == "Editar")
                Response.Redirect("UsuariosForm.aspx?id=" + id);
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsuariosForm.aspx");
        }
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hfIdEliminar.Value);
                negocio.Baja(id);
                CargarUsuarios();
                MostrarMensaje("Usuario eliminado correctamente.", esError: false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, esError: true);
            }
        }

        protected void btnVerInactivos_Click(object sender, EventArgs e)
        {
            pnlInactivos.Visible = !pnlInactivos.Visible;

            if (pnlInactivos.Visible)
            {
                CargarInactivos();
                btnVerInactivos.Text = "Ocultar Usuarios Inactivos";
            }
            else
            {
                btnVerInactivos.Text = "Ver Usuarios Inactivos";
            }
        }

        private void CargarInactivos()
        {
            var lista = negocio.ListarInactivos();
            gvInactivos.DataSource = lista.Select(u => new
            {
                u.Id,
                u.UserName,
                Email = u.Email ?? "-",
                RolNombre = u.Rol == Rol.Administrador ? "Administrador" : "Vendedor"
            }).ToList();
            gvInactivos.DataBind();
        }

        protected void gvInactivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null) return;
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Reactivar")
            {
                hfIdReactivar.Value = id.ToString();
                MostrarModal("modalReactivar");
            }
        }

        protected void btnConfirmarReactivar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hfIdReactivar.Value);
                negocio.Reactivar(id);

                pnlInactivos.Visible = true;
                btnVerInactivos.Text = "Ocultar Usuarios Inactivos";
                CargarInactivos();
                CargarUsuarios();

                MostrarMensaje("Usuario reactivado correctamente.", esError: false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, esError: true);
            }
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            pnlMensaje.CssClass = "alert alert-dismissible fade show mb-3 " + (esError ? "alert-danger" : "alert-success");
            litMensaje.Text = mensaje + " <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Cerrar\"></button>";
            pnlMensaje.Visible = true;
        }

        private void MostrarModal(string idModal)
        {
            string script = $@"
                (function() {{
                    function abrirModal() {{
                        if (typeof bootstrap !== 'undefined') {{
                            new bootstrap.Modal(document.getElementById('{idModal}')).show();
                        }} else {{
                            setTimeout(abrirModal, 50);
                        }}
                    }}
                    abrirModal();
                }})();
            ";
            ClientScript.RegisterStartupScript(GetType(), "mostrarModal_" + idModal, script, true);
        }
    }
}