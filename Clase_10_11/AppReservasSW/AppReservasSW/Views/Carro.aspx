<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carro.aspx.cs" Inherits="AppReservasSW.Views.Carro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 85px;
        }
        .auto-style2 {
            width: 85px;
            height: 22px;
        }
        .auto-style3 {
            height: 22px;
        }
        .auto-style4 {
            width: 279px;
        }
        .auto-style5 {
            height: 22px;
            width: 279px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grdCarros" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowPaging="True">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>

    <table style="width:100%;">
        <%--  ID CARRO  --%>
         <tr>
            <td class="auto-style6">
                <asp:Label ID="Label1" runat="server" Text="ID Carro"></asp:Label>
            </td>
            <td class="auto-style3">
                <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style3"></td>
        </tr>
        <%--  Modelo ID --%>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="Label2" runat="server" Text="Modelo"></asp:Label>
            </td>
            <td class="auto-style3">
                <asp:TextBox ID="txtModeloID" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style3"></td>
        </tr>

        <%--  Estado --%>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="Label3" runat="server" Text="Estado"></asp:Label>
            </td>
            <td class="auto-style3">
                <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style3"></td>
        </tr>

          <%--  Placa --%>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="Label4" runat="server" Text="Placa"></asp:Label>
            </td>

            <td class="auto-style3">
                <asp:TextBox ID="txtPlaca" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style3"></td>
        </tr>

        <%--  Sede --%>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="Label5" runat="server" Text="Sede"></asp:Label>
            </td>

            <td class="auto-style3">
                <asp:TextBox ID="txtSede" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style3"></td>
        </tr>
        <%--  Lote --%>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="Label6" runat="server" Text="Lote"></asp:Label>
            </td>

            <td class="auto-style3">
                <asp:TextBox ID="txtLote" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style3"></td>
        </tr>

        <%--  Reservacion --%>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="Label7" runat="server" Text="Reservacion"></asp:Label>
            </td>

            <td class="auto-style3">
                <asp:TextBox ID="txtRes" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style3"></td>
        </tr>

        <%--  Botones Agregar y Modificar--%>

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


        <tr>
            <td class="modal-sm" style="width: 190px">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
