<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleDeCompra.aspx.cs" Inherits="GestionComercialWeb.DetalleCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="mb-0">Detalle de Compra</h2>
        <a href="HistorialCompras.aspx" class="btn btn-outline-secondary">Volver al Historial</a>
    </div>

    <div class="card shadow-sm mb-4">
        <div class="card-body">
            <p><strong>N° Compra:</strong> <asp:Label ID="lblId" runat="server" /></p>
            <p><strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server" /></p>
            <p><strong>Proveedor:</strong> <asp:Label ID="lblProveedor" runat="server" /></p>
        </div>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <asp:GridView ID="gvDetalle" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="Producto" DataField="NombreProducto" />
                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                    <asp:BoundField HeaderText="Precio Unitario" DataField="PrecioUnitario" DataFormatString="{0:C2}" />
                    <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" DataFormatString="{0:C2}" />
                </Columns>
            </asp:GridView>

            <div class="text-end mt-3">
                <h4>Total: <asp:Label ID="lblTotal" runat="server" /></h4>
            </div>
        </div>
    </div>

</asp:Content>