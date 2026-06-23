using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class ClientesForm : System.Web.UI.Page
    {
        ClienteNegocio negocio = new ClienteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);

                    Cliente cliente = negocio.Listar().Find(x => x.Id == id);

                    if (cliente != null)
                    {
                        hfId.Value = cliente.Id.ToString();
                        txtNombre.Text = cliente.Nombre;
                        txtApellido.Text = cliente.Apellido;
                        txtDni.Text = cliente.Dni.ToString();
                        txtTelefono.Text = cliente.Telefono;
                        txtEmail.Text = cliente.Email;
                        txtDireccion.Text = cliente.Direccion;
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
            Dominio.Cliente cliente = new Dominio.Cliente();

            cliente.Nombre = txtNombre.Text;
            cliente.Apellido = txtApellido.Text;
            cliente.Dni = int.Parse(txtDni.Text);
            cliente.Telefono = txtTelefono.Text;
            cliente.Email = txtEmail.Text;
            cliente.Direccion = txtDireccion.Text;

            if (!string.IsNullOrEmpty(hfId.Value))
            {
                cliente.Id = int.Parse(hfId.Value);
                negocio.Modificar(cliente); 
            }
            else
            {
                negocio.Alta(cliente); 
            }

            Response.Redirect("Clientes.aspx");
        }
    }
}