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
                    lblMensaje.Text = Session["mensaje"].ToString();
                    lblMensaje.CssClass = "alert alert-success d-block mb-3";
                    lblMensaje.Visible = true;
                    Session.Remove("mensaje");
                }

                if (UsuarioActual.Rol != Rol.Administrador)
                {
                    btnNuevoCliente.Visible = false;
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
                try
                {
                    negocio.Baja(id);
                CargarClientes();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = ex.Message;
                    lblMensaje.CssClass = "alert alert-danger d-block mb-3";
                    lblMensaje.Visible = true;
                }
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
    }
}