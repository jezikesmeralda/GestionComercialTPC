using Dominio;
using Negocio;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Proveedores : PaginaBase
    {
        ProveedorNegocio negocio = new ProveedorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UsuarioActual.Rol != Rol.Administrador)
                {
                    btnNuevoProveedor.Visible = false;
                    gvProveedores.Columns[3].Visible = false;
                }

                CargarProveedores();
            }
        }

        private void CargarProveedores()
        {
            gvProveedores.DataSource = negocio.Listar();
            gvProveedores.DataBind();
        }

        protected void gvProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && UsuarioActual.Rol != Rol.Administrador)
            {
                Button btnEditar = (Button)e.Row.FindControl("btnEditar");
                Button btnEliminar = (Button)e.Row.FindControl("btnEliminar");

                if (btnEditar != null) btnEditar.Visible = false;
                if (btnEliminar != null) btnEliminar.Visible = false;
            }
        }

        protected void gvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null) return;

            int id;
            if (!int.TryParse(e.CommandArgument.ToString(), out id))
                return;

            if (e.CommandName == "Eliminar")
            {
                try
                {
                    negocio.Baja(id);
                    CargarProveedores();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
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
        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}