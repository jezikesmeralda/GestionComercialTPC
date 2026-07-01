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
    public partial class CategoriasForm : System.Web.UI.Page
    {
        CategoriaNegocio negocio = new CategoriaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);

                   Categoria categoria = negocio.Listar().Find(x => x.Id == id);

                    hfId.Value = categoria.Id.ToString();
                    txtNombre.Text = categoria.Nombre;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                Categoria categoria = new Categoria();

                categoria.Nombre = txtNombre.Text;

                if (!string.IsNullOrEmpty(hfId.Value))
                {
                    categoria.Id = int.Parse(hfId.Value);
                    negocio.Modificar(categoria);
                }
                else
                {
                    negocio.Alta(categoria);
                }

                Response.Redirect("Categorias.aspx");
            }
            catch (Exception ex)
            {
                
                lblError.Text =  ex.Message;
            }
    }
}
}

