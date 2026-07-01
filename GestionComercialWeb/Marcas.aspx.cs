using Dominio;
using Negocio;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Marcas : PaginaBase
    {
        MarcaNegocio negocio = new MarcaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UsuarioActual.Rol != Rol.Administrador)
                {
                    btnNuevaMarca.Visible = false;
                    gvMarcas.Columns[2].Visible = false; // 0=ID, 1=Nombre, 2=Acciones
                }

                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            gvMarcas.DataSource = negocio.Listar();
            gvMarcas.DataBind();
        }

        protected void gvMarcas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && UsuarioActual.Rol != Rol.Administrador)
            {
                Button btnEditar = (Button)e.Row.FindControl("btnEditar");
                Button btnEliminar = (Button)e.Row.FindControl("btnEliminar");

                if (btnEditar != null) btnEditar.Visible = false;
                if (btnEliminar != null) btnEliminar.Visible = false;
            }
        }

        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            if (!int.TryParse(e.CommandArgument.ToString(), out id))
                return;

            if (e.CommandName == "Eliminar")
            {
                try
                {
                    negocio.Baja(id);
                    cargarGrilla();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
            }

            if (e.CommandName == "Editar")
            {
                Response.Redirect("MarcasForm.aspx?id=" + id);
            }
        }

        protected void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("MarcasForm.aspx");
        }
        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}