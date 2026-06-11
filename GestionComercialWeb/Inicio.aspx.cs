using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace GestionComercialWeb
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ProductoNegocio productoNeg = new ProductoNegocio();
                lblProductos.Text = productoNeg.ContarActivos().ToString();
                lblClientes.Text = "10";
                lblStockBajo.Text = "3";
            }
        }
    }
}