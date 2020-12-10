<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empleado.aspx.cs" Inherits="AppReservasSW.Views.Empleado" %>
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
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <br />
    <br />

    <table style="width:100%; height: 82px;">


         <tr>
            <td class="auto-style6">
                Empleado ID</td>
            <td class="auto-style3">
                <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="txtID_TextChanged"></asp:TextBox>
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
                Sede</td>
            <td class="auto-style3">
                <asp:TextBox ID="txt_sede" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="auto-style6">
                Puesto</td>
            <td class="auto-style3">
                <asp:TextBox ID="txt_puesto" runat="server"></asp:TextBox>
            </td>
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
    </table>
</asp:Content>
