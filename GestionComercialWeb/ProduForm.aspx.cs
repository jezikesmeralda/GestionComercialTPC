using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace GestionComercialWeb
{
    public partial class ProduForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                    CategoriaNegocio categorianegocio = new CategoriaNegocio();
                    MarcaNegocio marcanegocio = new MarcaNegocio();
            try
            {
                if (!IsPostBack)
                {
                    
                    ddlMarca.DataSource = marcanegocio.Listar();
                    ddlMarca.DataTextField = "Nombre";
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataBind();


                    ddlCategoria.DataSource = categorianegocio.Listar();
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataBind();
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);

            }
            
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto();
            producto.Id = int.Parse(txtID.Text);
            producto.NombreProducto = txtNombre.Text;
            producto.Marca = new Marca() { Id = int.Parse(ddlMarca.SelectedValue) };
            producto.Categoria = new Categoria() { Id = int.Parse(ddlCategoria.SelectedValue) };
            producto.PrecioCosto = decimal.Parse(txtPrecioCosto.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            producto.PorcentajeGanancia = decimal.Parse(txtPorcetajeGanancia.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            producto.ImagenUrl = txtURLImagen.Text;
            producto.StockMinimo = int.Parse(txtStockMinimo.Text);
            producto.StockActual = int.Parse(txtStockActual.Text);
            producto.Activo = true;

            Session["Producto"] = producto;
            Response.Redirect("Productos.aspx");
        }
    }
}