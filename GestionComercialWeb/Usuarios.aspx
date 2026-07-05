<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="GestionComercialWeb.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Usuarios</h2>

    <div class="mb-3 mt-4">
        <asp:Button ID="btnNuevoUsuario" runat="server" Text="Nuevo Usuario" CssClass="btn btn-success" OnClick="btnNuevoUsuario_Click" />
    </div>

    <asp:Label ID="lblMensaje" runat="server" Visible="false" />

    <div class="card shadow-sm">
        <div class="card-body">
            <h5 class="mb-3">Listado de Usuarios</h5>

                    <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" OnRowCommand="gvUsuarios_RowCommand" OnRowDataBound="gvUsuarios_RowDataBound" DataKeyNames="Id">                <Columns>
                    <asp:BoundField HeaderText="Usuario" DataField="UserName" />
                    <asp:BoundField HeaderText="Email" DataField="Email" />
                    <asp:BoundField HeaderText="Rol" DataField="RolNombre" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="d-flex gap-2">
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-outline-primary btn-sm" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Seguro que querés eliminar este usuario?');" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>