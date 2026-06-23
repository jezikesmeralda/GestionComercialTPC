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

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);

                    Cliente cliente = new ClienteNegocio().Listar().Find(x => x.Id == id);

                    txtNombre.Text = cliente.Nombre;
                    txtApellido.Text = cliente.Apellido;
                    txtDni.Text = cliente.Dni.ToString();
                    txtEmail.Text = cliente.Email;
                    txtTelefono.Text = cliente.Telefono;
                    txtDireccion.Text = cliente.Direccion;

                    ViewState["IdCliente"] = cliente.Id;
                }
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

              //pagina error
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
        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                ClienteNegocio negocio = new ClienteNegocio();

                negocio.Baja(id);

                CargarClientes();
            }

            if (e.CommandName == "Editar")
            {
                Response.Redirect("ClientesForm.aspx?id=" + id);
            }
        }
}}