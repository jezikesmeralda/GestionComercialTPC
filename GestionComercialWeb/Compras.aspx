<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="GestionComercialWeb.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="mb-0">Compras</h2>

        <asp:Button ID="btnHistorial" runat="server" Text="Historial" CssClass="btn btn-outline-primary" PostBackUrl="~/HistorialCompras.aspx" Visible="false"/>
    </div>

    <div class="card shadow-sm mb-4">

        <div class="card-body">

            <div class="row mb-3">

                <div class="col-md-3">
                    <label>Proveedor</label>
                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Todas" Value="0" />
                        
                    </asp:DropDownList>
                    <asp:Label ID="lblErrorProveedor" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                </div>
            </div>
            <hr />
            <asp:UpdatePanel ID="upCompra" runat="server">
                <ContentTemplate>
                    <div class="row align-items-end mb-4">
                        <div class="col-md-4">
                            <label>Buscar Producto</label>
                            <asp:TextBox ID="txtBuscarProducto" runat="server" CssClass="form-control" placeholder="Ingrese nombre del producto"></asp:TextBox>
                            <asp:Label ID="lblErrorBusqueda" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                        </div>

                        <div class="col-md-2">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" OnClick="btnBuscar_Click" />
                            

                        </div>

                    </div>

                    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" DataKeyNames="Id" OnRowCommand="gvProductos_RowCommand">

                        <Columns>

                            <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />

                            <asp:BoundField DataField="StockActual" HeaderText="Stock" />

                            <asp:BoundField DataField="PrecioCosto" HeaderText="Costo" DataFormatString="{0:C2}" />

                            <asp:ButtonField Text="Seleccionar" CommandName="Seleccionar" ButtonType="Button"/>
                        </Columns>

                    </asp:GridView>
                    <asp:Label ID="lblErrorAgregar" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                    <div class="row align-items-end mb-3">
                        <div class="col-md-2">
                            <label>Cantidad</label>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <asp:Label ID="lblErrorCantidad" runat="server" CssClass="text-danger" Visible="false"></asp:Label>                        
                        </div>

                        <div class="col-md-2">
                            <label>Precio Compra</label>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"> </asp:TextBox>
                            <asp:Label ID="lblErrorPrecioCompra" runat="server" CssClass="text-danger" Visible="false"></asp:Label> 
                        </div>

                        <div class="col-md-2">

                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary w-100" OnClick="btnAgregar_Click" />

                        </div>

                    </div>

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
                </ContentTemplate>
            </asp:UpdatePanel>

            

            <div class="text-end">
                <asp:Button ID="btnRegistrarCompra" runat="server" Text="Registrar Compra" CssClass="btn btn-success" OnClick="btnRegistrarCompra_Click" />

            </div>

        </div>

    </div>

</asp:Content>
