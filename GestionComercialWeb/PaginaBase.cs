using Dominio;
using System;
using System.Web.UI;

namespace GestionComercialWeb
{
    public class PaginaBase : Page
    {
        protected Usuario UsuarioActual
        {
            get { return Session["usuario"] as Usuario; }
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            
            if (Session["usuario"] == null)
            {
                Session["mensaje"] = "Por favor, inicie sesión para continuar.";
                Response.Redirect("~/Login.aspx");
                return;
            }
        }
    }
}