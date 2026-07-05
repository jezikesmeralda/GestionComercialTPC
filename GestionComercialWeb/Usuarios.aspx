<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="GestionComercialWeb.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Usuarios</h2>
     <asp:HiddenField ID="hfIdEliminar" runat="server" />
    <asp:HiddenField ID="hfIdReactivar" runat="server" />
    <div class="mb-3 mt-4">
        <asp:Button ID="btnNuevoUsuario" runat="server" Text="Nuevo Usuario" CssClass="btn btn-success" OnClick="btnNuevoUsuario_Click" />
    </div>

     <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
        <asp:Literal ID="litMensaje" runat="server" />
    </asp:Panel>


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
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
     <div class="mb-3 mt-3">
        <asp:Button ID="btnVerInactivos" runat="server" Text="Ver Usuarios Inactivos" CssClass="btn btn-secondary" OnClick="btnVerInactivos_Click" />
    </div>

    <asp:Panel ID="pnlInactivos" runat="server" Visible="false" CssClass="card shadow-sm">
        <div class="card-body">
            <h5 class="mb-3">Usuarios Inactivos</h5>
            <asp:GridView ID="gvInactivos" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" OnRowCommand="gvInactivos_RowCommand" DataKeyNames="Id">
         <Columns>
             <asp:BoundField HeaderText="Usuario" DataField="UserName" />
             <asp:BoundField HeaderText="Email" DataField="Email" />
             <asp:BoundField HeaderText="Rol" DataField="RolNombre" />
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
                    ¿Seguro que desea eliminar este usuario?
               
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
                    ¿Seguro que desea reactivar este usuario?
               
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarReactivar" runat="server" Text="Reactivar" CssClass="btn btn-success" OnClick="btnConfirmarReactivar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
