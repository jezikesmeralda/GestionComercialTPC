using Dominio;
using Negocio;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Categorias : PaginaBase
    {
        CategoriaNegocio negocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UsuarioActual.Rol != Rol.Administrador)
                {
                    btnCategoria.Visible = false;
                    gvCategorias.Columns[2].Visible = false; 
                }

                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            gvCategorias.DataSource = negocio.Listar();
            gvCategorias.DataBind();
        }

        protected void gvCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && UsuarioActual.Rol != Rol.Administrador)
            {
                Button btnEditar = (Button)e.Row.FindControl("btnEditar");
                Button btnEliminar = (Button)e.Row.FindControl("btnEliminar");

                if (btnEditar != null) btnEditar.Visible = false;
                if (btnEliminar != null) btnEliminar.Visible = false;
            }
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            if (!int.TryParse(e.CommandArgument.ToString(), out id))
            {
                MostrarError("La categoría seleccionada no es válida.");
                return;
            }
           
                if (e.CommandName == "Eliminar")
            {
                try
                {
                    negocio.Baja(id);
                cargarGrilla();
                    lblError.Text = "Cliente eliminado correctamente.";
                    lblError.CssClass = "alert alert-success d-block mb-3";
                    lblError.Visible = true;
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.CssClass = "alert alert-danger d-block mb-3";
                    lblError.Visible = true;
                }
            }
            

            if (e.CommandName == "Editar")
            {
                Response.Redirect("CategoriasForm.aspx?id=" + id);
            }
        }

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoriasForm.aspx");
        }
        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}