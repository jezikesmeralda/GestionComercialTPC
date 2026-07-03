<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="GestionComercialWeb.Ventas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="mb-0">Ventas</h2>
        <asp:Button ID="btnHistorial" runat="server" Text="Historial" CssClass="btn btn-outline-primary" PostBackUrl="~/HistorialVentas.aspx" Visible="false" />
    </div>

    <div class="card shadow-sm mb-4">
        <div class="card-body">
            <div class="row mb-3">
                <div class="col-md-4">
                    <label class="form-label">Cliente</label>
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select"></asp:DropDownList>
                    <asp:Label ID="lblErrorCliente" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                </div>
            </div>
            </div>
            <hr />
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upVenta">
                <ProgressTemplate>
                    <div class="text-center text-muted small mt-1 mb-2">
                        <span class="spinner-border spinner-border-sm" role="status"></span>
                        Procesando...
       
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
          
            <asp:UpdatePanel ID="upVenta" runat="server">
                <ContentTemplate>
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
                <h4>Total: $<asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label></h4>
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
          </Triggers>
            </asp:UpdatePanel>
        </div>
    
    </div>
</asp:Content>