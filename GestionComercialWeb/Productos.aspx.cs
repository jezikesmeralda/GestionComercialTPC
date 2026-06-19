using System;
using System.Collections.Generic;
using System.Web.UI;
using Negocio;
using Dominio;

namespace GestionComercialWeb
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Producto> ListaProductos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListaProductos = new ProductoNegocio().Listar();
            }
            else
            {
                string busqueda = txtBuscar.Text.Trim().ToLower();
                List<Producto> todos = new ProductoNegocio().Listar();
                List<Producto> filtrados = new List<Producto>();

                foreach (Producto p in todos)
                {
                    if (string.IsNullOrWhiteSpace(busqueda) || p.NombreProducto.ToLower().Contains(busqueda))
                        filtrados.Add(p);
                }

                ListaProductos = filtrados;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
        }
    }
}