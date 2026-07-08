<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProduForm.aspx.cs" Inherits="GestionComercialWeb.ProduForm" %>

<asp:Content ID="ProduForm1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card shadow-sm my-4">
        <div class="card-body p-4">
            <h2 class="mb-4">
                <asp:Label ID="lblTitulo" runat="server" Text="Nuevo Producto" /></h2>
            <asp:HiddenField ID="hdnId" runat="server" Value="0" />
            <div class="row g-4">
                <div class="col-lg-8">

                    <div class="row g-3 mb-3">
                        <div class="col-md-8">
                            <label for="txtNombre" class="form-label">Nombre</label>
                            <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label fw-semibold">Stock actual</label>
                            <div class="pt-2">
                                <asp:Label ID="lblStockActualInfo" runat="server" CssClass="badge bg-secondary fs-6" Visible="false" />
                            </div>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="txtDescripcion" class="form-label fw-semibold">Descripcion</label>
                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescripcion" CssClass="form-control" />
                    </div>
                    <div class="row g-3 mb-3">
                        <div class="col-md-6">
                            <label for="txtMarca" class="form-label fw-semibold">Marca</label>
                            <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label for="txtCategoria" class="form-label fw-semibold">Categorias</label>
                            <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row g-3 mb-3">
                        <div class="col-md-4">
                            <label for="txtPrecioCosto" class="form-label fw-semibold">Precio Costo</label>
                            <div class="input-group">
                                <span class="input-group-text">$</span>
                                <asp:TextBox runat="server" ID="txtPrecioCosto" CssClass="form-control text-end" placeholder="0.00" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label for="txtPorcetajeGanancia" class="form-label fw-semibold">Porcentaje Ganancia</label>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtPorcetajeGanancia" CssClass="form-control text-end" placeholder="0" />
                                <span class="input-group-text">%</span>
                            </div>
                        </div>
                        <div class="col-md-4 d-flex flex-column justify-content-end pb-2">
                            <span class="text-muted small">Precio de venta estimado: </span>
                            <span class="fw-bold text-success fs-5" id="lblPrecioVentaPreview">$0.00</span>
                        </div>
                    </div>
                    <div class="row g-3 mb-3">
                        <div class="col-md-6">
                            <label class="form-label">Stock Mínimo</label>
                            <asp:TextBox runat="server" ID="txtStockMinimo" CssClass="form-control" />
                            <div class="form-text">Se usa para avisar cuando el stock está bajo.</div>
                        </div>
                    </div>
                    <div class="mb-4">
                        <label for="<%= txtURLImagen.ClientID %>" class="form-label fw-semibold">URL Imagen</label>
                        <asp:TextBox runat="server" ID="txtURLImagen" CssClass="form-control" onkeyup="actualizarPreview()" onchange="actualizarPreview()" />
                    </div>
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />

                    <div class="mt-4 border-top pt-3">
                        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                        <a href="Productos.aspx" class="btn btn-outline-secondary px-4">Cancelar</a>
                    </div>
                </div>
                <div class="col-lg-4 d-flex flex-column align-items-center justify-content-start border-start ps-lg-4">
                    <label class="form-label fw-semibold mb-3">Vista previa</label>
                    <img id="imgPreview" src="https://placehold.co/300x200?text=Sin+Imagen" class="img-fluid rounded border shadow-sm"
                        style="max-width: 100%; max-height: 250px; object-fit: contain; background-color: #f8f9fa;" />
                </div>
            </div>
        </div>
    </div>
    <

    <script>
        function actualizarPreview() {
            var url = document.getElementById('<%= txtURLImagen.ClientID %>').value;
            var img = document.getElementById('imgPreview');
            img.src = (url && url.trim() !== '') ? url : 'https://placehold.co/300x200';
        }

       
    </script>

</asp:Content>
