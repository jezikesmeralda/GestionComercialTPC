<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialCompras.aspx.cs" 
    Inherits="GestionComercialWeb.HistorialCompras" %>
<asp:Content ID="HistorialCompras" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="mb-0">Historial de Compras</h2>
        <a href="Compras.aspx" class="btn btn-outline-secondary">Volver</a>
    </div>
    <asp:GridView ID="gvHistorialCompra" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">

    <Columns>

        <asp:BoundField HeaderText="IDHCompra" DataField="Id" />

        <asp:TemplateField HeaderText="Producto">
    <ItemTemplate>
        <%# Eval("Producto.NombreProducto") %>
    </ItemTemplate>
</asp:TemplateField>

        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />

        <asp:BoundField HeaderText="PrecioUnitario" DataField="PrecioUnitario" />

        <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" />
    </Columns>

</asp:GridView>
</asp:Content>
