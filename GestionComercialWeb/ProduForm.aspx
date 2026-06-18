<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProduForm.aspx.cs" Inherits="GestionComercialWeb.ProduForm" %>

<asp:Content ID="ProduForm1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-6">
            <%-- <div class="mb-3">
                <label for="txtID" class="form-label">ID</label>
                <asp:TextBox runat="server" ID="txtID" CssClass="form-control" />
            </div> --%>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripcion</label>
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescripcion" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtMarca" class="form-label">Marca</label>
                <asp:DropDownList ID="ddlMarca" cssclass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="txtCategoria" class="form-label">Categorias</label>
                <asp:DropDownList ID="ddlCategoria" cssclass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="txtPrecioCosto" class="form-label">Precio Costo</label>
                <asp:TextBox runat="server" ID="txtPrecioCosto" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtPorcetajeGanancia" class="form-label">Porcentaje Ganancia</label>
                <asp:TextBox runat="server" ID="txtPorcetajeGanancia" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtURLImagen" class="form-label">URL Imagen</label>
                <asp:TextBox runat="server" ID="txtURLImagen" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtStockMinimo" class="form-label">Stock Minimo</label>
                <asp:TextBox runat="server" ID="txtStockMinimo" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtStockActual" class="form-label">Stock Actual</label>
                <asp:TextBox runat="server" ID="txtStockActual" CssClass="form-control" />
            </div>
            <div class="mb-3">            
                <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                <a href="Productos.aspx" class="btn btn-secondary">Cancelar</a>
            </div>   
        </div>
    </div>

</asp:Content>