<%@ Page Title="New" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="TranslationSys.New" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <asp:Table ID="ChoiceTable" runat="server" HorizontalAlign="Center" CellPadding="2" CellSpacing="5">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Right">請選擇文件類型 :</asp:TableCell>
                <asp:TableCell runat="server" ColumnSpan="2">
                    <asp:DropDownList ID="TableDDL" runat="server" OnSelectedIndexChanged="TableDDL_SelectedIndexChanged" AutoPostBack="True">

                        <asp:ListItem Value="Z">公開招標</asp:ListItem>

                        <asp:ListItem Value="I">會議文件</asp:ListItem>

                        <asp:ListItem Value="P">建議書</asp:ListItem>

                        <asp:ListItem Value="CI">內部運作文件</asp:ListItem>

                        <asp:ListItem Value="G">公函</asp:ListItem>

                        <asp:ListItem Value="N">新聞稿</asp:ListItem>
                    </asp:DropDownList>

                </asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Right">請按內容分類 :</asp:TableCell>
                <asp:TableCell runat="server" ColumnSpan="2">
                    <asp:DropDownList ID="TypeDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TypeDDL_SelectedIndexChanged">
                        <asp:ListItem>公告</asp:ListItem>
                        <asp:ListItem>招標方案</asp:ListItem>

                        <asp:ListItem>承投規則</asp:ListItem>

                        <asp:ListItem>技術規範</asp:ListItem>
                    </asp:DropDownList>

                </asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" Height="50px" TableSection="TableFooter" VerticalAlign="Bottom">
                <asp:TableCell runat="server" ColumnSpan="4" HorizontalAlign="Right">
                    <asp:Button ID="NextPage" runat="server" Text="下一頁" OnClick="NextPage_Click" Enabled="true"  class="btn btn-default"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
