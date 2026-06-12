<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="GestionComercialWeb.Compras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="mb-4">Compras</h2>

<div class="card shadow-sm mb-4">

        <div class="card-body">

            <div class="row">

                <div class="col-md-3 mb-3">
                    <label>Proveedor</label>
                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <div class="col-md-3 mb-3">
                    <label>Producto</label>
                    <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                
                <div class="col-md-2 mb-3">
                    <label>Cantidad</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="col-md-2 mb-3">
                    <label>Precio Compra</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"> </asp:TextBox>
                </div>

                <div class="col-md-2 d-flex align-items-end">

                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary w-100"/>

                </div>

            </div>

        </div>

    </div>

    <div class="card shadow-sm">

        <div class="card-body">

            <h5>Detalle Compra</h5>

            <asp:GridView ID="gvDetalle" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">

                <Columns>

                    <asp:BoundField HeaderText="Producto" DataField="Producto" />

                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                    
                    <asp:BoundField HeaderText="Precio" DataField="PrecioUnitario" />

                    <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" />
                </Columns>

            </asp:GridView>

            <div class="text-end mt-3">

                <h4>Total: $<asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label></h4>

            </div>

            <div class="text-end">

                <asp:Button ID="btnRegistrarCompra" runat="server" Text="Registrar Compra" CssClass="btn btn-success" />

            </div>

        </div>

    </div>

</asp:Content>
