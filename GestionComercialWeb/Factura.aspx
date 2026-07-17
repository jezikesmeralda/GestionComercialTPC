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
            <asp:Panel ID="pnlIntereses" runat="server" Visible="false">
                <div class="factura-total" style="font-size: 0.9em; border-top: none;">
                    Subtotal: $<asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                </div>
                <div class="factura-total" style="font-size: 0.9em; border-top: none;">
                    Interés (<asp:Label ID="lblInteresPct" runat="server"></asp:Label>%): 
                        $<asp:Label ID="lblMontoInteres" runat="server"></asp:Label>
                </div>
                <div class="factura-total" style="font-size: 0.9em; border-top: none;">
                    Medio de Pago:
                    <asp:Label ID="lblMedioPago" runat="server"></asp:Label>
                    &nbsp;|&nbsp;
                        <asp:Label ID="lblCuotas" runat="server"></asp:Label>
                    cuotas de 
                        $<asp:Label ID="lblCuotaMensual" runat="server"></asp:Label>
                </div>
            </asp:Panel>

            

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
    <div class="card shadow-sm mt-4">
        <div class="card-header bg-info text-white">
            <h5 class="mb-0">Resumen de Pago</h5>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-6">
                    <p class="mb-2"><strong>Medio de Pago:</strong>
                        <asp:Label ID="Label2" runat="server"></asp:Label></p>
                    <asp:Panel ID="pnlBancoEnFactura" runat="server" Visible="false">
                        <p class="mb-2"><strong>Banco:</strong>
                            <asp:Label ID="lblBancoEnFactura" runat="server"></asp:Label></p>
                        <p class="mb-0"><strong>Tarjeta terminada en:</strong>
                            <asp:Label ID="lblDigitosEnFactura" runat="server"></asp:Label></p>
                    </asp:Panel>
                </div>
                <div class="col-md-6 text-end">
                    <asp:Panel ID="pnlCuotasEnFactura" runat="server" Visible="false">
                        <p class="mb-2">Cuotas: <strong>
                            <asp:Label ID="lblCuotasEnFactura" runat="server"></asp:Label></strong></p>
                        <p class="mb-2">Interés: <strong>$<asp:Label ID="lblInteresEnFactura" runat="server"></asp:Label></strong></p>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
