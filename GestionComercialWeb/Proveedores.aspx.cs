using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace GestionComercialWeb
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProveedores();
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
    }
}
    