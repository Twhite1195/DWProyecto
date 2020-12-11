<%@ Page Async="true" Title="Empleado" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empleado.aspx.cs" Inherits="AppReservasSW.Views.Empleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    </div>
    <asp:GridView ID="grdEmpleados" runat="server">
    </asp:GridView>
 

    <table style="width:100%; height: 82px;">

         <tr>
            <td class="auto-style6">
                Empleado ID</td>
            <td class="auto-style3">
                <asp:TextBox ID="txt_empleado_id" runat="server" OnTextChanged="txtID_TextChanged"></asp:TextBox>
            </td>
        </tr>
       
         <tr>
            <td class="auto-style6">
                Nombre</td>
            <td class="auto-style3">
                <asp:TextBox ID="txt_nombre" runat="server" OnTextChanged="txtID_TextChanged"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="auto-style6">
                Apellido</td>
            <td class="auto-style3">
                <asp:TextBox ID="txt_apellido" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="auto-style6">
                Teléfono</td>
            <td class="auto-style3">
                <asp:TextBox ID="txt_telefono" runat="server"></asp:TextBox>
            </td>
        </tr>

          <tr>
            <td class="auto-style1">
                Cédula</td>
            <td class="auto-style1">
                <asp:TextBox ID="txt_cedula" runat="server"></asp:TextBox>
            </td>
        </tr>

               <tr>
            <td class="auto-style6">
                Residencia</td>
            <td class="auto-style3">
                <asp:TextBox ID="txt_residencia" runat="server"></asp:TextBox>
            </td>
        </tr>

            <tr>
            <td class="auto-style6">
                <asp:Label ID="Label2" runat="server" Text="Sede"></asp:Label>
            </td>
            <td class="auto-style3">
                <asp:DropDownList ID="drpSede" runat="server">
                   <asp:ListItem Selected="True" Value="1">HQ</asp:ListItem>
                    <asp:ListItem Value="3">Ghetto</asp:ListItem>
                    <asp:ListItem Value="2">Paradise</asp:ListItem>
                    <asp:ListItem Value="1">HQ</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style3"></td>
        </tr>

        <tr>
            <td class="auto-style6">
                <asp:Label ID="Label1" runat="server" Text="Puesto"></asp:Label>
            </td>
            <td class="auto-style3">
                <asp:DropDownList ID="drpPuesto" runat="server">
                   <asp:ListItem Selected="True" Value="1">Administrador</asp:ListItem>
                    <asp:ListItem Value="4">Jefe de supervisores</asp:ListItem>
                    <asp:ListItem Value="2">Gerente</asp:ListItem>
                    <asp:ListItem Value="1">Administrador</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style3"></td>
        </tr>

        
            <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnIngresar_Click"  />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
            </td>

            <td>
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>

          <%--  Boton Eliminar--%>
        <tr>
            <td class="modal-sm" style="width: 190px; height: 21px;">
                <asp:Label ID="lblStatus" runat="server" Text="Label" ForeColor="#006600" Visible="False"></asp:Label>
            </td>
            <td style="height: 21px"></td>
            <td style="height: 21px"></td>
        </tr>

    </table>
</asp:Content>
