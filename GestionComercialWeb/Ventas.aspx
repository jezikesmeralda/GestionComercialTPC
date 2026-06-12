<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="GestionComercialWeb.Ventas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2 class="mb-4">Ventas</h2>

    <div class="card shadow-sm mb-4">
        <div class="card-body">
    <div class="row mb-3">
        <div class="col-md-4">
            <label class="form-label">Cliente</label>
            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>
    </div>

    <hr />

    <div class="row align-items-end mb-4">
        <div class="col-md-5">
            <label class="form-label">Buscar Producto</label>
            <asp:TextBox ID="txtBuscarProducto" runat="server" CssClass="form-control" placeholder="Ingrese nombre del producto"></asp:TextBox>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" />
        </div>
    </div>

    <div class="alert alert-secondary mb-4">
        <strong>Producto encontrado:</strong>
        <asp:Label ID="lblProductoEncontrado" runat="server" Text="Ninguno"></asp:Label>
    </div>

    <div class="row align-items-end mb-3">
        <div class="col-md-3">
            <label class="form-label">Cantidad</label>
            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success w-100" />
        </div>
    </div>
</div>

    </div>

    <div class="card shadow-sm">

        <div class="card-body">

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

            <div class="text-end">

                <asp:Button ID="btnRegistrarVenta" runat="server" Text="Registrar Venta" CssClass="btn btn-success" />

            </div>

        </div>

    </div>

</asp:Content>

