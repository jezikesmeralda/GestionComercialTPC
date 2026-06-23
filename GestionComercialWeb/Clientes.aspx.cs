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
        ClienteNegocio negocio = new ClienteNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();

            }
        }

        
        private void CargarClientes()
        {
            ClienteNegocio negocio = new ClienteNegocio();

            gvClientes.DataSource = negocio.Listar();
            gvClientes.DataBind();
        }
        
        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null) return;

            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                negocio.Baja(id);
                CargarClientes();
            }

            if (e.CommandName == "Editar")
            {
                Response.Redirect("ClientesForm.aspx?id=" + id);
            }
        }
        protected void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientesForm.aspx");
        }
    }
}