<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="GestionComercialWeb.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registrarse - Vinoteca</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .registro-card {
            width: 100%;
            max-width: 400px;
            padding: 40px;
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.1);
        }
        .registro-title {
            text-align: center;
            font-size: 28px;
            font-weight: bold;
            margin-bottom: 8px;
        }
        .registro-subtitle {
            text-align: center;
            color: #6c757d;
            margin-bottom: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="registro-card">
            <div class="registro-title">Vinoteca</div>
            <div class="registro-subtitle">Crear cuenta</div>

            <div class="mb-3">
                <label class="form-label">Usuario</label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Ingrese un nombre de usuario" />
            </div>

            <div class="mb-3">
                <label class="form-label">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese una contraseña" />
            </div>

            <div class="mb-3">
                <label class="form-label">Confirmar Contraseña</label>
                <asp:TextBox ID="txtConfirmarPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Repita la contraseña" />
            </div>

            <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />

            <asp:Button ID="btnRegistrar" runat="server" Text="Crear cuenta" CssClass="btn btn-dark w-100 mb-2" OnClick="btnRegistrar_Click" />
            <a href="Login.aspx" class="btn btn-outline-dark w-100">Volver al login</a>
        </div>
    </form>
</body>
</html>