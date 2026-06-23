using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);

                    Proveedor proveedor = new ProveedorNegocio().Listar().Find(x => x.Id == id);

                    txtNombre.Text = proveedor.Nombre;
                    txtTelefono.Text = proveedor.Telefono;
                    txtEmail.Text = proveedor.Email;

                    ViewState["IdProveedor"] = proveedor.Id;
                }
            }
        }
        private void CargarProveedores()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();

            gvProveedores.DataSource = negocio.Listar();
            gvProveedores.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedor proveedor = new Proveedor();

                proveedor.Nombre = txtNombre.Text;
                proveedor.Telefono = txtTelefono.Text;
                proveedor.Email = txtEmail.Text;

                ProveedorNegocio negocio = new ProveedorNegocio();

                negocio.Alta(proveedor);

                LimpiarCampos();

                CargarProveedores();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
        }
        protected void gvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                MarcaNegocio negocio = new MarcaNegocio();

                negocio.Baja(id);

                CargarProveedores();
            }

            if (e.CommandName == "Editar")
            {
                Response.Redirect("Marcas.aspx?id=" + id);
            }
        }
}
}
