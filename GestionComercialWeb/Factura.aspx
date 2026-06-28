<%@ Page Title="Factura" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="GestionComercialWeb.Factura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .factura-container {
            max-width: 650px;
            margin: 30px auto;
            border: 1px solid #ccc;
            border-radius: 6px;
            padding: 30px;
            background: #fff;
            font-family: 'Courier New', monospace;
        }
        .factura-header {
            text-align: center;
            border-bottom: 2px dashed #333;
            padding-bottom: 15px;
            margin-bottom: 15px;
        }
        .factura-header h2 {
            margin: 0;
            letter-spacing: 2px;
        }
        .factura-datos {
            margin-bottom: 15px;
        }
        .factura-tabla {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 15px;
        }
        .factura-tabla th, .factura-tabla td {
            padding: 6px 4px;
            text-align: left;
        }
        .factura-tabla th {
            border-bottom: 1px dashed #333;
        }
        .factura-tabla td.num, .factura-tabla th.num {
            text-align: right;
        }
        .factura-total {
            border-top: 2px dashed #333;
            padding-top: 10px;
            text-align: right;
            font-size: 1.3em;
            font-weight: bold;
        }
        .factura-acciones {
            margin-top: 25px;
            text-align: center;
        }
        .factura-acciones .btn {
            margin: 0 5px;
        }

        @media print {
            .no-imprimir {
                display: none !important;
            }
            .factura-container {
                border: none;
                margin: 0;
            }
        }
    </style>

    <div class="factura-container" id="contenidoFactura">

        <asp:Panel ID="pnlFacturaNoEncontrada" runat="server" Visible="false">
            <p class="text-center text-danger">No se encontró la venta solicitada.</p>
        </asp:Panel>

        <asp:Panel ID="pnlFactura" runat="server">

            <div class="factura-header">
                <h2>VINOTECA</h2>
                <p>Factura N° <asp:Label ID="lblNumeroFactura" runat="server"></asp:Label></p>
            </div>

            <div class="factura-datos">
                <p><strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server"></asp:Label></p>
                <p><strong>Cliente:</strong> <asp:Label ID="lblCliente" runat="server"></asp:Label></p>
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

    <div class="factura-acciones no-imprimir">
        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="btn btn-primary" OnClientClick="window.print(); return false;" />
        <asp:LinkButton ID="btnDescargarPdf" runat="server" CssClass="btn btn-secondary" OnClick="btnDescargarPdf_Click">Descargar PDF</asp:LinkButton>
        <a id="lnkEnviarMail" runat="server" class="btn btn-outline-secondary" href="#">Enviar por mail</a>
        <a href="Ventas.aspx" class="btn btn-success">Nueva Venta</a>
    </div>

</asp:Content>