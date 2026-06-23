<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="GestionComercialWeb.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Marcas</h2>

    <div class="mb-3 mt-4">
    <asp:Button ID="btnNuevaMarca" runat="server" Text="Nueva Marca" CssClass="btn btn-success" OnClick="btnNuevaMarca_Click" />
</div>

    <div class="card shadow-sm">
        <div class="card-body">

            <h5 class="mb-3">Listado de Marcas</h5>

            <asp:GridView ID="gvMarcas" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" OnRowCommand="gvMarcas_RowCommand">

                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="Id" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="d-flex gap-2">
                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-outline-primary btn-sm me-1" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />

                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' />
                       </div>
                                </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>

        </div>
    </div>

</asp:Content>

