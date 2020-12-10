<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Puesto.aspx.cs" Inherits="AppReservasSW.Views.Puesto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    </div>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>

    <br />
     <table style="width:100%; height: 82px;">
        <tr>
            <td class="auto-style6">
                Puesto ID</td>
            <td class="auto-style3">
                <asp:TextBox ID="txt_puesto_ID" runat="server"></asp:TextBox>
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
