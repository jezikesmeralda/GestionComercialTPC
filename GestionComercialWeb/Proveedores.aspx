<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="GestionComercialWeb.Proveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Proveedores</h2>

    <div class="card shadow-sm mb-4">
        <div class="card-body">

            <h5 class="mb-3">Nuevo Proveedor</h5>

            <div class="row">

                <div class="col-md-4 mb-3">
                    <label class="form-label">Nombre</label>

                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-4 mb-3">
                    <label class="form-label">Teléfono</label>

                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                        
                </div>

                <div class="col-md-4 mb-3">
                    <label class="form-label">Email</label>

                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                </div>

            </div>

            <div class="mt-3">

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success me-2" OnClick="btnGuardar_Click" />

                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />

            </div>

        </div>
    </div>

    <div class="card shadow-sm">

        <div class="card-body">

            <h5 class="mb-3">Listado de Proveedores</h5>

            <asp:GridView ID="gvProveedores" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" OnRowCommand="gvProveedores_RowCommand">

                <Columns>

                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />

                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />

                    <asp:BoundField HeaderText="Email" DataField="Email" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-outline-primary btn-sm me-1" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>

