<%@ Page Title="Factura" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="GestionComercialWeb.Factura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/factura.css" rel="stylesheet" />

    <div class="factura-container" id="contenidoFactura">
            <asp:Panel ID="pnlFacturaNoEncontrada" runat="server" Visible="false">
                <p class="text-center text-danger">No se encontró la venta solicitada.</p>
            </asp:Panel>

            <asp:Panel ID="pnlFactura" runat="server">

                <div class="factura-header">
                    <h2>VINOTECA</h2>
                    <p>Factura N°<asp:Label ID="lblNumeroFactura" runat="server"></asp:Label></p>
                 
                </div>

                <div class="mb-3">
                    <p><strong>Fecha:</strong><asp:Label ID="lblFecha" runat="server"></asp:Label></p>
                    <p><strong>Cliente:</strong><asp:Label ID="lblCliente" runat="server"></asp:Label></p>
                </div>

                <table class="factura-tabla">
                    <thead>
                        <tr>
                            <th>Producto</th>
                            <th class="num">Cant.</th>
                            <th class="num">Precio</th>
                            <th class="num">Subtotal</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptDetalle" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Producto.NombreProducto") %></td>
                                    <td class="num"><%# Eval("Cantidad") %></td>
                                    <td class="num">$<%# Eval("PrecioUnitario", "{0:N2}") %></td>
                                    <td class="num">$<%# Eval("Subtotal", "{0:N2}") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>


                <div class="factura-total">
                    TOTAL $<asp:Label ID="lblTotal" runat="server"></asp:Label>
                </div>

            </asp:Panel>
        </div>
        
        <div class="text-center mt-4  no-imprimir">
            <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="btn btn-primary" OnClientClick="window.print(); return false;" />
            <asp:LinkButton ID="btnDescargarPdf" runat="server" CssClass="btn btn-secondary" OnClick="btnDescargarPdf_Click">Descargar PDF</asp:LinkButton>
            <asp:LinkButton ID="btnEnviarMail" runat="server" CssClass="btn btn-outline-secondary" OnClick="btnEnviarMail_Click">Enviar por mail</asp:LinkButton>
            <div class="text-center mt-2 no-imprimir">
             <asp:Label ID="lblMensajeMail" runat="server" Visible="false"></asp:Label>
             </div>
        </div>
</asp:Content>