using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TranslationSys
{
    public partial class New : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void getID(string code)
        {

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            connection.Open();
            String selectSQL = "Select Size From SizeTable Where Name = 'File" + code + "'";
            System.Data.SqlClient.SqlCommand myCommand1 = new System.Data.SqlClient.SqlCommand(selectSQL, connection);
            int x = Convert.ToInt32(myCommand1.ExecuteScalar());
            //String updateSQL = "Update SizeTable Set Size= " + (x + 1) + " Where Name = 'File" + code + "'";
            //System.Data.SqlClient.SqlCommand myCommand2 = new System.Data.SqlClient.SqlCommand(updateSQL, connection);
            //myCommand2.ExecuteNonQuery();
            connection.Close();
            Session["ID"] = code.Substring(0, 1).ToUpper() + string.Format("{0,3:X3}", x);
        }


        protected void NextPage_Click(object sender, EventArgs e)
        {


            string code = "";
            string selected = TypeDDL.SelectedItem.Text;
            if (selected == "公告")
            {
                Session["Type"] = "Z0";
                Session["Table"] = "Z";
                code = "t";
            }
            else if (selected == "招標方案")
            {
                Session["Type"] = "Z1";
                Session["Table"] = "Z";
                code = "t";
            }
            else if (selected == "承投規則")
            {
                Session["Type"] = "Z2";
                Session["Table"] = "Z";
                code = "t";
            }
            else if (selected == "技術規範")
            {
                Session["Type"] = "Z3";
                Session["Table"] = "Z";
                code = "t";
            }
            if (selected == "會議召集書")
            {
                Session["Type"] = "I1";
                Session["Table"] = "I";
                code = "m";
            }
            else if (selected == "會議紀要")
            {
                Session["Type"] = "I2";
                Session["Table"] = "I";
                code = "m";
            }
            else if (selected == "行為建議")
            {
                Session["Type"] = "P1";
                Session["Table"] = "P";
                code = "p1";
            }
            else if (selected == "開支建議")
            {
                Session["Type"] = "P2";
                Session["Table"] = "P";
                code = "p1";
            }
            else if (selected == "內部通知")
            {
                Session["Type"] = "CI1";
                Session["Table"] = "CI";
                code = "i";
            }
            if (selected == "批示")
            {
                Session["Type"] = "CI2";
                Session["Table"] = "CI";
                code = "i";
            }
            else if (selected == "理事會決議")
            {
                Session["Type"] = "CI3";
                Session["Table"] = "CI";
                code = "i";
            }
            else if (selected == "活動邀請")
            {
                Session["Type"] = "G1";
                Session["Table"] = "G";
                code = "o";
            }
            else if (selected == "邀請擔任委員")
            {
                Session["Type"] = "G2";
                Session["Table"] = "G";
                code = "o";
            }
            else if (selected == "會見")
            {
                Session["Type"] = "N1";
                Session["Table"] = "N";
                code = "p2";
            }

            Application.Lock();
            getID(code);
            Application.UnLock();
            Session["School"] = "ESAP";

            Response.Redirect("New_2.aspx");
        }


        protected void TypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            NextPage.Enabled = true;
        }
        protected void TableDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeDDL.Items.Clear();
            if (TableDDL.SelectedValue == "Z")
            {
                TypeDDL.Items.Add("公告");
                TypeDDL.Items.Add("招標方案");
                TypeDDL.Items.Add("承投規則");
                TypeDDL.Items.Add("技術規範");

            }
            else if (TableDDL.SelectedValue == "I")
            {
                TypeDDL.Items.Add("會議召集書");
                TypeDDL.Items.Add("會議紀要");
            }
            else if (TableDDL.SelectedValue == "P")
            {
                TypeDDL.Items.Add("行為建議");
                TypeDDL.Items.Add("開支建議");
            }
            else if (TableDDL.SelectedValue == "CI")
            {
                TypeDDL.Items.Add("內部通知");
                TypeDDL.Items.Add("批示");
                TypeDDL.Items.Add("理事會決議");
            }
            else if (TableDDL.SelectedValue == "G")
            {
                TypeDDL.Items.Add("活動邀請");
                TypeDDL.Items.Add("邀請擔任委員");
            }
            else if (TableDDL.SelectedValue == "N")
            {
                TypeDDL.Items.Add("會見");
            }
        }
    }
}