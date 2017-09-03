using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Novacode;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.IO;
using System.Collections;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TranslationSys
{
    public partial class Edit : System.Web.UI.Page
    {
        private void productNum(DropDownList d, int x)
        {
            d.Items.Add("\t");
            for (int i = 1; i <= x; i++)
            {
                d.Items.Add(i.ToString() + "\t");
                d.Items.Add(i.ToString() + ".1\t");
                d.Items.Add(i.ToString() + ".2\t");
                d.Items.Add(i.ToString() + ".3\t");
                d.Items.Add(i.ToString() + ".4\t");
                d.Items.Add(i.ToString() + ".5\t");
                d.Items.Add(i.ToString() + ".6\t");
                d.Items.Add(i.ToString() + ".7\t");
                d.Items.Add(i.ToString() + ".8\t");
                d.Items.Add(i.ToString() + ".9\t");
            }


        }

        private string CodeToString(string code)
        {
            string s = "";
            if (code == "Z0")
            {
                s = "公告";
            }
            else if (code == "Z1")
            {
                s = "招標方案";
            }
            else if (code == "Z2")
            {
                s = "承投規則";
            }
            else if (code == "Z3")
            {
                s = "技術規範";
            }
            if (code == "I1")
            {
                s = "會議召集書";
            }
            else if (code == "I2")
            {
                s = "會議紀要";
            }
            else if (code == "P1")
            {
                s = "行為建議";
            }
            else if (code == "P2")
            {
                s = "開支建議";
            }
            else if (code == "CI1")
            {
                s = "內部通知";
            }
            if (code == "CI2")
            {
                s = "批示";
            }
            else if (code == "CI3")
            {
                s = "理事會決議";
            }
            else if (code == "G1")
            {
                s = "活動邀請";
            }
            else if (code == "G2")
            {
                s = "邀請擔任委員";
            }
            else if (code == "N1")
            {
                s = "會見";
            }
            return s;
        }

        void getID(string code)
        {

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            connection.Open();
            String selectSQL = "Select Size From SizeTable Where Name = 'File" + code + "'";
            System.Data.SqlClient.SqlCommand myCommand1 = new System.Data.SqlClient.SqlCommand(selectSQL, connection);
            int x = Convert.ToInt32(myCommand1.ExecuteScalar());
            String updateSQL = "Update SizeTable Set Size= " + (x + 1) + " Where Name = 'File" + code + "'";
            System.Data.SqlClient.SqlCommand myCommand2 = new System.Data.SqlClient.SqlCommand(updateSQL, connection);
            myCommand2.ExecuteNonQuery();
            connection.Close();
            //Session["ID"] = x;
        }

        string getCode()
        {
            string code = "";
            string type = Session["Type"].ToString();
            if (type == "Z0")
            {
                code = "t";
            }
            else if (type == "Z1")
            {
                code = "t";
            }
            else if (type == "Z2")
            {
                code = "t";
            }
            else if (type == "Z3")
            {
                code = "t";
            }
            if (type == "I1")
            {
                code = "m";
            }
            else if (type == "I2")
            {
                code = "m";
            }
            else if (type == "P1")
            {
                code = "p1";
            }
            else if (type == "P2")
            {
                code = "p1";
            }
            else if (type == "CI1")
            {
                code = "i";
            }
            if (type == "CI2")
            {
                code = "i";
            }
            else if (type == "CI3")
            {
                code = "i";
            }
            else if (type == "G1")
            {
                code = "o";
            }
            else if (type == "G2")
            {
                code = "o";
            }
            else if (type == "N1")
            {
                code = "p2";
            }
            return code;
        }

        protected override void OnInit(EventArgs e)
        {

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            connection.Open();
            string FileId = Session["Edit_File"].ToString();

            String selectSQL = "Select Department,Date,Type,FileName From DataTable Where FileId = '" + FileId + "';";
            System.Data.SqlClient.SqlCommand myCommand1 = new System.Data.SqlClient.SqlCommand(selectSQL, connection);
            SqlDataReader reader = myCommand1.ExecuteReader();
            int txtzh = 1, txtpt = 1;
            if (reader.Read())
            {
                string Department = reader.GetString(0);
                string Date = reader.GetString(1);
                string Type = reader.GetString(2);
                string filename = reader.GetString(3);

                if (Department == "ESA")
                {
                    SchoolChoice.SelectedValue = "藝術高等學院";
                }
                else if (Department == "ESCE")
                {
                    SchoolChoice.SelectedValue = "管理科學高等學校";
                }
                else if (Department == "ESS")
                {
                    SchoolChoice.SelectedValue = "高等衛生學校";
                }
                else if (Department == "ESLT")
                {
                    SchoolChoice.SelectedValue = "語言暨翻譯高等學校";
                }
                else if (Department == "ESEFD")
                {
                    SchoolChoice.SelectedValue = "體育暨運動高等學校";
                }
                else if (Department == "ESAP")
                {
                    SchoolChoice.SelectedValue = "公共行政高等學校";
                }
                txtDatePicker.Text = Date;
                FileName.Text = filename;
                reader.Close();
                String selectSQL1 = "Select Size From SizeTable Where Name = '" + Type + "';";
                System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(selectSQL1, connection);
                int size = int.Parse(myCommand.ExecuteScalar().ToString());

                String ZHTitleSQL = "Select Title From  Data Where Language='ZH' AND ID='" + FileId + "'";
                System.Data.SqlClient.SqlCommand ZHTitleCommand = new System.Data.SqlClient.SqlCommand(ZHTitleSQL, connection);
                InputTitleZH.Text = ZHTitleCommand.ExecuteScalar().ToString();

                String PTTitleSQL = "Select Title From  Data Where Language='PT' AND ID='" + FileId + "'";
                System.Data.SqlClient.SqlCommand PTTitleCommand = new System.Data.SqlClient.SqlCommand(PTTitleSQL, connection);
                InputTitlePT.Text = PTTitleCommand.ExecuteScalar().ToString();




                for (int i = 1; i <= size; i++)
                {
                    String ZHSQL = "Select Row"+i+" From  Data Where Language='ZH' AND ID='" + FileId + "'";
                    System.Data.SqlClient.SqlCommand ZHCommand = new System.Data.SqlClient.SqlCommand(ZHSQL, connection);
                    string zhtxt = ZHCommand.ExecuteScalar().ToString();

                    TableRow row = new TableRow();

                    DropDownList numZH = new DropDownList();
                    productNum(numZH, size);
                    TableCell numCellZH = new TableCell();
                    numCellZH.Style.Add("padding", "0");
                    numCellZH.Style.Add("vertical-align", "top");
                    numCellZH.Controls.Add(numZH);
                    numZH.ID = "DropZH" + i;
                    numZH.AutoPostBack = true;
                    numZH.SelectedIndexChanged += new EventHandler((ds, __) =>
                    {
                        string sub = ((DropDownList)ds).ID.Substring(6);
                        ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT" + sub)).SelectedValue = ((DropDownList)ds).SelectedValue;

                    });
                    row.Cells.Add(numCellZH);

                    TableCell cellzh = new TableCell();
                    cellzh.Style.Add("padding", "0 50px 10px 0");
                    cellzh.Style.Add("vertical-align", "top");
                    TextBox tbzh = new TextBox();
                    tbzh.Text = zhtxt;
                    tbzh.TextMode = TextBoxMode.MultiLine;
                    tbzh.Style.Add("width", "400px");
                    tbzh.Style.Add("min-height", "25px");
                    tbzh.Style.Add("max-height", "400px");
                    tbzh.Style.Add("resize", "none");
                    tbzh.ID = "InputZH" + txtzh++;

                    tbzh.Load += new EventHandler((____, __) =>
                    {
                        ClientScript.RegisterStartupScript(tbzh.GetType(), tbzh.ID, "resize(MainContent_" + tbzh.ID + ");", true);
                    });
                    string someScript = "function resize(txt){txt.style.height = 'auto'; txt.style.height = txt.scrollHeight +10+ 'px';}";
                    tbzh.Attributes.Add("onkeyup", "resize(this);");
                    ClientScript.RegisterStartupScript(this.GetType(), "resize", someScript, true);

                    cellzh.Controls.Add(tbzh);
                    row.Cells.Add(cellzh);


                    //PT
                    String PTSQL = "Select Row" + i + " From  Data Where Language='PT' AND ID='" + FileId + "'";
                    System.Data.SqlClient.SqlCommand PTCommand = new System.Data.SqlClient.SqlCommand(PTSQL, connection);
                    string pttxt = PTCommand.ExecuteScalar().ToString();

                    DropDownList numPT = new DropDownList();
                    productNum(numPT, size);
                    TableCell numCellPT = new TableCell();
                    numCellPT.Style.Add("padding", "0");
                    numCellPT.Style.Add("vertical-align", "top");
                    numCellPT.Controls.Add(numPT);
                    numPT.ID = "DropPT" + i;
                    numPT.AutoPostBack = true;
                    numPT.SelectedIndexChanged += new EventHandler((ds, __) =>
                    {
                        string sub = ((DropDownList)ds).ID.Substring(6);
                        ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT" + sub)).SelectedValue = ((DropDownList)ds).SelectedValue;

                    });
                    row.Cells.Add(numCellPT);

                    TableCell cellpt = new TableCell();
                    cellpt.Style.Add("padding", "0 50px 10px 0");
                    cellpt.Style.Add("vertical-align", "top");
                    TextBox tbpt = new TextBox();
                    tbpt.Text = pttxt;
                    tbpt.TextMode = TextBoxMode.MultiLine;
                    tbpt.Style.Add("width", "400px");
                    tbpt.Style.Add("min-height", "25px");
                    tbpt.Style.Add("max-height", "400px");
                    tbpt.Style.Add("resize", "none");
                    tbpt.ID = "InputPT" + txtpt++;

                    tbpt.Load += new EventHandler((____, __) =>
                    {
                        ClientScript.RegisterStartupScript(tbpt.GetType(), tbpt.ID, "resize(MainContent_" + tbpt.ID + ");", true);
                    });
                    tbpt.Attributes.Add("onkeyup", "resize(this);");
                    ClientScript.RegisterStartupScript(this.GetType(), "resize", someScript, true);

                    cellpt.Controls.Add(tbpt);
                    row.Cells.Add(cellpt);
                    Table1.Rows.Add(row);

                }



            }

            Session["txtzh"] = txtzh;
            Session["txtpt"] = txtpt;
            connection.Close();


            base.OnInit(e);

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SchoolChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ((DropDownList)sender).SelectedItem.Text;
            if (selected == "藝術高等學校")
            {
                Session["School"] = "ESA";
            }
            else if (selected == "管理科學高等學校")
            {
                Session["School"] = "ESCE";
            }
            else if (selected == "高等衛生學校")
            {
                Session["School"] = "ESS";
            }
            else if (selected == "語言暨翻譯高等學校")
            {
                Session["School"] = "ESLT";
            }
            else if (selected == "體育暨運動高等學校")
            {
                Session["School"] = "ESEFD";
            }
            else if (selected == "公共行政高等學校")
            {
                Session["School"] = "ESAP";
            }
            FileName.Text = Session["ID"].ToString() + "/" + Session["School"] + "/2017";
        }





        protected void OutputToWord_Click(object sender, EventArgs e)
        {


            string fn = FileName.Text;
            fn = fn.Replace("/", ".");
            string fileName = Server.MapPath("..\\Data") + "\\" + fn + ".docx";
            //string fileName = @"C:\Users\MPI\Documents\Visual Studio 2017\Projects\transSysforMPI-master\DATA\" + fn + ".docx";
            System.IO.FileInfo file = new System.IO.FileInfo(fileName);
            if (file.Exists)
            {
                Response.Write("<script>alert('已經有相同編號的文件')</script>");
                return;
            }


            string type = Session["Type"].ToString();
            string table = Session["Table"].ToString();


            // Create a document in memory:
            var doc1 = DocX.Load(Server.MapPath("..\\Template") + "\\" + table + ".docx");
            var doc2 = DocX.Load(Server.MapPath("..\\Template") + "\\" + table + ".docx");

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            connection.Open();
            String selectSQL = "Select Size From SizeTable Where Name = '" + type + "'";
            System.Data.SqlClient.SqlCommand myCommand1 = new System.Data.SqlClient.SqlCommand(selectSQL, connection);
            int x = Convert.ToInt32(myCommand1.ExecuteScalar());



            string txtTypeZH = "";
            string txtTypePT = "";
            if (table == "Z") { txtTypeZH = "公開招標"; txtTypePT = "concurso público"; }
            else if (table == "I") { txtTypeZH = "會議文件"; txtTypePT = "documentos reunião"; }
            else if (table == "P") { txtTypeZH = "建議書"; txtTypePT = "recomendações"; }
            else if (table == "CI") { txtTypeZH = "內部運作文件"; txtTypePT = "O arquivo funcionamento interno"; }
            else if (table == "G") { txtTypeZH = "公函"; txtTypePT = "cartas"; }
            else if (table == "N") { txtTypeZH = "新聞稿"; txtTypePT = "Press Release"; }

            doc1.ReplaceText("Table", txtTypeZH);
            doc1.ReplaceText("Title", InputTitleZH.Text);
            doc2.ReplaceText("Table", txtTypePT);
            doc2.ReplaceText("Title", InputTitlePT.Text);

            String[] zhData = new String[x];
            String[] ptData = new String[x];

            if (table == "P")
            {
                doc1.ReplaceText("Type", CodeToString(type));
                doc1.ReplaceText("Time", txtDatePicker.Text);
                doc1.ReplaceText("Code", FileName.Text);
                doc2.ReplaceText("Type", CodeToString(type));
                doc2.ReplaceText("Time", txtDatePicker.Text);
                doc2.ReplaceText("Code", FileName.Text);




                int lblzh = 1;
                int txtzh = 1;
                int lblpt = 1;
                int txtpt = 1;
                for (int i = 1; i <= 3 && i <= x; i++)
                {
                    String rowZHSQL = "Select Row" + i + " From " + table + "Table Where Language = 'ZH' AND Type = '" + type + "ZH'";
                    System.Data.SqlClient.SqlCommand rowZHCommand = new System.Data.SqlClient.SqlCommand(rowZHSQL, connection);
                    string rowZHString = Convert.ToString(rowZHCommand.ExecuteScalar());

                    if (rowZHString.Equals("TextBox"))
                    {
                        if (ListPointCB.Checked) { doc1.ReplaceText("Paragraph" + i, ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        else { doc1.ReplaceText("Paragraph" + i, ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        zhData[i - 1] = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh++)).Text;
                    }
                    else
                    {
                        doc1.InsertParagraph();
                        if (ListPointCB.Checked)
                        { doc1.ReplaceText("Paragraph" + i, ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text); }
                        else { doc1.ReplaceText("Paragraph" + i, ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text); }
                        zhData[i - 1] = ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh++)).Text;
                    }
                }

                for (int i = 4; i <= x; i++)
                {
                    String rowZHSQL = "Select Row" + i + " From " + table + "Table Where Language = 'ZH' AND Type = '" + type + "ZH'";
                    System.Data.SqlClient.SqlCommand rowZHCommand = new System.Data.SqlClient.SqlCommand(rowZHSQL, connection);
                    string rowZHString = Convert.ToString(rowZHCommand.ExecuteScalar());
                    if (rowZHString.Equals("TextBox"))
                    {
                        if (ListPointCB.Checked) { doc1.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        else { doc1.InsertParagraph(((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        zhData[i - 1] = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh++)).Text;
                    }
                    else
                    {
                        doc1.InsertParagraph();
                        if (ListPointCB.Checked)
                        { doc1.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text).Bold(); }
                        else { doc1.InsertParagraph(((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text).Bold(); }
                        zhData[i - 1] = ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh++)).Text;
                    }
                }

                doc1.InsertSectionPageBreak();

                for (int i = 1; i <= 3 && i <= x; i++)
                {
                    String rowPTSQL = "Select Row" + i + " From " + table + "Table Where Language = 'PT' AND Type = '" + type + "PT'";
                    System.Data.SqlClient.SqlCommand rowPTCommand = new System.Data.SqlClient.SqlCommand(rowPTSQL, connection);
                    string rowPTString = Convert.ToString(rowPTCommand.ExecuteScalar());

                    if (rowPTString.Equals("TextBox"))
                    {
                        if (ListPointCB.Checked) { doc2.ReplaceText("Paragraph" + i, ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT" + i)).Text + ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt)).Text); }
                        else { doc2.ReplaceText("Paragraph" + i, ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt)).Text); }
                        ptData[i - 1] = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt++)).Text;
                    }
                    else
                    {
                        doc2.InsertParagraph();
                        if (ListPointCB.Checked)
                        { doc2.ReplaceText("Paragraph" + i, ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT" + i)).Text + ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt)).Text); }
                        else { doc2.ReplaceText("Paragraph" + i, ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt)).Text); }
                        ptData[i - 1] = ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt++)).Text;
                    }
                }

                for (int i = 4; i <= x; i++)
                {
                    String rowPTSQL = "Select Row" + i + " From " + table + "Table Where Language = 'PT' AND Type = '" + type + "PT'";
                    System.Data.SqlClient.SqlCommand rowPTCommand = new System.Data.SqlClient.SqlCommand(rowPTSQL, connection);
                    string rowPTString = Convert.ToString(rowPTCommand.ExecuteScalar());
                    if (rowPTString.Equals("TextBox"))
                    {
                        if (ListPointCB.Checked) { doc2.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT" + i)).Text + ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt)).Text); }
                        else { doc2.InsertParagraph(((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt)).Text); }
                        ptData[i - 1] = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt++)).Text;
                    }
                    else
                    {
                        doc2.InsertParagraph();
                        if (ListPointCB.Checked)
                        { doc2.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT" + i)).Text + ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt)).Text).Bold(); }
                        else { doc2.InsertParagraph(((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt)).Text).Bold(); }
                        ptData[i - 1] = ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt++)).Text;
                    }
                }
                doc1.InsertDocument(doc2);
            }
            else
            {


                int lblzh = 1;
                int txtzh = 1;
                int lblpt = 1;
                int txtpt = 1;
                for (int i = 1; i <= x; i++)
                {
                    String rowZHSQL = "Select Row" + i + " From " + table + "Table Where Language = 'ZH' AND Type = '" + type + "ZH'";
                    System.Data.SqlClient.SqlCommand rowZHCommand = new System.Data.SqlClient.SqlCommand(rowZHSQL, connection);
                    string rowZHString = Convert.ToString(rowZHCommand.ExecuteScalar());
                    if (rowZHString.Equals("TextBox"))
                    {
                        if (ListPointCB.Checked) { doc1.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        else { doc1.InsertParagraph(((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        zhData[i - 1] = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh++)).Text;
                    }
                    else
                    {
                        doc1.InsertParagraph();
                        if (ListPointCB.Checked)
                        { doc1.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text).Bold(); }
                        else { doc1.InsertParagraph(((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text).Bold(); }
                        zhData[i - 1] = ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh++)).Text;
                    }
                }
                doc1.InsertSectionPageBreak();
                for (int i = 1; i <= x; i++)
                {
                    String rowPTSQL = "Select Row" + i + " From " + table + "Table Where Language = 'PT' AND Type = '" + type + "PT'";
                    System.Data.SqlClient.SqlCommand rowZHCommand = new System.Data.SqlClient.SqlCommand(rowPTSQL, connection);
                    string rowPTString = Convert.ToString(rowZHCommand.ExecuteScalar());
                    if (rowPTString.Equals("TextBox"))
                    {
                        if (ListPointCB.Checked) { doc2.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT" + i)).Text + ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt)).Text); }
                        else { doc2.InsertParagraph(((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt)).Text); }
                        ptData[i - 1] = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt++)).Text;
                    }
                    else
                    {
                        doc2.InsertParagraph();
                        if (ListPointCB.Checked)
                        { doc2.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT" + i)).Text + ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt)).Text).Bold(); }
                        else { doc2.InsertParagraph(((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt)).Text).Bold(); }
                        ptData[i - 1] = ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt++)).Text;
                    }

                }
                doc1.InsertDocument(doc2);
            }



            doc1.SaveAs(fileName);

            System.IO.FileInfo file2 = new System.IO.FileInfo(fileName);
            if (file2.Exists)
            {
                string id = Guid.NewGuid().ToString();
                getID(getCode());
                String strSQL = "Insert into DataTable (FileName,Date,Type,Status,Title,UserId,FileId) Values (@fileName,@date,@type,@status,@title,@userid,@fileid)";

                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.Parameters.AddWithValue("@fileName", fn);
                myCommand.Parameters.AddWithValue("@date", txtDatePicker.Text);
                myCommand.Parameters.AddWithValue("@type", type);
                myCommand.Parameters.AddWithValue("@status", 0);
                myCommand.Parameters.AddWithValue("@title", InputTitleZH.Text);
                myCommand.Parameters.AddWithValue("@userid", User.Identity.GetUserId().ToString());
                myCommand.Parameters.AddWithValue("@fileid", id);
                myCommand.ExecuteNonQuery();


                String insertSQLZH = "Insert into Data (ID,Language,Title";
                for (int i = 1; i <= x; i++)
                {
                    insertSQLZH += ",Row" + i;
                }
                insertSQLZH += ") Values (@id,@language,@title";
                for (int i = 1; i <= x; i++)
                {
                    insertSQLZH += ",@row" + i;
                }
                insertSQLZH += ")";

                SqlCommand insertCommandZH = new SqlCommand(insertSQLZH, connection);
                insertCommandZH.Parameters.AddWithValue("@id", id);
                insertCommandZH.Parameters.AddWithValue("@language", "ZH");
                insertCommandZH.Parameters.AddWithValue("@title", InputTitleZH.Text);
                for (int i = 1; i <= x; i++)
                {
                    insertCommandZH.Parameters.AddWithValue("@row" + i, zhData[i - 1]);
                }
                insertCommandZH.ExecuteNonQuery();

                String insertSQLPT = "Insert into Data (ID,Language,Title";
                for (int i = 1; i <= x; i++)
                {
                    insertSQLPT += ",Row" + i;
                }
                insertSQLPT += ") Values (@id,@language,@title";
                for (int i = 1; i <= x; i++)
                {
                    insertSQLPT += ",@row" + i;
                }
                insertSQLPT += ")";

                SqlCommand insertCommandPT = new SqlCommand(insertSQLPT, connection);
                insertCommandPT.Parameters.AddWithValue("@id", id);
                insertCommandPT.Parameters.AddWithValue("@language", "PT");
                insertCommandPT.Parameters.AddWithValue("@title", InputTitlePT.Text);
                for (int i = 1; i <= x; i++)
                {
                    insertCommandPT.Parameters.AddWithValue("@row" + i, ptData[i - 1]);
                }
                insertCommandPT.ExecuteNonQuery();



                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file2.Name);
                Response.AddHeader("Content-Length", file2.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file2.FullName);
                Response.End();


            }
            else
            {
                Response.Write("This file does not exist.");
            }
            connection.Close();
            Response.Write("輸出成功");


        }




    }
}