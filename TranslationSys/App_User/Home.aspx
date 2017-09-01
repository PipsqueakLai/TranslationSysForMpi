<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TranslationSys.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header page-content">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Title], [Date], [Status] FROM [DataTable] WHERE ([UserId] = @UserId)">
            <SelectParameters>
                <asp:Parameter Name="UserId" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Font-Size="20px" Width="1056px" class="table table-striped">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title">
                <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                    <HeaderStyle HorizontalAlign="Center" Width="0px" />
                    <ItemStyle HorizontalAlign="Center" Width="0px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Button" />
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
