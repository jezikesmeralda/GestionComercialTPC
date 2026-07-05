using Dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace GestionComercialWeb.Services
{
    public static class EmailServices
    {
        public static void EnviarFactura(Venta venta, byte[] pdfBytes)
        {
            if (venta.Cliente == null || string.IsNullOrWhiteSpace(venta.Cliente.Email))
                throw new Exception("El cliente no tiene un email registrado.");

            string host = ConfigurationManager.AppSettings["Smtp:Host"];
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Smtp:Port"]);
            string usuario = ConfigurationManager.AppSettings["Smtp:Usuario"];
            string password = ConfigurationManager.AppSettings["Smtp:Password"];
            string remitenteEmail = ConfigurationManager.AppSettings["Smtp:From"];
            string nombreRemitente = ConfigurationManager.AppSettings["Smtp:NombreRemitente"];

            using (MailMessage mensaje = new MailMessage())
            {
                mensaje.From = new MailAddress(remitenteEmail, nombreRemitente);
                mensaje.To.Add(venta.Cliente.Email);
                mensaje.Subject = "Factura " + venta.NumeroFactura;
                mensaje.IsBodyHtml = true;
                mensaje.Body = ArmarCuerpoHtml(venta);

                using (MemoryStream stream = new MemoryStream(pdfBytes))
                {
                    mensaje.Attachments.Add(new Attachment(stream, venta.NumeroFactura + ".pdf", "application/pdf"));

                    using (SmtpClient cliente = new SmtpClient(host, puerto))
                    {
                        cliente.EnableSsl = true;
                        cliente.Credentials = new NetworkCredential(usuario, password);
                        cliente.Send(mensaje);
                    }
                }
            }
        }

        private static string ArmarCuerpoHtml(Venta venta)
        {
            return $@"<p>Hola {venta.Cliente.Nombre},</p>
                <p>Adjuntamos la factura <strong>{venta.NumeroFactura}</strong> correspondiente a su compra del {venta.FechaVenta:dd/MM/yyyy}.</p>
                <p>Total: <strong>${venta.Total:N2}</strong></p>
                <p>¡Gracias por su compra!</p>
                <p>Vinoteca</p>
            ";
        }
    

    public static void EnviarCredenciales(string email, string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("El usuario no tiene un email registrado.");

            string host = ConfigurationManager.AppSettings["Smtp:Host"];
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Smtp:Port"]);
            string usuario = ConfigurationManager.AppSettings["Smtp:Usuario"];
            string passwordSmtp = ConfigurationManager.AppSettings["Smtp:Password"];
            string remitenteEmail = ConfigurationManager.AppSettings["Smtp:From"];
            string nombreRemitente = ConfigurationManager.AppSettings["Smtp:NombreRemitente"];

            using (MailMessage mensaje = new MailMessage())
            {
                mensaje.From = new MailAddress(remitenteEmail, nombreRemitente);
                mensaje.To.Add(email);
                mensaje.Subject = "Bienvenido a Vinoteca - Tus credenciales de acceso";
                mensaje.IsBodyHtml = true;
                mensaje.Body = $@"
            <p>Hola <strong>{userName}</strong>,</p>
            <p>Tu cuenta ha sido creada exitosamente en el sistema de Vinoteca.</p>
            <p>Tus credenciales de acceso son:</p>
            <p><strong>Usuario:</strong> {userName}</p>
            <p><strong>Contraseña:</strong> {password}</p>
            <p>Te recomendamos cambiar tu contraseña al ingresar por primera vez.</p>
            <p>Vinoteca</p>
        ";

                using (SmtpClient cliente = new SmtpClient(host, puerto))
                {
                    cliente.EnableSsl = true;
                    cliente.Credentials = new NetworkCredential(usuario, passwordSmtp);
                    cliente.Send(mensaje);
                }
            }
        }

    }

}