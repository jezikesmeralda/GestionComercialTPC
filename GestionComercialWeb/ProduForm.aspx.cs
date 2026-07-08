using Negocio;
using System;
using System.Web.UI;
using Dominio;

namespace GestionComercialWeb
{
    public partial class ProduForm : PaginaBase
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

                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        CargarProducto(id);
                    }
                    else
                    {
                        
                       lblStockActualInfo.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
            }
        }

        private void CargarProducto(int id)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto producto = negocio.Listar().Find(p => p.Id == id);

            if (producto != null)
            {
                hdnId.Value = producto.Id.ToString();
                lblTitulo.Text = "Editar Producto";

                txtNombre.Text = producto.NombreProducto;
                txtDescripcion.Text = producto.Descripcion;
                txtURLImagen.Text = producto.ImagenUrl;
                txtPrecioCosto.Text = producto.PrecioCosto.ToString(System.Globalization.CultureInfo.InvariantCulture);
                txtPorcetajeGanancia.Text = producto.PorcentajeGanancia.ToString(System.Globalization.CultureInfo.InvariantCulture);
                txtStockMinimo.Text = producto.StockMinimo.ToString();

                ddlMarca.SelectedValue = producto.Marca.Id.ToString();
                ddlCategoria.SelectedValue = producto.Categoria.Id.ToString();

                
                lblStockActualInfo.Visible = true;
                lblStockActualInfo.Text = producto.StockActual + " unidades";
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                producto.Id = int.Parse(hdnId.Value);
                producto.NombreProducto = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.ImagenUrl = txtURLImagen.Text;
                producto.PrecioCosto = decimal.Parse(txtPrecioCosto.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                producto.PorcentajeGanancia = decimal.Parse(txtPorcetajeGanancia.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                producto.StockMinimo = int.Parse(txtStockMinimo.Text);
                producto.Activo = true;

                int idMarca = int.Parse(ddlMarca.SelectedValue);
                int idCategoria = int.Parse(ddlCategoria.SelectedValue);

                ProductoNegocio negocio = new ProductoNegocio();

                if (producto.Id == 0)
                {
                  
                    negocio.Alta(producto, idMarca, idCategoria);
                }
                else
                {
                    negocio.Modificar(producto, idMarca, idCategoria);
                }
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