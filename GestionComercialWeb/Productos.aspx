<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="GestionComercialWeb.Productos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Productos</h2>

    <div class="card shadow-sm mb-4">
        <div class="card-body">
            <div class="row align-items-end">
                <div class="col-md-4">
                    <label class="form-label">Buscar Producto</label>
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Ingrese nombre del producto"></asp:TextBox>
                </div>

                <div class="col-md-2">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" OnClick="btnBuscar_Click"/>
                </div>

                <div class="col-md-2">
                    <label class="form-label">Marca</label>
                    <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Todas" Value="0" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label class="form-label">Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Todas" Value="0" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label class="form-label">Stock</label>
                    <asp:DropDownList ID="ddlStock" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Todos" Value="0" />
                        <asp:ListItem Text="Con Stock" Value="1" />
                        <asp:ListItem Text="Stock Bajo" Value="2" />
                        <asp:ListItem Text="Sin Stock" Value="3" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>

    <hr />
    <div class="col-md-2">
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Producto" CssClass="btn btn-success w-100" PostBackUrl="~/ProduForm.aspx" />
    </div>
    <hr />

    <div class="row row-cols-1 row-cols-md-3 g-4">
        <% foreach (Dominio.Producto produ in ListaProductos) { %>
            <div class="col">
                <div class="card h-100">
                    <img src='<%= string.IsNullOrEmpty(produ.ImagenUrl) ? "https://placehold.co/300x200" 
                        : (produ.ImagenUrl.StartsWith("http") ? produ.ImagenUrl : ResolveUrl("~/" + produ.ImagenUrl)) %>' 
                        class="card-img-top" alt="..." style="height: 200px; object-fit: contain; background-color: white;">
                    <div class="card-body">
                        <h5 class="card-title"><%: produ.NombreProducto %></h5>
                        <p class="card-text"><%: produ.Descripcion %></p>
                        <p><strong>Stock: </strong><%: produ.StockActual %> unidades</p>
                    </div>
                </div>
            </div>
        <% } %>
    </div>

</asp:Content>