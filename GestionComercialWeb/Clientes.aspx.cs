using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Dni = int.Parse(txtDni.Text);
                cliente.Email = txtEmail.Text;
                cliente.Telefono = txtTelefono.Text;
                cliente.Direccion = txtDireccion.Text;

                new ClienteNegocio().Alta(cliente);
                CargarClientes();
                LimpiarCampos();
            }
            catch(Exception ex)
            {

               /* lblError.Text = "Por favor, complete los campos correctamente.";
                lblError.Visible = true;*/
            }

        }
        private void CargarClientes()
        {
            ClienteNegocio negocio = new ClienteNegocio();

            gvClientes.DataSource = negocio.Listar();
            gvClientes.DataBind();
        }
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDni.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";

            txtNombre.Focus();
        }
    }
}