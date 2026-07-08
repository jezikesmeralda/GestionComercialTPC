<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="GestionComercialWeb.Productos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Productos</h2>
     <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
        <asp:Literal ID="litMensaje" runat="server" />
    </asp:Panel>
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
                        class="card-img-top" alt="..." style="height: 200px; object-fit: contain; background-color: white;"/>
                    <div class="card-body">
                        <h5 class="card-title"><%: produ.NombreProducto %></h5>
                        <p class="card-text"><%: produ.Descripcion %></p>
                        <p class="fs-5 text-success fw-bold">$<%: produ.PrecioVenta.ToString("N2") %></p>
                        <p><strong>Stock: </strong><%: produ.StockActual %> unidades</p>
                        <% if (UsuarioActual.Rol == Dominio.Rol.Administrador) { %>
                        <a href='<%= "ProduForm.aspx?id=" + produ.Id %>' class="btn btn-outline-primary btn-sm">Editar</a>
                         <a href="javascript:void(0);" class="btn btn-outline-danger btn-sm"
                           onclick="confirmarEliminar('<%: produ.Id %>')">
                           Eliminar
                        </a>
                        <% } %>
                    </div>
                </div>
            </div>
        <% } %>
    </div>
    <div class="mb-3 mt-4">
        <asp:Button ID="btnVerInactivos" runat="server" Text="Ver Productos Inactivos" CssClass="btn btn-secondary" OnClick="btnVerInactivos_Click" />
    </div>

    <asp:Panel ID="pnlInactivos" runat="server" Visible="false" CssClass="card shadow-sm">
        <div class="card-body">
            <h5 class="mb-3">Productos Inactivos</h5>

            <div class="row row-cols-1 row-cols-md-3 g-4">
                <% foreach (Dominio.Producto produ in ListaProductosInactivos)
                    { %>
                <div class="col">
                    <div class="card h-100 border-secondary">
                        <img src='<%= string.IsNullOrEmpty(produ.ImagenUrl) ? "https://placehold.co/300x200" 
                                : (produ.ImagenUrl.StartsWith("http") ? produ.ImagenUrl : ResolveUrl("~/" + produ.ImagenUrl)) %>'
                            class="card-img-top opacity-75" alt="..." style="height: 200px; object-fit: contain; background-color: white;" />
                        <div class="card-body">
                            <h5 class="card-title"><%: produ.NombreProducto %></h5>
                            <p class="card-text"><%: produ.Descripcion %></p>
                            <p class="fs-5 text-muted fw-bold">$<%: produ.PrecioVenta.ToString("N2") %></p>
                            <p><strong>Stock: </strong><%: produ.StockActual %> unidades</p>
                            <% if (UsuarioActual.Rol == Dominio.Rol.Administrador) { %>
                            <a href="javascript:void(0);" class="btn btn-outline-success btn-sm"
                                onclick="confirmarReactivar('<%: produ.Id %>')">Reactivar
                                </a>
                             <% } %>
                        </div>
                    </div>
                </div>
                <% } %>
            </div>
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
                    ¿Seguro que desea dar de baja este producto?
               
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <a id="linkConfirmarEliminar" href="#" class="btn btn-danger">Eliminar</a>
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
                    ¿Seguro que desea reactivar este producto?
               
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <a id="linkConfirmarReactivar" href="#" class="btn btn-success">Reactivar</a>
                </div>
            </div>
        </div>
    </div>

    <script>
        function confirmarEliminar(id) {
            document.getElementById('linkConfirmarEliminar').href = 'Productos.aspx?eliminar=' + id;
            new bootstrap.Modal(document.getElementById('modalEliminar')).show();
        }

        function confirmarReactivar(id) {
            document.getElementById('linkConfirmarReactivar').href = 'Productos.aspx?reactivar=' + id + '&verInactivos=1';
            new bootstrap.Modal(document.getElementById('modalReactivar')).show();
        }
    </script>
</asp:Content>
