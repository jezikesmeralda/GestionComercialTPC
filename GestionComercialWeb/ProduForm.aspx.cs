using Negocio;
using System;
using System.Web.UI;
using Dominio;

namespace GestionComercialWeb
{
    public partial class ProduForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    MarcaNegocio marcanegocio = new MarcaNegocio();
                    ddlMarca.DataSource = marcanegocio.Listar();
                    ddlMarca.DataTextField = "Nombre";
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataBind();

                    CategoriaNegocio categorianegocio = new CategoriaNegocio();
                    ddlCategoria.DataSource = categorianegocio.Listar();
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                producto.NombreProducto = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.ImagenUrl = txtURLImagen.Text;
                producto.PrecioCosto = decimal.Parse(txtPrecioCosto.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                producto.PorcentajeGanancia = decimal.Parse(txtPorcetajeGanancia.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                producto.StockMinimo = int.Parse(txtStockMinimo.Text);
                producto.StockActual = int.Parse(txtStockActual.Text);
                producto.Activo = true;

                int idMarca = int.Parse(ddlMarca.SelectedValue);
                int idCategoria = int.Parse(ddlCategoria.SelectedValue);

                new ProductoNegocio().Alta(producto, idMarca, idCategoria);

                Response.Redirect("Productos.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Por favor, complete los campos correctamente.";
                lblError.Visible = true;
            }
        }
    }
}