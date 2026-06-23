<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="GestionComercialWeb.Clientes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
 <h2>Clientes</h2>
    <div class="mb-3 mt-4">
        <asp:Button ID="btnNuevoCliente" runat="server" Text="Nuevo Cliente" CssClass="btn btn-success" OnClick="btnNuevoCliente_Click" />
    </div>

  <div class="card shadow-sm">
      <div class="card-body">
          <h5 class="mb-3">Listado de Clientes</h5>

          <asp:GridView ID="gvClientes" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" OnRowCommand="gvClientes_RowCommand">
              <Columns>

                  <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                  <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                  <asp:BoundField HeaderText="DNI" DataField="Dni" />
                  <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                  <asp:BoundField HeaderText="Email" DataField="Email" />
                  <asp:BoundField HeaderText="Dirección" DataField="Direccion" />

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
