<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true"
CodeBehind="Inicio.aspx.cs"
Inherits="GestionComercialWeb.Inicio" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="text-center mb-5">

        <h1 class="display-4 fw-bold">COMERCIO</h1>

        <p class="subtitulo">
            Sistema de Gestión Comercial
        </p>

    </div>

    <div class="row text-center mb-5">

        <div class="col-md-3 mb-3">
            <a href="Productos.aspx" class="btn btn-dark w-100 p-3">
                Productos
            </a>
        </div>

        <div class="col-md-3 mb-3">
            <a href="Clientes.aspx" class="btn btn-dark w-100 p-3">
                Clientes
            </a>
        </div>

        <div class="col-md-3 mb-3">
            <a href="Compras.aspx" class="btn btn-dark w-100 p-3">
                Compras
            </a>
        </div>

        <div class="col-md-3 mb-3">
            <a href="Ventas.aspx" class="btn btn-dark w-100 p-3">
                Ventas
            </a>
        </div>

    </div>

    <div class="row">

        <div class="col-md-4 mb-3">

            <div class="card shadow-sm">
                <div class="card-body text-center">

                    <h5 class="card-title">
                        Productos Activos
                    </h5>

                    <h2>
                        <asp:Label ID="lblProductos" runat="server" ></asp:Label>
                    </h2>

                </div>
            </div>

        </div>

        <div class="col-md-4 mb-3">

            <div class="card shadow-sm">
                <div class="card-body text-center">

                    <h5 class="card-title">
                        Clientes
                    </h5>

                    <h2>
                        <asp:Label ID="lblClientes" runat="server" ></asp:Label>
                    </h2>

                </div>
            </div>

        </div>

        <div class="col-md-4 mb-3">

            <div class="card shadow-sm">
                <div class="card-body text-center">

                    <h5 class="card-title">
                        Stock Bajo
                    </h5>

                    <h2>
                        <asp:Label ID="lblStockBajo" runat="server" ></asp:Label>
                    </h2>

                </div>
            </div>

        </div>

    </div>

</asp:Content>