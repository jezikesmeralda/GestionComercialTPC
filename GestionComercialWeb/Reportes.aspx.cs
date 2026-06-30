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
    public partial class Reportes : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsuarioActual.Rol != Rol.Administrador)
                Response.Redirect("Inicio.aspx");

            if (!IsPostBack)
                Cargar();
        }
    
     private void Cargar()
        {
            CargarProductos();
            CargarClientes();
            CargarVendedores();
            CargarStockBajo();

        }
        private void CargarProductos()
        {
            var lista = new ReportesNegocio().ProductosMasVendidos();

            rptProductos.DataSource = lista.Select((item, index) => new
                {
                    Posicion = index + 1,
                    item.NombreProducto,
                    item.CantidadVendida,
                    item.MontoTotal
                }).ToList();

            rptProductos.DataBind();
        }
        private void CargarClientes()
        {
            var lista = new ReportesNegocio().ClientesMasCompraron();

            rptClientes.DataSource = lista.Select((item, index) => new
                {
                    Posicion = index + 1,
                    item.NombreCliente,
                    item.CantidadCompras,
                    item.MontoTotal
                }).ToList();

            rptClientes.DataBind();
        }
        private void CargarVendedores()
        {
            var lista = new ReportesNegocio().Vendedores();

            rptVendedores.DataSource = lista
                .Select((item, index) => new
                {
                    Posicion = index + 1,
                    item.NombreVendedor,
                    item.CantidadVentas,
                    item.MontoTotal
                }).ToList();

            rptVendedores.DataBind();
        }

        private void CargarStockBajo()
        {
            
            rptStockBajo.DataSource = new ReportesNegocio().StockBajo();
            rptStockBajo.DataBind();
        }
    }
}