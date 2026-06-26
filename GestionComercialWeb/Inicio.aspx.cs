using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace GestionComercialWeb
{
    public partial class Inicio : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ProductoNegocio productoNeg = new ProductoNegocio();
                ClienteNegocio clienteNeg = new ClienteNegocio();

                lblProductos.Text = productoNeg.ContarActivos().ToString();
                lblClientes.Text = clienteNeg.ContarActivos().ToString();
                lblStockBajo.Text = productoNeg.ContarStockBajo().ToString();
            }
        }
    }
}