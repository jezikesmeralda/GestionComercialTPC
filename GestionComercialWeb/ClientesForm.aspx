<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientesForm.aspx.cs" Inherits="GestionComercialWeb.ClientesForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm">
            <div class="card-body">
                <h3 class="mb-4">Datos del Cliente</h3>

                <asp:HiddenField ID="hfId" runat="server" />

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
                        <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="valDni" runat="server"
                            ControlToValidate="txtDni"
                            ValidationExpression="^\d{8}$"
                            ValidateEmptyText="false"
                            ErrorMessage="El DNI debe tener exactamente 8 dígitos."
                            CssClass="text-danger"
                            Display="Dynamic" />
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="valTelefono" runat="server"
                            ControlToValidate="txtTelefono"
                            ValidationExpression="^\d{10}$"
                            ValidateEmptyText="false"
                            ErrorMessage="El teléfono debe tener exactamente 10 dígitos."
                            CssClass="text-danger"
                            Display="Dynamic" />
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                    </div>
                </div>

                <div class="row text-start">
                    <div class="col-md-12 mb-4">
                        <label class="form-label">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle, número, localidad"></asp:TextBox>
                    </div>
                </div>

                <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />

                <div class="mt-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-secondary ms-2" CausesValidation="False" PostBackUrl="~/Clientes.aspx" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>