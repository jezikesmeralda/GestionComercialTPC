<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="GestionComercialWeb.Productos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Productos</h2>
   <h3> Prueba de Marca </h3>
    <asp:GridView ID="dgvMarca" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre Marca" />
             <asp:BoundField DataField="Activo" HeaderText="Activo" />
        </Columns>
    </asp:GridView>
     <h3> Prueba de Categoria </h3>
  <asp:GridView ID="dgvCategoria" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
      <Columns>
         <asp:BoundField DataField="Nombre" HeaderText="Nombre Categoria" />
            <asp:BoundField DataField="Activo" HeaderText="Activo" />
      </Columns>
  </asp:GridView>
</asp:Content>
