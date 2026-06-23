<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="GestionComercialWeb.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-4">
    <h2 class="mb-0">Compras</h2>

    <asp:Button ID="btnHistorial"
        runat="server"
        Text="Historial"
        CssClass="btn btn-outline-primary"
        PostBackUrl="~/HistorialCompras.aspx" />
</div>

    <div class="card shadow-sm mb-4">

        <div class="card-body">

            <div class="row mb-3">

                <div class="col-md-3">
                    <label>Proveedor</label>
                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Todas" Value="0" />
                    </asp:DropDownList>
                </div>
            </div>
             <hr />
            <div class="row align-items-end mb-4">
                <div class="col-md-4">
                    <label>Buscar Producto</label>
                    <asp:TextBox ID="txtBuscarProducto" runat="server" CssClass="form-control" placeholder="Ingrese nombre del producto"></asp:TextBox>

                </div>

                <div class="col-md-2">

                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" Onclick="btnBuscar_Click"/>

                </div>

            </div>

            <div class="alert alert-secondary mb-4">

                <strong>Producto encontrado:</strong>
                <asp:Label ID="lblProductoEncontrado" runat="server" Text="Ninguno"></asp:Label>
                <div class="row mt-2">

                    <div class="col-md-3">
                        <strong>Stock Actual:</strong>
                        <asp:Label ID="lblStockActual" runat="server" Text="-"></asp:Label>
                    </div>

                    <div class="col-md-3">
                        <strong>Último Costo:</strong>
                        <asp:Label ID="lblUltimoCosto" runat="server" Text="-"></asp:Label>
                    </div>

                </div>
            </div>

            <div class="row align-items-end mb-3">
                <div class="col-md-2">
                    <label>Cantidad</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="col-md-2">
                    <label>Precio Compra</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"> </asp:TextBox>
                </div>

                <div class="col-md-2">

                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary w-100" OnClick="btnAgregar_Click" />

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

                <asp:Button ID="btnRegistrarCompra" runat="server" Text="Registrar Compra" CssClass="btn btn-success" OnClick="btnRegistrarCompra_Click" />

            </div>

        </div>

    </div>

</asp:Content>
