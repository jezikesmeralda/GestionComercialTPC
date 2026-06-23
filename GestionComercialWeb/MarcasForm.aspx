<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarcasForm.aspx.cs" Inherits="GestionComercialWeb.MarcasForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="card shadow-sm">

        <div class="card-body">

            <h3>Marca</h3>

            <div class="mb-3">

                <label>Nombre</label>

                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>

            </div>

            <asp:HiddenField ID="hfId" runat="server" />

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />

            <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-secondary ms-2" PostBackUrl="~/Marcas.aspx" />

        </div>

    </div>
</asp:Content>
