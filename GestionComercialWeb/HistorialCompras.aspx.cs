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
    public partial class HistorialCompras : PaginaBase
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (UsuarioActual.Rol != Rol.Administrador)
                Response.Redirect("Inicio.aspx");

            if (!IsPostBack)
            {
                var lista = new DetalleCompraNegocio().Listar();

                gvHistorialCompra.DataSource = lista;

                gvHistorialCompra.DataBind();
            }
        }
    }
}