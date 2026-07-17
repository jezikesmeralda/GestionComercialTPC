<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="GestionComercialWeb.Ventas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="mb-0">Ventas</h2>
        <asp:Button ID="btnHistorial" runat="server" Text="Historial" CssClass="btn btn-outline-primary" PostBackUrl="~/HistorialVentas.aspx" Visible="false" />
    </div>

    <div class="card shadow-sm mb-4">
        <div class="card-body">

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upVenta">
                <ProgressTemplate>
                    <div class="text-center text-muted small mt-1 mb-2">
                        <span class="spinner-border spinner-border-sm" role="status"></span>
                        Procesando...
       
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="upVenta" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label class="form-label">Cliente</label>
                            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select"></asp:DropDownList>
                            <asp:Label ID="lblErrorCliente" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <hr />
                    <asp:Panel ID="pnlBuscador" runat="server" DefaultButton="btnBuscar">
                        <div class="row align-items-end mb-4">
                            <div class="col-md-5">
                                <label class="form-label">Buscar Producto</label>
                                <asp:TextBox ID="txtBuscarProducto" runat="server" CssClass="form-control" placeholder="Ingrese nombre del producto"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" OnClick="btnBuscar_Click" />
                            </div>
                        </div>
                        <asp:Label ID="lblErrorProducto" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

                    </asp:Panel>

                    <asp:Panel ID="pnlResultados" runat="server" Visible="false">
                        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" DataKeyNames="Id" OnRowCommand="gvProductos_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                                <asp:BoundField DataField="StockActual" HeaderText="Stock" />
                                <asp:BoundField DataField="PrecioVenta" HeaderText="Precio" DataFormatString="{0:C2}" />
                                <asp:ButtonField Text="Seleccionar" CommandName="Seleccionar" ButtonType="Button" />
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
                 
                                    <strong>Stock:</strong>
                                    <asp:Label ID="lblStockSeleccionado" runat="server"></asp:Label>
                                    &nbsp;&nbsp;
                 
                                    <strong>Precio:</strong>
                                    <asp:Label ID="lblPrecioSeleccionado" runat="server"></asp:Label>
                                </div>

                                <asp:Button ID="btnCancelarSeleccion" runat="server" Text="✕ Cambiar" CssClass="btn btn-sm btn-outline-secondary" OnClick="btnCancelarSeleccion_Click" />
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="row align-items-end mb-3">
                        <div class="col-md-3">
                            <label class="form-label">Cantidad</label>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <asp:Label ID="lblErrorCantidad" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                        </div>


                        <div class="col-md-2">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success w-100" OnClick="btnAgregar_Click" />
                        </div>
                        <asp:Label ID="lblErrorAgregar" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                    </div>

                    <hr />
                    <h5>Detalle Venta</h5>

                    <asp:GridView ID="gvDetalleVenta" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderText="Producto" DataField="Producto" />
                            <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                            <asp:BoundField HeaderText="Precio" DataField="Precio" />
                            <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" />
                        </Columns>
                    </asp:GridView>

                    <div class="text-end mt-3">

                        <h5>Subtotal: $
        <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                        </h5>
                        <div class="card shadow-sm mt-3 p-3">
                            <div class="row align-items-end">
                                <div class="col-md-4">
                                    <label class="form-label fw-bold">Medio de Pago</label>
                                    <asp:DropDownList ID="ddlMedioPago" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlMedioPago_Changed" AutoPostBack="true">
                                        <asp:ListItem Text="Efectivo" Value="Efectivo" />
                                        <asp:ListItem Text="Débito" Value="Debito" />
                                        <asp:ListItem Text="Crédito" Value="Credito" />
                                    </asp:DropDownList>
                                    
                                </div>

                                <asp:Panel ID="pnlCuotas" runat="server" Visible="false" CssClass="col-md-4">
                                    <label class="form-label fw-bold">Cuotas</label>
                                    <asp:DropDownList ID="ddlCuotas" runat="server" CssClass="form-select"
                                        OnSelectedIndexChanged="ddlCuotas_Changed" AutoPostBack="true">
                                        <asp:ListItem Text="1 cuota (sin interés)" Value="1" />
                                        <asp:ListItem Text="3 cuotas (10%)" Value="3" />
                                        <asp:ListItem Text="6 cuotas (20%)" Value="6" />
                                        <asp:ListItem Text="12 cuotas (40%)" Value="12" />
                                    </asp:DropDownList>
                                </asp:Panel>

                                <asp:Panel ID="pnlResumenCuotas" runat="server" Visible="false" CssClass="col-md-4 text-end">
                                    <p class="mb-1 text-muted">
                                        Interés (<asp:Label ID="lblInteres" runat="server" Text="0"></asp:Label>%): 
        $<asp:Label ID="lblMontoInteres" runat="server" Text="0"></asp:Label>
                                    </p>
                                    <p class="mb-1">Total con interés: <strong>$<asp:Label ID="lblTotalConInteres" runat="server" Text="0"></asp:Label></strong></p>
                                    <p class="mb-0 text-muted">Cuota mensual: $<asp:Label ID="lblCuotaMensual" runat="server" Text="0"></asp:Label></p>
                                    <asp:Label ID="lblDebug" runat="server" CssClass="text-danger small"></asp:Label>
                                </asp:Panel>
                            </div>
                            <div class="row mt-3">
                                <asp:Panel ID="pnlDatosTarjeta" runat="server" Visible="false" CssClass="col-md-8">
                                    <div class="card border-info p-3">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label class="form-label fw-bold">Banco</label>
                                                <asp:DropDownList ID="ddlBanco" runat="server" CssClass="form-select">
                                                    <asp:ListItem Text="Seleccione un banco..." Value="0" />
                                                    <asp:ListItem Text="Banco Nación" Value="1" />
                                                    <asp:ListItem Text="Banco Provincia" Value="2" />
                                                    <asp:ListItem Text="BBVA" Value="3" />
                                                    <asp:ListItem Text="Santander" Value="4" />
                                                    <asp:ListItem Text="Itaú" Value="5" />
                                                    <asp:ListItem Text="Hipotecario" Value="6" />
                                                    <asp:ListItem Text="Credicoop" Value="7" />
                                                </asp:DropDownList>
                                                <asp:Label ID="lblErrorBanco" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="form-label fw-bold">Últimos 4 dígitos</label>
                                                <asp:TextBox ID="txtUltimos4Digitos" runat="server" CssClass="form-control" placeholder="Ej: 1234" MaxLength="4"></asp:TextBox>
                                                <small class="text-muted d-block mt-1">Ingrese los últimos 4 dígitos de la tarjeta</small>
                                                <asp:Label ID="lblErrorDigito" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-2" Visible="false" />

                        <div class="text-end">
                            <asp:Button ID="btnRegistrarVenta" runat="server" Text="Registrar Venta" CssClass="btn btn-success" OnClick="btnRegistrarVenta_Click" />
                        </div>
                </ContentTemplate>
                <Triggers>

                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelarSeleccion" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlMedioPago" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCuotas" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

    </div>
</asp:Content>
