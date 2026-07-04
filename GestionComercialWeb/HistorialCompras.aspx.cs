using Dominio;
using Negocio;
using System;
using System.Linq;

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
                var compras = new ComprasNegocio().Listar();
                gvHistorialCompra.DataSource = compras.Select(c => new
                {
                    c.Id,
                    c.FechaCompra,
                    NombreProveedor = c.Proveedor.Nombre,
                    c.Total
                }).ToList();
                gvHistorialCompra.DataBind();
            }
        }
    }
}