<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="GestionComercialWeb.Clientes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
 <h2>Clientes</h2>
   <div class="card shadow-sm mb-4">
      <div class="card-body">
         <h5 class="mb-3">Nuevo Cliente</h5>
          <div class="row">
              <div class="col-md-6 mb-3">
                  <label class="form-label">Nombre</label>

                  <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
              </div>

              <div class="col-md-6 mb-3">
                  <label class="form-label">Apellido</label>

                  <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

          </div>

          <div class="row">
              <div class="col-md-4 mb-3">
                  <label class="form-label">DNI</label>

                  <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" ></asp:TextBox>
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
          <div class="row">
    <div class="col-md-12 mb-3">
        <label class="form-label">Dirección</label>

        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle y número"></asp:TextBox>
    </div>
</div>

          <div class="mt-3">
              <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success me-2" OnClick="btnGuardar_Click"/>
              <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />
          </div>

      </div>

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
                          <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-outline-primary btn-sm me-1" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />

                          <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' />

                      </ItemTemplate>
                  </asp:TemplateField>
              </Columns>
          </asp:GridView>
      </div>

  </div>
</asp:Content>
