using Dominio;
using Negocio;
using System;
using System.Web.UI;
using System.Net.Mail;

namespace GestionComercialWeb
{
    public partial class ClientesForm : PaginaBase
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

                    if (cliente == null)
                    {
                        Response.Redirect("Clientes.aspx");
                        return;
                        
                    }
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblError.Text = "El nombre es obligatorio.";
                lblError.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                lblError.Text = "El apellido es obligatorio.";
                lblError.Visible = true;
                return;
            }

            int dni;
            if (!int.TryParse(txtDni.Text, out dni) || dni <= 0)
            {
                lblError.Text = "El DNI debe ser un número válido.";
                lblError.Visible = true;
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress(txtEmail.Text);
                }
                catch
                {
                    lblError.Text = "Ingrese un email válido.";
                    lblError.Visible = true;
                    return;
                }
            }
            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Dni = dni;
                cliente.Telefono = txtTelefono.Text;
                cliente.Email = txtEmail.Text;
                cliente.Direccion = txtDireccion.Text;

                if (!string.IsNullOrEmpty(hfId.Value))
                {
                    cliente.Id = int.Parse(hfId.Value);
                    negocio.Modificar(cliente);
                    Session["mensaje"] = "Cliente modificado correctamente.";
                }
                else
                {
                    negocio.Alta(cliente);
                    Session["mensaje"] = "Cliente agregado correctamente.";
                }

                Response.Redirect("Clientes.aspx");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY") || ex.Message.Contains("duplicate key"))
                    lblError.Text = "Ya existe un cliente con ese DNI.";
                else
                    lblError.Text = ex.Message;

                lblError.Visible = true;
            }
        }
    }
}