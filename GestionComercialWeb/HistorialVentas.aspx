<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialVentas.aspx.cs" Inherits="GestionComercialWeb.HistorialVentas" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="mb-0">Historial de Ventas</h2>
        <a href="Ventas.aspx" class="btn btn-outline-secondary">Volver</a>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <asp:GridView ID="gvHistorialVentas" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="N° Factura" DataField="NumeroFactura" />
                    <asp:BoundField HeaderText="Fecha" DataField="FechaVenta" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField HeaderText="Cliente" DataField="NombreCliente" />
                    <asp:BoundField HeaderText="Vendedor" DataField="NombreVendedor" />
                    <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C2}" />
                    <asp:TemplateField HeaderText="Detalle">
                        <ItemTemplate>
                            <a href='<%# "Factura.aspx?id=" + Eval("Id") %>' class="btn btn-outline-primary btn-sm">Ver Factura</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>