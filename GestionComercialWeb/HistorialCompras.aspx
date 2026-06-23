<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialCompras.aspx.cs" 
    Inherits="GestionComercialWeb.HistorialCompras" %>
<asp:Content ID="HistorialCompras" ContentPlaceHolderID="MainContent" runat="server">
  
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
