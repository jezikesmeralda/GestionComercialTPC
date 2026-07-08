using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class ProveedoresForm : PaginaBase
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
            litMensaje.Text = "";
            lblErrorNombre.Visible = false;
            lblErrorTelefono.Visible = false;
            lblErrorEmail.Visible = false;
            lblError.Visible = false;

            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarError(lblErrorNombre, "El nombre es obligatorio.");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MostrarError(lblErrorTelefono, "El teléfono es obligatorio.");
                valido = false;
            }
            else if (!Regex.IsMatch(txtTelefono.Text, @"^\d{10}$"))
            {
                MostrarError(lblErrorTelefono, "El teléfono debe tener exactamente 10 dígitos.");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MostrarError(lblErrorEmail, "El email es obligatorio.");
                valido = false;
            }
            else
            {
                try
                {
                    System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress(txtEmail.Text);
                }
                catch
                {
                    MostrarError(lblErrorEmail, "Ingrese un email válido.");
                    valido = false;
                }
            }

            if (!valido) return;

            try
            {
                Dominio.Proveedor nuevo = new Dominio.Proveedor();
                nuevo.Nombre = txtNombre.Text.Trim();
                nuevo.Telefono = txtTelefono.Text.Trim();
                nuevo.Email = txtEmail.Text.Trim();

                

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
                MostrarError(lblError, "Ocurrió un error al guardar el proveedor.");
            }
        }
        private void MostrarError(Label lbl, string mensaje)
        {
            lbl.Text = mensaje;
            lbl.Visible = true;
        }
    }
}