using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionComercialWeb
{
    public partial class MarcasForm : System.Web.UI.Page
    {
        MarcaNegocio negocio = new MarcaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);

                    Marca marca = negocio.Listar()
                                         .Find(x => x.Id == id);

                    hfId.Value = marca.Id.ToString();
                    txtNombre.Text = marca.Nombre;
                }
            }

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
          try { 
                Marca marca = new Marca();

                marca.Nombre = txtNombre.Text;

                if (!string.IsNullOrEmpty(hfId.Value))
                {
                    marca.Id = int.Parse(hfId.Value);
                    negocio.Modificar(marca);
                }
                else
                {
                    negocio.Alta(marca);
                }

                Response.Redirect("Marcas.aspx");
            }
           catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }
    }
}
    