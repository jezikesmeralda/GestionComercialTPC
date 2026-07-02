using Dominio;
using Negocio;
using System;
using System.Linq;

namespace GestionComercialWeb
{
    public partial class HistorialVentas : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsuarioActual.Rol != Rol.Administrador)
                Response.Redirect("Inicio.aspx");

            if (!IsPostBack)
            {
                var ventas = new VentaNegocio().Listar();
                gvHistorialVentas.DataSource = ventas.Select(v => new
                {
                    v.Id,
                    v.NumeroFactura,
                    v.FechaVenta,
                    NombreCliente = v.Cliente.Nombre,
                    NombreVendedor = v.Vendedor.UserName,
                    v.Total
                }).ToList();
                gvHistorialVentas.DataBind();
            }
        }
    }
}