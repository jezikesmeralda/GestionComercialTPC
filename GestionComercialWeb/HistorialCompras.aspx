<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialCompras.aspx.cs" Inherits="GestionComercialWeb.HistorialCompras" %>

<asp:Content ID="HistorialCompras" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="mb-0">Historial de Compras</h2>
        <a href="Compras.aspx" class="btn btn-outline-secondary">Volver</a>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <asp:GridView ID="gvHistorialCompra" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="N° Compra" DataField="Id" />
                    <asp:BoundField HeaderText="Fecha" DataField="FechaCompra" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField HeaderText="Proveedor" DataField="NombreProveedor" />
                    <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C2}" />
                    <asp:TemplateField HeaderText="Detalle">
                        <ItemTemplate>
                            <a href='<%# "DetalleDeCompra.aspx?id=" + Eval("Id") %>' class="btn btn-outline-primary btn-sm">Ver Detalle</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>