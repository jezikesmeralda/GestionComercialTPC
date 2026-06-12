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

          <div class="mt-3">
              <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success me-2" />
              <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />
          </div>

      </div>

  </div>

  <div class="card shadow-sm">
      <div class="card-body">
          <h5 class="mb-3">Listado de Clientes</h5>

      <asp:GridView ID="gvClientes" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false">
          <Columns>

              <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
              <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
              <asp:BoundField HeaderText="DNI" DataField="Dni" />
           </Columns>
      </asp:GridView>
      </div>

  </div>
</asp:Content>
