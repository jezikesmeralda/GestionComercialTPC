using Dominio;
using Negocio;
using System;
using System.Linq;

namespace GestionComercialWeb
{
    public partial class DetalleCompra : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsuarioActual.Rol != Rol.Administrador)
                Response.Redirect("Inicio.aspx");

            if (!IsPostBack)
            {
                int id;
                if (!int.TryParse(Request.QueryString["id"], out id))
                {
                    Response.Redirect("HistorialCompras.aspx");
                    return;
                }

                Compra compra = new ComprasNegocio().ObtenerPorId(id);

                if (compra == null)
                {
                    Response.Redirect("HistorialCompras.aspx");
                    return;
                }

                lblId.Text = compra.Id.ToString();
                lblFecha.Text = compra.FechaCompra.ToString("dd/MM/yyyy HH:mm");
                lblProveedor.Text = compra.Proveedor.Nombre;
                lblTotal.Text = compra.Total.ToString("C2");

                gvDetalle.DataSource = compra.Detalles.Select(d => new
                {
                    NombreProducto = d.Producto.NombreProducto,
                    d.Cantidad,
                    d.PrecioUnitario,
                    d.Subtotal
                }).ToList();
                gvDetalle.DataBind();
            }
        }
    }
}