using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace TranslationSys
{
    public partial class Home : Page
    {

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label1 = (Label)e.Row.FindControl("Label1");
                LinkButton linkbutton1 = (LinkButton)e.Row.FindControl("LinkButton1");
                string status = label1.Text;
                if (status == "0")
                {
                    label1.Text = "原文";
                    label1.Attributes.Add("class", "label label-warning");
                }
                else if (status == "1")
                {
                    label1.Text = "翻譯中";
                    label1.Attributes.Add("class", "label label-info");
                    if(User.IsInRole("User"))
                        linkbutton1.Enabled=false;
                }
                else if (status == "2")
                {
                    label1.Text = "已翻譯";
                    label1.Attributes.Add("class", "label label-success");
                }

                Label label2 = (Label)e.Row.FindControl("Label2");
                string type = label2.Text;
                if (type == "Z0")
                {
                    label2.Text = "公告";
                }
                else if (type == "Z1")
                {
                    label2.Text = "招標方案";
                }
                else if (type == "Z2")
                {
                    label2.Text = "承投規則";
                }
                else if (type == "Z3")
                {
                    label2.Text = "技術規範";
                }
                if (type == "I1")
                {
                    label2.Text = "會議召集書";
                }
                else if (type == "I2")
                {
                    label2.Text = "會議紀要";
                }
                else if (type == "P1")
                {
                    label2.Text = "行為建議";
                }
                else if (type == "P2")
                {
                    label2.Text = "開支建議";
                }
                else if (type == "CI1")
                {
                    label2.Text = "內部通知";
                }
                if (type == "CI2")
                {
                    label2.Text = "批示";
                }
                else if (type == "CI3")
                {
                    label2.Text = "理事會決議";
                }
                else if (type == "G1")
                {
                    label2.Text = "活動邀請";
                }
                else if (type == "G2")
                {
                    label2.Text = "邀請擔任委員";
                }
                else if (type == "N1")
                {
                    label2.Text = "會見";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.IsInRole("User"))
            {
                SqlDataSource1.SelectParameters.Add(new Parameter("UserId"));
                SqlDataSource1.SelectCommand = "SELECT [Status], [Type], [Date], [Title], [FileName],[FileId] FROM [DataTable] WHERE ([UserId] = @UserId)";
                SqlDataSource1.SelectParameters["UserId"].DefaultValue = User.Identity.GetUserId();
            }
            else
            {
                SqlDataSource1.SelectParameters.Add(new Parameter("TranslatorID"));
                SqlDataSource1.SelectCommand = "SELECT [Status], [Type], [Date], [Title], [FileName],[FileId] FROM [DataTable] WHERE ([TranslatorID] is NULL) or ([TranslatorID] = @TranslatorID)";
                SqlDataSource1.SelectParameters["TranslatorID"].DefaultValue = User.Identity.GetUserId();
            }
            
            
            string someScript = "$(document).ready(function () {var sidebar = $('#page-sidebar');sidebar.addClass('page-sidebar-left');sidebar.removeClass('page-sidebar-hide');var content=$('#page-content');content.addClass('page-sidebar-content')})";
            ClientScript.RegisterStartupScript(this.GetType(), "x", someScript, true);


        }

        protected void LinkButteon1_Click(object sender, EventArgs e)
        {

            LinkButton linkbutton1 = (LinkButton)sender;
            string x = linkbutton1.CommandArgument;
            Session["Edit_File"] = x;
            if (!User.IsInRole("User"))
            {
                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
                connection.Open();
                String updateSQL = "Update DataTable Set Status=1 ,TranslatorID='"+User.Identity.GetUserId()+"' Where FileId='"+x+"'";
                System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(updateSQL, connection);
                myCommand.ExecuteNonQuery();
                connection.Close();
            }


            Response.Redirect("~/App_User/Edit");

        }
    }


}