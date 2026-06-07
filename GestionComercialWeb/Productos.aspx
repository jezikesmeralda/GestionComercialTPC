<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="GestionComercialWeb.Productos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Productos</h2>

    <div class="row row-cols-1 row-cols-md-3 g-4">
        <% foreach (Dominio.Producto produ in ListaProductos) { %>
            <div class="col">
                <div class="card h-100">
                    <img src="https://placehold.co/300x200" class="card-img-top" alt="...">
                    <div class="card-body">
                        <h5 class="card-title"><%: produ.NombreProducto %></h5>
                        <p class="card-text"><%: produ.Descripcion %></p>
                    </div>
                </div>
            </div>
        <% } %>
    </div>

    <h3 class="mt-4">Marcas</h3>
    <asp:GridView ID="dgvMarca" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre Marca" />
            <asp:BoundField DataField="Activo" HeaderText="Activo" />
        </Columns>
    </asp:GridView>

    <h3 class="mt-4">Categorías</h3>
    <asp:GridView ID="dgvCategoria" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre Categoría" />
            <asp:BoundField DataField="Activo" HeaderText="Activo" />
        </Columns>
    </asp:GridView>
</asp:Content>