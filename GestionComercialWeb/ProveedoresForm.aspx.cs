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
    public partial class ProveedoresForm : System.Web.UI.Page
    {
        ProveedorNegocio negocio = new ProveedorNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id;
                    if (!int.TryParse(Request.QueryString["id"], out id))
                    {
                        Response.Redirect("Proveedores.aspx");
                        return;
                    }
                    Dominio.Proveedor prov = negocio.Listar().Find(x => x.Id == id);

                    if (prov != null)
                    {
                        hfId.Value = prov.Id.ToString();
                        txtNombre.Text = prov.Nombre;
                        txtTelefono.Text = prov.Telefono;
                        txtEmail.Text = prov.Email;

                    }
                }

            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (!Page.IsValid)
            {
               MostrarError(lblError,"Por favor, complete todos los campos requeridos correctamente.");
                return;
            }
            litMensaje.Text = "";
            lblErrorNombre.Visible = false;
            lblErrorTelefono.Visible = false;
            lblErrorEmail.Visible = false;

            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarError(lblErrorNombre, "El nombre es obligatorio.");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text) )
            {
                MostrarError(lblErrorTelefono, "El teléfono es obligatorio");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) )
            {
                MostrarError(lblErrorEmail, "Ingrese un email válido.");
                valido = false;
            }

            if (!valido) return;

            try
            {
                Dominio.Proveedor nuevo = new Dominio.Proveedor();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Telefono = txtTelefono.Text;
                nuevo.Email = txtEmail.Text;

                if (!string.IsNullOrEmpty(hfId.Value))
                {
                    nuevo.Id = int.Parse(hfId.Value);
                    negocio.Modificar(nuevo);
                }
                else
                {
                    negocio.Alta(nuevo); 
                }


                Response.Redirect("Proveedores.aspx");
            }
            catch (Exception ex)
            {
                MostrarError(lblError,"Ocurrió un error al guardar el proveedor.");
            }
        }
        private void MostrarError(Label lbl, string mensaje)
        {
            lbl.Text = mensaje;
            lbl.Visible = true;
        }
    }
}

