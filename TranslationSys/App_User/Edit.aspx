<%@ Page Title="Edit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="TranslationSys.Edit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <table style="width: 1022px; margin: 40px auto;">
                <tr>
                    <td class="">部門/機構：</td>
                    <td class="">
                        <asp:DropDownList ID="SchoolChoice" runat="server" OnSelectedIndexChanged="SchoolChoice_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem>公共行政高等學校</asp:ListItem>
                            <asp:ListItem>藝術高等學校</asp:ListItem>
                            <asp:ListItem>管理科學高等學校</asp:ListItem>
                            <asp:ListItem>高等衛生學校</asp:ListItem>
                            <asp:ListItem>語言暨翻譯高等學校</asp:ListItem>
                            <asp:ListItem>體育暨運等高等學校</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class=""></td>
                    <td class="">日期：</td>
                    <td class="">
                        <asp:TextBox ID="txtDatePicker" runat="server" class="form_date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="">內容分類：</td>
                    <td>
                        <asp:Label ID="Typelbl" runat="server"></asp:Label>
                    </td>
                    <td></td>
                    <td>編號：</td>
                    <td>
                        <asp:TextBox ID="FileName" runat="server" Text=""></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="">公文標題：</td>
                    <td>
                        <asp:TextBox ID="InputTitleZH" runat="server" Height="70px" TextMode="MultiLine" Width="280px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>denominação do documento oficial：</td>
                    <td>
                        <asp:TextBox ID="InputTitlePT" runat="server" Height="70px" TextMode="MultiLine" Width="280px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                        <asp:CheckBox ID="ListPointCB" runat="server" Text="List" Checked="True" class="checkbox" />
                    </td>
                    <td colspan="4">
                        <asp:Button ID="Save" runat="server" Text="儲存和離開" class="btn pull-right" />
                    </td>

                </tr>
            </table>
        </div>
        <div class="row">
            <asp:Table ID="Table1" runat="server" Style="margin: auto auto;">
                <asp:TableRow runat="server" HorizontalAlign="Center" class="">
                </asp:TableRow>

            </asp:Table>

        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel">請選擇輸出的檔案樣式</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-6 col-md-4">
                            <div class="thumbnail">
                                <a href="javascript: document.getElementById('option1').checked = true;" class="thumbnail">
                                    <img src="holder.js/130x150"></a>
                                <div class="caption">
                                    <input type="radio" name="options" id="option1" />樣式一
                                </div>
                            </div>
                        </div>

                        <div class="col-xs-6 col-md-4">
                            <div class="thumbnail">
                                <a href="javascript: document.getElementById('option2').checked = true;" class="thumbnail">
                                    <img src="holder.js/130x150"></a>
                                <div class="caption">
                                    <input type="radio" name="options" id="option2" />樣式二
                                </div>
                            </div>
                        </div>

                        <div class="col-xs-6 col-md-4">
                            <div class="thumbnail">
                                <a href="javascript: document.getElementById('option3').checked = true;" class="thumbnail">
                                    <img src="holder.js/130x150"></a>
                                <div class="caption">
                                    <input type="radio" name="options" id="option3" />樣式三
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                    <asp:Button class="btn btn-primary" runat="server" Text="提交更改" OnClick="OutputToWord_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal -->
    </div>

    <link href="/bootstrap/css/bootstrap.min.css" rel="stylesheet" media="screen">
    <link href="/css/bootstrap-datepicker.min.css" rel="stylesheet" media="screen">
    <script type="text/javascript" src="/jquery/jquery-1.8.3.min.js" charset="UTF-8"></script>
    <script type="text/javascript" src="/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/js/bootstrap-datepicker.js" charset="UTF-8"></script>
    <script type="text/javascript" src="/js/locales/bootstrap-datepicker.en.js" charset="UTF-8"></script>
    <script type="text/javascript" src="/JS/holder.js"></script>
    <script type="text/javascript">
        $('.form_date').datepicker({
            format: 'yyyy/mm/dd',
            language: 'en',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            forceParse: 0
        });
    </script>
</asp:Content>
