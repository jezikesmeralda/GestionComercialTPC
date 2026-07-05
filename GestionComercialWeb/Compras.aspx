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
                        <asp:ListItem Text="Seleccione un proveedor..." Value="0" />

                    </asp:DropDownList>
                    <asp:Label ID="lblErrorProveedor" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                </div>
            </div>
            <hr />

            <asp:UpdateProgress ID="upProgress" runat="server" AssociatedUpdatePanelID="upCompra">
                <ProgressTemplate>
                    <div class="text-center text-muted small mt-1 mb-2">
                        <span class="spinner-border spinner-border-sm" role="status"></span>
                        Procesando...
         
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="upCompra" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="pnlExito" runat="server" Visible="false">
                        <div class="alert alert-success alert-dismissible mb-4">
                            <h5 class="alert-heading">✔ Compra registrada correctamente</h5>
                            <p class="mb-0">
                                <strong>Proveedor:</strong>
                                <asp:Label ID="lblExitoProveedor" runat="server"></asp:Label>
                                &nbsp;&nbsp;
            
                                <strong>Total:</strong>
                                <asp:Label ID="lblExitoTotal" runat="server"></asp:Label>
                            </p>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlBuscador" runat="server" DefaultButton="btnBuscar">
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
                    </asp:Panel>

                        <asp:Panel ID="pnlResultados" runat="server" Visible="false">
                            <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" DataKeyNames="Id" OnRowCommand="gvProductos_RowCommand">

                                <Columns>

                                    <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />

                                    <asp:BoundField DataField="StockActual" HeaderText="Stock" ItemStyle-HorizontalAlign="Center" />

                                    <asp:BoundField DataField="PrecioCosto" HeaderText="Ultimo Costo" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" />

                                    <asp:ButtonField Text="Seleccionar" CommandName="Seleccionar" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-primary" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>

                        <asp:Panel ID="pnlProductoSeleccionado" runat="server" Visible="false">
                            <div class="alert alert-success mb-3">
                                <div class="d-flex justify-content-between align-items-center">
                                    <div>
                                        <strong>Producto seleccionado:</strong>
                                        <asp:Label ID="lblProductoSeleccionado" runat="server"></asp:Label>
                                        &nbsp;&nbsp;
                
                                        <strong>Stock actual:</strong>
                                        <asp:Label ID="lblStockSeleccionado" runat="server"></asp:Label>
                                    </div>
                                    <asp:Button ID="btnCancelarSeleccion" runat="server" Text="✕ Cambiar" CssClass="btn btn-sm btn-outline-secondary" OnClick="btnCancelarSeleccion_Click" />
                                </div>
                            </div>
                        </asp:Panel>

                       <asp:Panel ID="pnlAgregar" runat="server" DefaultButton="btnAgregar">
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
                             <asp:Label ID="lblErrorAgregar" runat="server" CssClass="text-danger d-block mt-1" Visible="false"></asp:Label>
                    </asp:Panel>

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
                        <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-2" Visible="false"></asp:Label>

                        <div class="text-end">
                            <asp:Button ID="btnRegistrarCompra" runat="server" Text="Registrar Compra" CssClass="btn btn-success" OnClick="btnRegistrarCompra_Click" />
                        </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelarSeleccion" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnRegistrarCompra" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

        </div>

    </div>

</asp:Content>
