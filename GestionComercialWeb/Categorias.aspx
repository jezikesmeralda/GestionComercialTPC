<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="GestionComercialWeb.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Categorías</h2>
     <asp:HiddenField ID="hfIdEliminar" runat="server" />
    <asp:HiddenField ID="hfIdReactivar" runat="server" />
    <div class="mb-3 mt-4">
        <asp:Button ID="btnCategoria" runat="server" Text="Nueva Categoria" CssClass="btn btn-success" OnClick="btnNuevaCategoria_Click" />
    </div>
    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
        <asp:Literal ID="litMensaje" runat="server" />
    </asp:Panel>
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
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="mb-3 mt-3">
        <asp:Button ID="btnVerInactivos" runat="server" Text="Ver Categorías Inactivas" CssClass="btn btn-secondary" OnClick="btnVerInactivos_Click" />
    </div>

    <asp:Panel ID="pnlInactivos" runat="server" Visible="false" CssClass="card shadow-sm">
        <div class="card-body">
            <h5 class="mb-3">Categorías Inactivas</h5>
            <asp:GridView ID="gvInactivos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" OnRowCommand="gvInactivos_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="Id" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnReactivar" runat="server" Text="Reactivar" CssClass="btn btn-outline-success btn-sm" CommandName="Reactivar" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>


    <div class="modal fade" id="modalEliminar" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">Confirmar eliminación</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    ¿Seguro que desea eliminar esta categoría?
               
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modalReactivar" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title">Confirmar reactivación</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    ¿Seguro que desea reactivar esta categoría?
               
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarReactivar" runat="server" Text="Reactivar" CssClass="btn btn-success" OnClick="btnConfirmarReactivar_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
