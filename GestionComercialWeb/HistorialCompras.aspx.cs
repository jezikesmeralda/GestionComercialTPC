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
    public partial class HistorialCompras : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            var lista = new DetalleCompraNegocio().Listar();

            Response.Write("Cantidad: " + lista.Count);

            gvHistorialCompra.DataSource = lista;
            gvHistorialCompra.DataBind();
        }
    }
}