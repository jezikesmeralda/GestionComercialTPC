using Dominio;
using Negocio;
using System;
using GestionComercialWeb.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class Factura : System.Web.UI.Page
    {
        private VentaNegocio ventaNegocio = new VentaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFactura();
            }
        }
        private void CargarFactura()
        {
            int idVenta;

            if (!int.TryParse(Request.QueryString["id"], out idVenta))
            {
                MostrarNoEncontrada();
                return;
            }

            Venta venta = ventaNegocio.ObtenerPorId(idVenta);

            if (venta == null)
            {
                MostrarNoEncontrada();
                return;
            }

            
            Session["VentaFactura"] = venta;

            lblNumeroFactura.Text = venta.NumeroFactura;
            lblFecha.Text = venta.FechaVenta.ToString("dd/MM/yyyy");
            lblCliente.Text = venta.Cliente.Nombre + " " + venta.Cliente.Apellido;
            lblTotal.Text = venta.Total.ToString("N2");

            rptDetalle.DataSource = venta.Detalles;
            rptDetalle.DataBind();
        }

        private void MostrarNoEncontrada()
        {
            pnlFactura.Visible = false;
            pnlFacturaNoEncontrada.Visible = true;
        }

        protected void btnDescargarPdf_Click(object sender, EventArgs e)
        {
            Venta venta = ObtenerVentaActual();
            if (venta == null) return;

            byte[] pdfBytes = FacturaPdfGenerador.Generar(venta);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + venta.NumeroFactura + ".pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
        }
        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            Venta venta = ObtenerVentaActual();
            if (venta == null) return;

            try
            {
                byte[] pdfBytes = FacturaPdfGenerador.Generar(venta);
                EmailServices.EnviarFactura(venta, pdfBytes);

                MostrarMensajeMail("Factura enviada a " + venta.Cliente.Email + ".", esError: false);
            }
            catch (Exception ex)
            {
                MostrarMensajeMail("No se pudo enviar el mail: " + ex.Message, esError: true);
            }
        }
        private Venta ObtenerVentaActual()
        {
            Venta venta = Session["VentaFactura"] as Venta;

            if (venta == null)
            {
                int idVenta;
                if (int.TryParse(Request.QueryString["id"], out idVenta))
                {
                    venta = ventaNegocio.ObtenerPorId(idVenta);
                }
            }
            return venta;
        }
        private void MostrarMensajeMail(string texto, bool esError)
        {
            lblMensajeMail.Text = texto;
            lblMensajeMail.CssClass = esError ? "text-danger" : "text-success";
            lblMensajeMail.Visible = true;
        }
    }
}

