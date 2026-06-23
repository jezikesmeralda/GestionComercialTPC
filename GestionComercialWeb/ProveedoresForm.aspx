<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProveedoresForm.aspx.cs" Inherits="GestionComercialWeb.ProveedoresForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        

        <asp:Literal ID="litMensaje" runat="server"></asp:Literal>

        <div class="card shadow-sm">
            <div class="card-body">
                <h3 class="mb-4">Datos del Proveedor</h3>

                <asp:HiddenField ID="hfId" runat="server" />

                <div class="row">

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Nombre / Razón Social</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                            ControlToValidate="txtNombre" 
                            ErrorMessage="El nombre es obligatorio." 
                            CssClass="text-danger small d-block mt-1" Display="Dynamic" />
                    </div>


                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" 
                            ControlToValidate="txtTelefono" ErrorMessage="El teléfono es obligatorio." 
                            CssClass="text-danger small d-block mt-1" Display="Dynamic" />
                        
                        <asp:RegularExpressionValidator ID="revTelefono" runat="server" 
                            ControlToValidate="txtTelefono" 
                            ValidationExpression="^\d+$" 
                            ErrorMessage="Ingrese solo números para el teléfono." 
                            CssClass="text-danger small d-block mt-1" Display="Dynamic" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                            ControlToValidate="txtEmail" ErrorMessage="El email es obligatorio." 
                            CssClass="text-danger small d-block mt-1" Display="Dynamic" />
                        
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" 
                            ControlToValidate="txtEmail" 
                            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" 
                            ErrorMessage="Formato de correo electrónico inválido." 
                            CssClass="text-danger small d-block mt-1" Display="Dynamic" />
                    </div>
                </div>

                <div class="mt-3">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-secondary ms-2" PostBackUrl="~/Proveedores.aspx" CausesValidation="false" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>


