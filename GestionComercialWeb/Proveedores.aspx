<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="GestionComercialWeb.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Proveedores</h2>
    <div class="mb-3 mt-4">
        <asp:Button ID="btnNuevoProveedor" runat="server" Text="Nuevo Proveedor" CssClass="btn btn-success" OnClick="btnNuevoProveedor_Click" />
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <h5 class="mb-3">Listado de Proveedores</h5>
            <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-2" Visible="false" />
            <asp:GridView ID="gvProveedores" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" OnRowCommand="gvProveedores_RowCommand" OnRowDataBound="gvProveedores_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                    <asp:BoundField HeaderText="Email" DataField="Email" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="d-flex gap-2">
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-outline-primary btn-sm me-1" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Seguro que querés eliminar este proveedor?');" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>