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
                    int id = int.Parse(Request.QueryString["id"]);
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
            if (!Page.IsValid)
            {
                litMensaje.Text = @"
                    <div class='alert alert-warning alert-dismissible fade show' role='alert'>
                        <strong>¡Atención!</strong> Por favor revise los campos marcados en rojo.
                        <button type='button' class='btn-close' data-bs-dismiss='alert'></button>
                    </div>";
                return;
            }

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
                    negocio.Alta(nuevo); // O negocio.Alta(nuevo)
                }


                Response.Redirect("Proveedores.aspx");
            }
            catch (Exception ex)
            {
                litMensaje.Text = $@"
                    <div class='alert alert-danger alert-dismissible fade show' role='alert'>
                        <strong>¡Error de Servidor!</strong> No se pudo procesar la solicitud. Detalle: {ex.Message}
                        <button type='button' class='btn-close' data-bs-dismiss='alert'></button>
                    </div>";
            }
        }
    }
}

