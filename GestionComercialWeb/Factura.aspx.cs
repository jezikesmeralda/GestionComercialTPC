using Dominio;
using Negocio;
using System;
using GestionComercialWeb.Services;

namespace GestionComercialWeb
{
    public partial class Factura : PaginaBase
    {
        private VentaNegocio ventaNegocio = new VentaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
                CargarFactura();
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
            lblMedioPago.Text = venta.MedioPago ?? "No especificado";

            if (venta.MedioPago != "Efectivo")
            {
                pnlBancoEnFactura.Visible = true;
                lblBancoEnFactura.Text = venta.Banco ?? "";
                lblDigitosEnFactura.Text = venta.UltimosDigitos ?? "";
            }

            if (venta.MedioPago == "Credito")
            {
                pnlCuotasEnFactura.Visible = true;
                lblCuotasEnFactura.Text = venta.Cuotas.ToString();
                lblInteresEnFactura.Text = venta.Interes.ToString("F2");
            }
            
            if (venta.Cuotas > 1 && venta.Interes > 0)
            {
                pnlIntereses.Visible = true;
                lblSubtotal.Text = venta.Total.ToString("N2");
                lblInteresPct.Text = venta.Interes.ToString("N0");
                lblMontoInteres.Text = (venta.TotalConInteres - venta.Total).ToString("N2");
                lblMedioPago.Text = venta.MedioPago;
                lblCuotas.Text = venta.Cuotas.ToString();
                lblCuotaMensual.Text = (venta.TotalConInteres / venta.Cuotas).ToString("N2");
                lblTotal.Text = venta.TotalConInteres.ToString("N2");
            }
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
            if (venta == null)
            {
                MostrarMensajeMail("No se pudo obtener la factura.", true);
                return;
            }
            try
            {
                byte[] pdfBytes = FacturaPdfGenerador.Generar(venta);

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + venta.NumeroFactura + ".pdf");
                Response.BinaryWrite(pdfBytes);
                Response.End();
            }
            catch (Exception ex)
            {
                MostrarMensajeMail("Error al generar el PDF: " + ex.Message, true);
            }
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
                    venta = ventaNegocio.ObtenerPorId(idVenta);
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