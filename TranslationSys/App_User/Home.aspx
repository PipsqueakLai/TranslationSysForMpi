<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TranslationSys.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header page-content">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="">
            <SelectParameters>
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Font-Size="20px" Width="1134px" class="table table-striped" HorizontalAlign="Center" OnRowDataBound="GridView1_RowDataBound" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:TemplateField HeaderText="Title" SortExpression="Title">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("Title") %>' OnClick="LinkButteon1_Click" CommandArgument='<%# Bind("FileName") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" SortExpression="Type">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Type") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Button" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><p class="text-center">No Record Available</p></EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
