<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="GestionComercialWeb.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Categorías</h2>

    <div class="mb-3 mt-4">
        <asp:Button ID="btnCategoria" runat="server" Text="Nueva Categoria" CssClass="btn btn-success" OnClick="btnNuevaCategoria_Click" />
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <h5 class="mb-3">Listado de Categorías</h5>

            <asp:GridView ID="gvCategorias" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" OnRowCommand="gvCategorias_RowCommand" OnRowDataBound="gvCategorias_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="Id" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="d-flex gap-2">
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-outline-primary btn-sm me-1" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Seguro que querés eliminar esta categoría?');" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>