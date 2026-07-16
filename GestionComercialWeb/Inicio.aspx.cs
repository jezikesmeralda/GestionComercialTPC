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

                if (UsuarioActual.Rol != Rol.Administrador)
                {
                    divCompras.Visible = false;
                    divProveedores.Visible = false;
                    divMarcas.Visible = false;
                    divCategorias.Visible = false;
                    divReportes.Visible = false;
                }
            }
        }
    }
}