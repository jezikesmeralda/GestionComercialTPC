<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuariosForm.aspx.cs" Inherits="GestionComercialWeb.UsuariosForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><asp:Label ID="lblTitulo" runat="server" Text="Nuevo Usuario" /></h2>

    <div class="card shadow-sm mt-4">
        <div class="card-body">

            <asp:HiddenField ID="hdnId" runat="server" Value="0" />

            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label">Nombre de usuario</label>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Rol</label>
                    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Vendedor" Value="0" />
                        <asp:ListItem Text="Administrador" Value="1" />
                    </asp:DropDownList>
                </div>
            </div>

            <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />

            <div class="mt-2">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                <a href="Usuarios.aspx" class="btn btn-secondary ms-2">Cancelar</a>
            </div>

        </div>
    </div>

</asp:Content>