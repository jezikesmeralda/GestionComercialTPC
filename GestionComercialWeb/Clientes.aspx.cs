using Dominio;
using Negocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Clientes : PaginaBase
    {
        ClienteNegocio negocio = new ClienteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["mensaje"] != null)
                {
                    MostrarMensaje(Session["mensaje"].ToString(), esError: false);
                    Session.Remove("mensaje");
                }

               if (UsuarioActual.Rol != Rol.Administrador)
                {
                   // btnNuevoCliente.Visible = false;
                    gvClientes.Columns[6].Visible = false;
                }

              CargarClientes();
            }
        }

        private void CargarClientes()
        {
            gvClientes.DataSource = negocio.Listar();
            gvClientes.DataBind();
        }

        protected void gvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && UsuarioActual.Rol != Rol.Administrador)
            {
                Button btnEditar = (Button)e.Row.FindControl("btnEditar");
                Button btnEliminar = (Button)e.Row.FindControl("btnEliminar");

                if (btnEditar != null) btnEditar.Visible = false;
                if (btnEliminar != null) btnEliminar.Visible = false;
            }
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
         
            if (e.CommandArgument == null) return;

            int id;
            if (!int.TryParse(e.CommandArgument.ToString(), out id))
            {
                return;
            }

            if (e.CommandName == "Eliminar")
            {
                hfIdEliminar.Value = id.ToString();

                MostrarModal("modalEliminar");
                return;
            }

            if (e.CommandName == "Editar")
            {
                Response.Redirect("ClientesForm.aspx?id=" + id);
            }
        }

        protected void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientesForm.aspx");
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hfIdEliminar.Value);
                negocio.Baja(id);
                CargarClientes();
                MostrarMensaje("Cliente eliminado correctamente.", esError: false);
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
                btnVerInactivos.Text = "Ocultar Clientes Inactivos";
            }
            else
            {
                btnVerInactivos.Text = "Ver Clientes Inactivos";
            }
        }

        private void CargarInactivos()
        {
            gvInactivos.DataSource = negocio.ListarInactivos();
            gvInactivos.DataBind();
        }

        protected void gvInactivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null) return;

            int id;
            if (!int.TryParse(e.CommandArgument.ToString(), out id))
                return;

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
                btnVerInactivos.Text = "Ocultar Clientes Inactivos";
                CargarInactivos();
                CargarClientes();

                MostrarMensaje("Cliente reactivado correctamente.", esError: false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, esError: true);
            }
        }
        private void MostrarMensaje(string mensaje, bool esError)
        {
            pnlMensaje.CssClass = "alert alert-dismissible fade show mb-3 " + (esError ? "alert-danger" : "alert-success");
            litMensaje.Text = mensaje +" <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Cerrar\"></button>";
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