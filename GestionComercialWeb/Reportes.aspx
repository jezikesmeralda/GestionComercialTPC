<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="GestionComercialWeb.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="mb-0">Reportes</h2>
    </div>

    <div class="card shadow-sm mb-4">
        <div class="card-header bg-dark text-white">
            <h5 class="mb-0">Productos más vendidos (por unidades)</h5>
        </div>
        <div class="card-body">
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Producto</th>
                        <th class="text-center">Unidades vendidas</th>
                        <th class="text-end">Total facturado</th>
                    </tr>
                </thead>

                <tbody>
                    <asp:Repeater ID="rptProductos" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Posicion") %></td>
                                <td><%# Eval("NombreProducto") %></td>
                                <td class="text-center"><%# Eval("CantidadVendida") %></td>
                                <td class="text-end"><%# Eval("MontoTotal", "{0:C2}") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    <div class="card shadow-sm mb-4">
        <div class="card-header bg-dark text-white">
            <h5 class="mb-0">Clientes que más gastaron</h5>
        </div>
        <div class="card-body">
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Cliente</th>
                        <th class="text-center">Cantidad de compras</th>
                        <th class="text-end">Total gastado</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptClientes" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Posicion") %></td>
                                <td><%# Eval("NombreCliente") %></td>
                                <td class="text-center"><%# Eval("CantidadCompras") %></td>
                                <td class="text-end"><%# Eval("MontoTotal", "{0:C2}") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    <div class="card shadow-sm mb-4">
    <div class="card-header bg-dark text-white">
        <h5 class="mb-0">Vendedores con más facturación</h5>
    </div>
    <div class="card-body">
        <table class="table table-striped table-hover">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Vendedor</th>
                    <th class="text-center">Ventas realizadas</th>
                    <th class="text-end">Total vendido</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptVendedores" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Posicion") %></td>
                            <td><%# Eval("NombreVendedor") %></td>
                            <td class="text-center"><%# Eval("CantidadVentas") %></td>
                            <td class="text-end"><%# Eval("MontoTotal", "{0:C2}") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</div>

    <div class="card shadow-sm mb-4">
        <div class="card-header bg-dark text-white">
            <h5 class="mb-0">Productos con stock bajo</h5>
        </div>
        <div class="card-body">
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>Producto</th>
                        <th>Marca</th>
                        <th class="text-end">Stock actual</th>
                        <th class="text-end">Stock mínimo</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptStockBajo" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("NombreProducto") %></td>
                                <td><%# Eval("NombreMarca") %></td>
                                <td class="text-end"><%# Eval("StockActual") %></td>
                                <td class="text-end"><%# Eval("StockMinimo") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
