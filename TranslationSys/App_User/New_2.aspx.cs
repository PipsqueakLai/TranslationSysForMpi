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

namespace TranslationSys
{
    public partial class New_2 : System.Web.UI.Page
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

        protected override void OnInit(EventArgs e)
        {

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            connection.Open();

            string type = "";
            type = Session["Type"].ToString();
            string table = "";
            table = Session["Table"].ToString();

            Typelbl.Text = CodeToString(type);

            String selectSQL = "Select Size From SizeTable Where Name = '" + type + "'";
            System.Data.SqlClient.SqlCommand myCommand1 = new System.Data.SqlClient.SqlCommand(selectSQL, connection);
            int x = Convert.ToInt32(myCommand1.ExecuteScalar());



            int lblzh = 1, txtzh = 1;
            int lblpt = 1, txtpt = 1;
            for (int i = 1; i <= x; i++)
            {
                String rowZHSQL = "Select Row" + i + " From " + table + "Table Where Language = 'ZH' AND Type = '" + type + "ZH'";
                System.Data.SqlClient.SqlCommand rowZHCommand = new System.Data.SqlClient.SqlCommand(rowZHSQL, connection);
                string rowZHString = Convert.ToString(rowZHCommand.ExecuteScalar());

                String rowPTSQL = "Select Row" + i + " From " + table + "Table Where Language = 'PT' AND Type = '" + type + "PT'";
                System.Data.SqlClient.SqlCommand rowPTCommand = new System.Data.SqlClient.SqlCommand(rowPTSQL, connection);
                string rowPTString = Convert.ToString(rowPTCommand.ExecuteScalar());

                TableRow row = new TableRow();

                DropDownList numZH = new DropDownList();
                productNum(numZH, x);
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

                if (rowZHString.Equals("TextBox"))
                {
                    TableCell cell = new TableCell();
                    cell.Style.Add("padding", "0 50px 10px 0");
                    cell.Style.Add("vertical-align", "top");
                    TextBox tb = new TextBox();
                    tb.TextMode = TextBoxMode.MultiLine;
                    tb.Style.Add("width", "400px");
                    tb.Style.Add("min-height", "25px");
                    tb.Style.Add("max-height", "400px");
                    tb.Style.Add("resize", "none");
                    tb.ID = "InputZH" + txtzh++;

                    tb.Load += new EventHandler((____, __) =>
                    {
                        ClientScript.RegisterStartupScript(tb.GetType(), tb.ID, "resize(MainContent_" + tb.ID + ");", true);
                    });
                    string someScript = "function resize(txt){txt.style.height = 'auto'; txt.style.height = txt.scrollHeight +10+ 'px';}";
                    tb.Attributes.Add("onkeyup", "resize(this);");
                    ClientScript.RegisterStartupScript(this.GetType(), "resize", someScript, true);

                    cell.Controls.Add(tb);
                    row.Cells.Add(cell);
                }
                else
                {
                    TableCell cell = new TableCell();
                    cell.Style.Add("padding", "0 50px 10px 0");
                    cell.Style.Add("vertical-align", "top");
                    Label lb = new Label();
                    lb.ID = "LabelZH" + lblzh++;
                    lb.Text = rowZHString;
                    cell.Controls.Add(lb);
                    row.Cells.Add(cell);
                }


                DropDownList numPT = new DropDownList();

                TableCell numCellPT = new TableCell();
                productNum(numPT, x);
                numCellPT.Style.Add("padding", "0");
                numCellPT.Style.Add("vertical-align", "top");
                numCellPT.Controls.Add(numPT);
                numPT.ID = "DropPT" + i;
                numPT.AutoPostBack = true;
                numPT.SelectedIndexChanged += new EventHandler((ds, ____) =>
                {
                    string sub = ((DropDownList)ds).ID.Substring(6);
                    ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + sub)).SelectedValue = ((DropDownList)ds).SelectedValue;

                });
                row.Cells.Add(numCellPT);

                if (rowPTString.Equals("TextBox"))
                {
                    TableCell cell = new TableCell();
                    cell.Style.Add("vertical-align", "top");
                    cell.Style.Add("padding", "0 50px 10px 0");
                    TextBox tb = new TextBox();
                    tb.TextMode = TextBoxMode.MultiLine;
                    tb.Style.Add("width", "400px");
                    tb.Style.Add("min-height", "25px");
                    tb.Style.Add("max-height", "400px");
                    tb.Style.Add("resize", "none");
                    tb.ID = "InputPT" + txtpt++;




                    tb.Load += new EventHandler((sender, Loadevent) =>
                    {
                        ClientScript.RegisterStartupScript(tb.GetType(), tb.ID, "resize(MainContent_" + tb.ID + ");", true);
                    });
                    string someScript = "function resize(txt){txt.style.height = 'auto'; txt.style.height = txt.scrollHeight +10+ 'px';}";
                    tb.Attributes.Add("onkeyup", "resize(this);");
                    ClientScript.RegisterStartupScript(this.GetType(), "resize", someScript, true);
                    cell.Controls.Add(tb);
                    row.Cells.Add(cell);
                }
                else
                {
                    TableCell cell = new TableCell();
                    cell.Style.Add("padding", "0 50px 10px 0");
                    cell.Style.Add("vertical-align", "top");
                    Label lb = new Label();
                    lb.ID = "LabelPT" + lblpt++;
                    lb.Text = rowPTString;
                    cell.Controls.Add(lb);
                    row.Cells.Add(cell);

                }

                Table1.Rows.Add(row);


            }
            Session["lblzh"] = lblzh;
            Session["lblpt"] = lblpt;
            Session["txtzh"] = txtzh;
            Session["txtpt"] = txtpt;
            connection.Close();


            base.OnInit(e);

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                FileName.Text = Session["ID"].ToString() + "/" + Session["School"] + "/2017";
            }
        }

        protected void Generatorx_Click(object sender, EventArgs e)
        {

            //MExcel excel = new MExcel();
            //excel.LoadExcelToDataBase(@"C:\Users\MPI\Desktop\M2.xlsx");
            InputTitlePT.Text = Dictionary(InputTitleZH.Text);
        }

        protected void Generator_Click(object sender, EventArgs e)
        {
            string type = Session["Type"].ToString();
            if (type == "Z0")
            {

                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH6");
                TextBox TextBox7 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH7");
                TextBox TextBox8 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH8");
                TextBox TextBox9 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH9");

                InputTitleZH.Text = "「招攬第64屆澳門格蘭披治大賽車 - 澳門GT盃冠名贊助」 ";

                TextBox1.Text = "根據行政程序法典第一百六十五條第二款、第一百七十條第一款及第一百七 十六條，以及七月六日第 63/85/M 號法令第十三條的規定，並根據社會文化 司司長於二零一七年三月二十四日的批示，體育局現為招攬第 64 屆澳門格蘭 披治大賽車 – 澳門 GT 盃冠名贊助，代表判給人進行公開招攬程序。 ";
                TextBox2.Text = "有意之投標者可於本招攬公告刊登日起，於辦公時間上午九時至下午一時、 下午二時三十分至五時三十分，前往位於澳門羅理基博士大馬路 818 號體育 局總部接待處查閱卷宗或索取招攬案卷複印本一份。投標者亦可於體育局網 頁（www.sport.gov.mo） ";
                TextBox3.Text = "在遞交投標書期限屆滿前，有意投標者應自行前往體育局總部，以了解有否 附加說明之文件。 ";
                TextBox4.Text = "講解會將訂於二零一七年四月十一日〈星期二〉上午十時正在澳門友誼大馬 路 207 號澳門格蘭披治賽車大樓會議室進行。倘上述講解會日期及時間因颱 風或不可抗力之原因導致體育局停止辦公，則上述講解會日期及時間順延至 緊接之首個工作日的相同時間。 ";
                TextBox5.Text = "遞交投標書的截止時間為二零一七年四月二十四日〈星期一〉中午十二時正， 逾時的投標書不被接納。倘上述截標日期及時間因颱風或不可抗力之原因導 致體育局停止辦公，則上述遞交投標書的截止日期及時間順延至緊接之首個 工作日的相同時間。投標者須於該截止時間前將投標書交往位於上指地址的 體育局總部。 ";
                TextBox6.Text = "開標將訂於二零一七年四月二十五日〈星期二〉上午九時三十分在澳門羅理 基博士大馬路 818 號體育局總部會議室進行。倘上述開標日期及時間因颱風 或不可抗力之原因導致體育局停止辦公，又或上述截止遞交投標書的日期及 時間因颱風或不可抗力之原因順延，則開標的日期及時間順延至緊接之首個 工作日的相同時間。 ";
                TextBox7.Text = "投標書自開標日起計九十日內有效。 ";
                TextBox8.Text = "二零一七年四月五日於體育局。";
                TextBox9.Text = "代局長  劉楚遠 ";
            }
            else if (type == "Z1")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH6");
                TextBox TextBox7 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH7");
                TextBox TextBox8 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH8");
                TextBox TextBox9 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH9");
                TextBox TextBox10 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH10");
                TextBox TextBox11 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH11");
                TextBox TextBox12 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH12");
                TextBox TextBox13 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH13");
                TextBox TextBox14 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH14");
                TextBox TextBox15 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH15");
                TextBox TextBox16 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH16");
                TextBox TextBox17 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH17");
                TextBox TextBox18 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH18");
                DropDownList DropDownList1 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH1");
                DropDownList DropDownList2 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH2");
                DropDownList DropDownList3 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH3");
                DropDownList DropDownList4 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH4");
                DropDownList DropDownList5 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH5");
                DropDownList DropDownList6 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH6");
                DropDownList DropDownList7 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH7");
                DropDownList DropDownList8 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH8");
                DropDownList DropDownList9 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH9");
                DropDownList DropDownList10 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH10");
                DropDownList DropDownList11 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH11");
                DropDownList DropDownList12 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH12");
                DropDownList DropDownList13 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH13");
                DropDownList DropDownList14 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH14");
                DropDownList DropDownList15 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH15");
                DropDownList DropDownList16 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH16");
                DropDownList DropDownList17 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH17");
                DropDownList DropDownList18 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH18");
                DropDownList DropDownList19 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH19");
                DropDownList DropDownList20 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH20");
                DropDownList DropDownList21 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH21");
                DropDownList DropDownList22 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH22");
                DropDownList DropDownList23 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH23");
                DropDownList DropDownList24 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH24");
                DropDownList DropDownList25 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH25");



                DropDownList1.SelectedValue = "1\t";
                DropDownList2.SelectedValue = "\t";
                DropDownList3.SelectedValue = "2\t";
                DropDownList4.SelectedValue = "2.1\t";
                DropDownList5.SelectedValue = "2.2\t";
                DropDownList6.SelectedValue = "2.3\t";
                DropDownList7.SelectedValue = "2.4\t";
                DropDownList8.SelectedValue = "2.5\t";
                DropDownList9.SelectedValue = "3\t";
                DropDownList10.SelectedValue = "3.1\t";
                DropDownList11.SelectedValue = "3.2\t";
                DropDownList12.SelectedValue = "3.3\t";
                DropDownList13.SelectedValue = "4\t";
                DropDownList14.SelectedValue = "4.1\t";
                DropDownList15.SelectedValue = "4.2\t";
                DropDownList16.SelectedValue = "4.3\t";
                DropDownList17.SelectedValue = "5\t";
                DropDownList18.SelectedValue = "5.1\t";
                DropDownList19.SelectedValue = "5.2\t";
                DropDownList20.SelectedValue = "6\t";
                DropDownList21.SelectedValue = "6.1\t";
                DropDownList22.SelectedValue = "6.2\t";
                DropDownList23.SelectedValue = "7\t";
                DropDownList24.SelectedValue = "7.1\t";
                DropDownList25.SelectedValue = "7.2\t";

                InputTitleZH.Text = "「招攬第64屆澳門格蘭披治大賽車 - 澳門GT盃冠名贊助」 ";

                TextBox1.Text = "為第64屆澳門格蘭披治大賽車- 澳 門GT盃招攬冠名贊助";
                TextBox2.Text = "批准招標人： 社會文化司司長判給人： 社會文化司司長協 議書簽署人： 體育局局長招標實體： 體育局";
                TextBox3.Text = "招標案卷可自公告刊登於《澳門特別行政區公報》之日起，至開標日及開標時間止，於辦公時間內於澳門羅理基博士大馬路818號體育局總部查閱。";
                TextBox4.Text = "組成招標案卷的文件載於總目錄中。";
                TextBox5.Text = "有意投標者可以到第2.2條所述地址索取招標案卷複印本一份或於體育局網頁 (www.sport.gov.mo) ";
                TextBox6.Text = "由 競 投 者 負 責 對 招 標 案 卷 副 本 作 核 實 和 比 較 ， 且 不 妨 礙 七 月 六 日 第63/85/M號法令的規定。 ";
                TextBox7.Text = "對於招標文件的理解存有任何疑問，應最遲於2017年4月19日（星期三）中午十二時前以書面方式送交招標方案 第2.2條所指的體 育局總部， 體育局不接受以郵寄方式遞交的查詢文件。 ";
                TextBox8.Text = "第3.1條所指的疑問，體育局將於5個工作日內以書面方式解答。 ";
                TextBox9.Text = "所有由體育局作出的解答、補充說明及改正的副本將加入招標案卷內，並以公告形式公佈於體育局總部、澳門格蘭披治賽車大樓及上載於體育局網頁（www.sport.gov.mo） 下載區 內免費下載，競投者有義務自行到上述地點查閱。 ";
                TextBox10.Text = "投標書應由競投者或其合法代表於2017年4月24日（星期一）中午十二時前交到體育局總部，不接受以郵寄方式遞交的投標書。逾期遞交的投標書 不被接納。 ";
                TextBox11.Text = "凡投標書內容有違招標案卷的任何規定或條文，或載有限制性條款，不確 實及虛假資料者，一律不予接納。 ";
                TextBox12.Text = "贊助金額應以阿拉伯數目字標出，不得以其他文字或方式表述，否則將導 致其投標書不被接納。";
                TextBox13.Text = "開標儀式將於2017年4月25日（星期二）上午九時三十分，在體育局總部 會議室舉行。 ";
                TextBox14.Text = "在開標會議中，將就投標書是否獲接納作決議：完全符合要求者獲接納、 錯誤可彌補者有條件被接納，但須在二十四小時內彌補該等缺陷；以及根 據法律，錯誤及遺漏無法補正者不予接納。 ";
                TextBox15.Text = "競投者必須已於澳門特別行政區財政局及/或商業及動產登記局作開業及/ 或商業登記。 ";
                TextBox16.Text = "是次招攬贊助的招標不接受以合作經營方式的競投者參與投標。";
                TextBox17.Text = "招 標 方 案 第9條 所 述 的 所 有 文 件 必 須 以 澳 門 特 別 行 政 區 任 一 正 式 語 文 撰 寫，可選用公司專用信箋或A4尺寸的普通紙張打字或電腦印出，也可使用 顏色相同的圓珠筆或墨水筆書寫；必須使用相同的字型或相同書寫筆法及 墨水，同時需字體端正、字跡清晰；不可有塗改、插行、間線或在字上劃 線。";
                TextBox18.Text = "招標方案第9條所述的所有文件必須由容易被理解及有系統的文字、圖表 和資料文件組成。 ";
            }
            else if (type == "Z2")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH6");
                TextBox TextBox7 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH7");
                TextBox TextBox8 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH8");
                TextBox TextBox9 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH9");
                TextBox TextBox10 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH10");
                TextBox TextBox11 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH11");
                DropDownList DropDownList1 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH1");
                DropDownList DropDownList2 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH2");
                DropDownList DropDownList3 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH3");
                DropDownList DropDownList4 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH4");
                DropDownList DropDownList5 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH5");
                DropDownList DropDownList6 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH6");
                DropDownList DropDownList7 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH7");
                DropDownList DropDownList8 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH8");
                DropDownList DropDownList9 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH9");
                DropDownList DropDownList10 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH10");
                DropDownList DropDownList11 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH11");
                DropDownList DropDownList12 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH12");
                DropDownList DropDownList13 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH13");
                DropDownList DropDownList14 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH14");
                DropDownList1.SelectedValue = "1\t";
                DropDownList2.SelectedValue = "1.1\t";
                DropDownList3.SelectedValue = "1.2\t";
                DropDownList4.SelectedValue = "1.3\t";
                DropDownList5.SelectedValue = "1.4\t";
                DropDownList6.SelectedValue = "2\t";
                DropDownList7.SelectedValue = "2.1\t";
                DropDownList8.SelectedValue = "2.2\t";
                DropDownList9.SelectedValue = "2.3\t";
                DropDownList10.SelectedValue = "2.4\t";
                DropDownList11.SelectedValue = "3\t";
                DropDownList12.SelectedValue = "3.1\t";
                DropDownList13.SelectedValue = "3.2\t";
                DropDownList14.SelectedValue = "3.3\t";

                InputTitleZH.Text = "「招攬第64屆澳門格蘭披治大賽車 - 澳門GT盃冠名贊助」 ";

                TextBox1.Text = "獲判給者須遵守招標案卷及協議書的規定。 ";
                TextBox2.Text = "如上款所述的文件有任遺漏之處，均應導守七月六日第 63/85/M 號法 令及澳門特別行政區其他現行適用的法律。 ";
                TextBox3.Text = "獲判給者須切實遵守其他現行及與服務相關的規範。 ";
                TextBox4.Text = "在不影響協議書的規定下，獲判給者須遵守適用於其所提供的服務之 規例、遵守官方機關認可的規格和文件手續，以及生產商或專利實體 的指引。 ";
                TextBox5.Text = "體育局可因下列任何一種情況單方解除協議書，但必須提前以書面方 式通知獲判給者： " + Environment.NewLine +
                    "a) 如獲判給者沒有履行所承擔義務的任何一項或沒有按時履行所承 擔義務； " + Environment.NewLine +
                    "b) 獲判給者未經體育局事先書面許可，將協議書全部或部分義務及 責任轉讓給第三者； " + Environment.NewLine +
                    "c) 獲判給者沒有盡責地履行其所應承擔的義務。";
                TextBox6.Text = "若獲判給者不遵守上述規定而導致體育局單方面解除協議書時，則獲 判給者無權向體育局追回已支付的款項。 ";
                TextBox7.Text = "若獲判給者不遵守協議書的任何規定，體育局有權單方面解除協議書，  沒收獲判給者的確定保證金，這不妨礙判給實體就損失和損害對其提 出的訴訟。 ";
                TextBox8.Text = "雙方協議解除協議書 " + Environment.NewLine +
                    "雙方可透過協議隨時解除協議書，而透過協定解除協議書之效果應在同一協 議內定出。提出解除協議書的一方必須自解除協議書生效之日起計最少提前 三十(30)個工作天以書面方式通知對方。";
                TextBox9.Text = "通知義務： " + Environment.NewLine +
    "               倘獲判給者未能履行或需要更改有助賽事宣傳的其他附加計劃，由獲悉事件 發生日起計 5 日內，獲判給者應以書面向體育局報告，以便體育局在其能力 範圍內採取措施。";
                TextBox10.Text = "執行技術規範： " + Environment.NewLine +
                    "a) 獲判給者當知悉或必須知悉時，在認識到技術規範、體育局的指令、 通告及通知上出現錯漏時，獲判給者應通知體育局；" + Environment.NewLine +
                    "b) 如未有遵守上項之義務，而又證實獲判者故意或過失地以與正常的 工藝規則不符的方式行為時，由此錯漏引致的後果皆由獲判給者負 責；" + Environment.NewLine +
                    "c) 獲判給者是唯一對執行技術規範出現的錯誤或缺漏承擔責任的人。 ";
                TextBox11.Text = "履行贊助協議 " + Environment.NewLine +
                    "獲判給者須按雙方已協議的期限付清贊助的總金額。 ";
            }
            else if (type == "Z3")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH6");
                TextBox TextBox7 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH7");
                TextBox TextBox8 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH8");
                TextBox TextBox9 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH9");
                TextBox TextBox10 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH10");
                TextBox TextBox11 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH11");
                TextBox TextBox12 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH12");
                TextBox TextBox13 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH13");
                TextBox TextBox14 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH14");
                TextBox TextBox15 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH15");
                TextBox TextBox16 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH16");
                TextBox TextBox17 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH17");
                TextBox TextBox18 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH18");
                TextBox TextBox19 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH19");
                TextBox TextBox20 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH20");
                TextBox TextBox21 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH21");
                DropDownList DropDownList1 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH1");
                DropDownList DropDownList2 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH2");
                DropDownList DropDownList3 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH3");
                DropDownList DropDownList4 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH4");
                DropDownList DropDownList5 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH5");
                DropDownList DropDownList6 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH6");
                DropDownList DropDownList7 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH7");
                DropDownList DropDownList8 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH8");
                DropDownList DropDownList9 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH9");
                DropDownList DropDownList10 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH10");
                DropDownList DropDownList11 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH11");
                DropDownList DropDownList12 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH12");
                DropDownList DropDownList13 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH13");
                DropDownList DropDownList14 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH14");
                DropDownList DropDownList15 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH15");
                DropDownList DropDownList16 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH16");
                DropDownList DropDownList17 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH17");
                DropDownList DropDownList18 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH18");
                DropDownList DropDownList19 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH19");
                DropDownList DropDownList20 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH20");
                DropDownList DropDownList21 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH21");
                DropDownList DropDownList22 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH22");
                DropDownList DropDownList23 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH23");
                DropDownList DropDownList24 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH24");
                DropDownList DropDownList25 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH25");
                DropDownList DropDownList26 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH26");
                DropDownList1.SelectedValue = "1\t";
                DropDownList2.SelectedValue = "\t";
                DropDownList3.SelectedValue = "2\t";
                DropDownList4.SelectedValue = "2.1\t";
                DropDownList5.SelectedValue = "2.2\t";
                DropDownList6.SelectedValue = "2.3\t";
                DropDownList7.SelectedValue = "3\t";
                DropDownList8.SelectedValue = "3.1\t";
                DropDownList9.SelectedValue = "3.2\t";
                DropDownList10.SelectedValue = "3.3\t";
                DropDownList11.SelectedValue = "3.4\t";
                DropDownList12.SelectedValue = "4\t";
                DropDownList13.SelectedValue = "4.1\t";
                DropDownList14.SelectedValue = "4.2\t";
                DropDownList15.SelectedValue = "4.3\t";
                DropDownList16.SelectedValue = "4.4\t";
                DropDownList17.SelectedValue = "4.5\t";
                DropDownList18.SelectedValue = "4.6\t";
                DropDownList19.SelectedValue = "4.7\t";
                DropDownList20.SelectedValue = "5\t";
                DropDownList21.SelectedValue = "5.1\t";
                DropDownList22.SelectedValue = "5.2\t";
                DropDownList23.SelectedValue = "5.3\t";
                DropDownList24.SelectedValue = "5.4\t";
                DropDownList25.SelectedValue = "5.5\t";
                DropDownList26.SelectedValue = "5.6\t";

                InputTitleZH.Text = "「招攬第64屆澳門格蘭披治大賽車 - 澳門GT盃冠名贊助」 ";

                TextBox1.Text = "為 2017 年 11 月 16 日至 19 日期間舉辦之第 64 屆澳門格蘭披治大賽車澳門 GT 盃公開招攬贊助。 ";
                TextBox2.Text = "贊助商可獲得“澳門 GT 盃”的冠名權利。 ";
                TextBox3.Text = "賽事名稱為“贊助商名稱”+“賽事名稱”。 ";
                TextBox4.Text = "體育局對於賽事冠名名稱有最終的決定權。 ";
                TextBox5.Text = "商標將會出現在與賽事有關的印刷品及廣告、官方網頁，傳媒中心車手 訪問台背景板以及所贊助的賽事頒獎台背景板等。 ";
                TextBox6.Text = "商標將會出現在參賽車輛於排位起步時宣傳女郎手舉的排位牌上。 ";
                TextBox7.Text = "商標將會出現在所有參與澳門 GT 盃的車輛上的標籤貼紙上。 ";
                TextBox8.Text = "體育局對於賽事車輛上的標籤貼紙位置有最終的決定權。 ";
                TextBox9.Text = "贊助商與其 2 名宣傳女郎均可出席澳門 GT 盃賽事的頒獎台頒獎儀式：  負責頒獎予賽事的第一名、第二名及第三名。 ";
                TextBox10.Text = "贊助商與其 2 名宣傳女郎均可與第 4.1 條所指之澳門 GT 盃賽事的車手 合照。";
                TextBox11.Text = "贊助商可於澳門格蘭披治大賽車官方記者招待會上宣佈其為澳門 GT 盃 冠名贊助，以及於宣傳推廣活動及廣告中使用。 ";
                TextBox12.Text = "贊助商可獲邀出席大賽車的官方活動。 ";
                TextBox13.Text = "贊助費的 10%將會以廣告牌回贈給贊助商，詳細資料載於廣告價目表中， 最終可選擇的廣告位置按當時的銷售情況而定。如贊助商欲放棄廣告牌 之權利，贊助商則可選擇換取相等或不多於回贈價值之賽事門票。 ";
                TextBox14.Text = "贊助商可獲門票及通行證：  門票： 2017 年 11 月 18 日及 19 日大看台 B 門票各 80 張；  通行證： 40 張嘉賓通行證，有關嘉賓通行證適用於 2017 年 11 月    16 日至 19 日期間。";
                TextBox15.Text = "贊助商可獲以 30%折扣優惠購買最多總數 200 張賽事日門票(2017 年 11 月 18 日及 19 日)。 ";
                TextBox16.Text = "贊助商可於大賽車賽程表內刊登廣告 (大賽車賽程表：1 個 1/4 版的廣 告)。 ";
                TextBox17.Text = "贊助商可透過本地及國際媒體進行活動宣傳及推廣。 ";
                TextBox18.Text = "贊助商有權在其市場活動中利用賽事之認可標誌作宣傳，必須事先獲得 體育局的書面許可。 ";
                TextBox19.Text = "所有的廣告宣傳嚴禁附帶有關涉及社會輿論訊息、煙草、色情及鼓吹賭 博成份之內容。此外，有關廣告宣傳內容必須事先獲得體育局的書面許 可方可使用。 ";
                TextBox20.Text = "獲選中的競投者不得更改澳門 GT 盃冠名贊助及賽事名稱。 ";
                TextBox21.Text = "體育局對於賽事冠名名稱有最終的決定權。 ";
            }
            else if (type == "P1")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");

                InputTitleZH.Text = "2016 泛珠三角大學生計算機作品賽";

                TextBox1.Text = "尊敬的副院長:";
                TextBox2.Text = "有關本校電腦學課程在國內進行招生工作的事宜，為直接聯繫褔建廈門的重點中學，從而有針對性地吸納其優質學生，並跟進電腦學課程去年到褔建各中學的宣傳成效，以及推廣本院將於7月份舉行的“2016泛珠三角大學生計算機作品賽總決賽”活動(附件一)，本校擬於2016年6月下旬委派電腦學課程張小弟助理主任、柯韋副教授及李文燁講師，到褔建省廈門一中及廈門雙十中學進行直接招生、專業演講及活動宣傳，以加強與袓國內地院校的交流，為期約6天，具體日期待定(附件二)，呈請副院長批准有關的公幹行為。";
            }
            else if (type == "P2")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH3");

                InputTitleZH.Text = "電腦學課程到福建進行招生宣傳";

                TextBox1.Text = "理事會公鑒：";
                TextBox2.Text = "跟進副院長對 P018/ESAP/2017 建議書(附件一)的批示，本校電腦學課程將於2017年6月24日至29日，委派張小弟副教授聯同招生暨註冊處技術輔導員陳家勤，到副建進行招生宣傳推廣工作，有關的機票及酒店住宿供應事宜，經公共關係辦公室透過Q112/GRP/2017發出5份書面諮詢問價，共收回4份有效報價單(附件二)。";
                TextBox3.Text = "是次公幹的預算為MOP30,650.00，開支的法律依據是5月15日第30/89/M號法令修改之12月15日第122/84/M號法令第8條第1、2、3款及第01REG/CG/2014“澳門理工學院日津貼及啟程津貼規定”第四條及第五條，懇請理事會批准。";
            }
            else if (type == "I1")
            {
                InputTitleZH.Text = "技術暨學術委員會全體會議";


                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1")).Text = "事由: 2015年8月26日(星期三)下午3時正舉行澳門理工學院技術暨學術委員會(技學委)全體會議";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2")).Text = "茲召集閣下出席2015年8月26日下午3時正在本院匯智樓一樓一號演講廳舉行之澳門理工學院技術暨學術委員會全體會議，會議議程如下:";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH3")).Text = "通過2015年5月27日技學委全會會議紀要";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH4")).Text = "選舉技學委常設委員會委員";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH5")).Text = "選舉學術委員會成員 — 語言暨翻譯高等學校代表";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH6")).Text = "报告2015/2016 學年的主要工作";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH7")).Text = "其他事項";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH8")).Text = "會議將提供葡語和英語同聲傳譯服務，記載議程事宜之文件已置於學院總部B102室技術暨學術委員會秘書處及上載於“電子文件管理系統”(https://edms.ipm.edu.mo)，供技術暨學術委員會成員查閱。";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH9")).Text = "特此召集";

                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH3")).SelectedValue = "1\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH4")).SelectedValue = "2\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH5")).SelectedValue = "3\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH6")).SelectedValue = "4\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH7")).SelectedValue = "5\t";
            }
            else if (type == "I2")
            {
                InputTitleZH.Text = "技術暨學術委員會全會會議";

                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1")).Text = "通過2014年9月3日技學委全會會議紀要";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2")).Text = "經出席委員審閱後，2014年9月3日技學委全會會議紀要以75票贊成、0票反對獲得通過。";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH3")).Text = "註: 曾同時出席2014年9月3日及是次會議之委員方具投票權。";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH4")).Text = "主席報告本院已就新校區(澳門大學舊校區五棟大樓)之設計及規劃工作組成工作小組。此外，主校部室外運動場之改建計劃亦已準備就緒，新大樓之規劃以配合全院發展需求為前題，將設有新圖書館及其他學術/科研單位，務求進一步加強本院之學術及科研力量。主席歡迎委員就大樓之應用及規劃提出建議，";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH5")).Text = "教職人員職業道德指引";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH6")).Text = "由於湯翠蘭老師因公務未克出席是次會議，故湯者師以電郵通知技學委秘書處其就題述指引之意見及建議，並由大會秘書代為讀出。";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH7")).Text = "主席表示現今社會各界對教職人員的專業操守極為關注，本澳高等教育相關部門亦有意成立不同的委員會以處理相關事務。主席冀全體教職人員能以高尚的道德情操和敬業的精神去完成本院的教育使命。";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH8")).Text = "陳卓華老師大致認同《教職人員職員道德指引》之內容，惟第一條第一點“職業道德的內涵 – 教學方面”的最後一句列明 — “…教師不得將個人的政治、宗教信仰強加予學生”，然而教師在教授普世價值外，難免需要與學生分享自己的學術觀點。主席指出此句重點在“強加”兩字，本院在制訂題述指訂的同時亦將玫力捍衛教師的權利，故此條款不妨礙平等的學術討論。";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH9")).Text = "主席請技學委秘書處整理委員們意見後送法律顧問研究。";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH10")).Text = "其他事項";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH11")).Text = "大會秘書報告現屆技學委常設委員會選舉委員之任期將於8月份屆滿；另，學術委員會需補選語言暨翻譯高等學校之代表，技學委秘書處將通佑各校提交新一屆候選委員建議名單之安排。";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH12")).Text = "出席委員未提出其他討論事項，主席於下午4時30分宣佈會議結束。";

                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH1")).SelectedValue = "1\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH5")).SelectedValue = "2\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH10")).SelectedValue = "3\t";

            }
            else if (type == "CI1")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");

                InputTitleZH.Text = "事由:邀請出席《環球葡萄牙語第四冊》、《第三屆葡萄牙語教學國際論壇論文集》和《中葡動詞詞典》新書發行儀式";

                TextBox1.Text = "謹訂於2017年6月26日(星期一)上午11時正於澳門理工學院匯智樓一樓1號演講廳舉行《環球葡萄牙語第四冊》、《第三屆葡萄牙語教學國際論壇論文集》和《中葡動詞詞典》新書發行儀式。屆時社會文化司司長譚俊榮博士親臨主禮。誠邀 屆時出席*。";
                TextBox2.Text = "※儀式將以葡語進行。";


            }
            else if (type == "CI2")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");

                InputTitleZH.Text = "第17/PRE/2016號批示";

                TextBox1.Text = "經公共行政高等學校校長建議、聽取技術暨學術委員會於二零一六年四月十五日之意見，以及澳門理工學院理事會於二零一六年四月十八日之決議，現按照社會文化司司長於二零零一年九月二十日在二零零一年九月十八日第61/PRE/01號建議書內所作之批示，以及經十二月六日第469 / 99 / M號訓令核准的《澳門理工學院章程》第十條第一款d)項、第十四條第款i)項及第二十四條第三款及第四款之規定，委任張小弟副教授擔任公共行政高等學校電腦學課程助理課程主任，任期自二零一六年四月十九日起至二零一六年八月十五日止。";
                TextBox2.Text = "二零一六年四月十八日於澳門理工學院";


            }
            else if (type == "CI3")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");


                InputTitleZH.Text = "";

                TextBox1.Text = "鑒於下述各單位的工作需要，現按照經八月二十三日第29/SAAEJ/99號批示核准的，並經第15/2011號行政長官批示、第457/2011號行政長官批示及第12/2014號行政長官批示修改的《澳門理工學院人事章程》(以下簡稱《人事章程》)第23條第1款及第3款的規定，理事會決議於有關單位內設立職務主管及核准以下人員擔任職務主管職位，任期一年，自2015年9月1日至2016年8月31日止。";
                TextBox2.Text = "另外，按照《人事章程》第23條第2款及第3款的規定，理事會決議核准以下擔任職務主管職位的人員除原有薪酬外，每月可額外收取相當於薪俸表一百點的百分之五十的酬金。";


            }
            else if (type == "G1")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH3");

                InputTitleZH.Text = "邀請來澳門出席“世界中葡翻譯大賽比賽”大賽頒奬典禮及翻譯論壇";

                TextBox1.Text = "張辰琳:";
                TextBox2.Text = "感謝你們參加“世界中葡翻譯大賽比賽”!由于 貴隊的參賽作品質量良好，現誠邀 你們于2017年8月8 日(抵達)至11日(離開)期間，莅臨澳門理工學院出席大賽頒奬典禮及翻譯論壇，以加強學術交流。所有擭邀來澳出席的人員均在頒奬禮上擭頒發參賽証書(擭奬隊伍將另發擭奬証書)，以茲表揚。上述期間，你門的宿舍住宿(兩人一間)及標准膳食費用由澳門理工學院負責，每人可擭得往來之旅費津貼澳門幣3600元(約人民币3,000元)。";
                TextBox3.Text = "如有垂詢，請與“世界中葡翻譯大賽比賽”組委會聯絡，電邮：wcptc@ipm.edu.mo。";

            }
            else if (type == "G2")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH6");

                InputTitleZH.Text = "誠邀出任本校電腦學學士學位課程的業界諮詢委員會委員";

                TextBox1.Text = "岑錦棠廳長台鑒：";
                TextBox2.Text = "素仰 閣下見識廣博，學有專精，致力於政府工作，推動資訊及通訊系統現代化，因此，本人謹代表澳門理工學院公共行政高等學校誠意邀請 閣下成為本校電腦學學士學位課程的業界諮詢委員會(以下簡稱『委員會』)的委員，任期為三學年(2017/2018至2019/2020)。";
                TextBox3.Text = "『委員會』成立的主要目的是為電腦學課程的持續發展，提供有價值和相關業界的建議，提升教學質量，培育符合社會及業界需求的畢業生，使其備專業佑識及技能，增加學生在就業市場上競爭優勢，而在進修方面能爭取卓越的成績。";
                TextBox4.Text = "岑廳長具備多年在政府及電腦行業工作的豐富經驗，閣下是『業界諮詢委員會』最佳委員人選，期待 閣下可就課程的專業的戰略方向和發展願景，特別是在研究領域，課程設置及其發展等關鍵課題提出真知灼見，分享寶貴經驗。";
                TextBox5.Text = "感謝您撥冗閱讀本函，並懇請考慮成為我們的電腦學學士課程的業界諮詢委員會成員。隨函附上課程簡介，課程科目以及澳門理工學院“電腦教學與研究”的簡介，以供閣下參考之用。如蒙俯允，不勝銘感，敬候示覆。";
                TextBox6.Text = "查詢及賜覆，煩請與本校電腦學課程主任林燦堂副教授聯絡，電話8599-3342或電郵ctlam@ipm.edu.mo。";

            }
            else if (type == "N1")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH6");

                InputTitleZH.Text = "行政長官崔世安會見澳門工程師學會代表";

                TextBox1.Text = "行政長官崔世安今（8）日在政府總部會見澳門工程師學會會長譚立武一行，就本地工程業界對外科普交流合作、本地青年人才培養，以及促進本地工程師資格認證等進行交流。";
                TextBox2.Text = "行政長官聽取該會介紹會務概況和發展方針，以及學會將於今年11月在澳門舉辦第二屆葡語系工程師大會的情況。行政長官表示，特區政府重視及支持本地工程師業界的發展及培養人才的工作，對於該會希望藉舉辦第二屆葡語系工程師大會的契機，促使澳門成為葡語系國家及內地工程界代表的交流平台，加強業界合作，他表示支持，也預祝大會將取得成功。 ";
                TextBox3.Text = "譚立武表示，澳門工程師學會的成員是來自不同專業範疇的工程技術人員，從事於交通、運輸、大型基建等行業的公、私營機構，同時還有來自學術機構的導師。工程師學會為本地技術人員提供持續進修的機會，以及注重對外的科普交流合作，同時，還承接政府部門科研項目等，努力達致服務澳門和提升業界水平的會務宗旨。";
                TextBox4.Text = "該會對外交流事務部部長田達德稱，將於今年11月在澳門舉辦的第二屆葡語系工程師大會，將邀請多個葡語系國家，連同內地北京、泛珠三角區域的工程師代表及部分國家的相關部長出席。他認為，大會不單從技術層面促進雙方工程業界交流，還具有推動合作及本地經濟發展的意義，希望得到行政長官及特區政府的支持。";
                TextBox5.Text = "會面中，行政長官也聽取了該會就本地工程業界的專業資格認證及青年工程師培訓工作的意見，該會希望提升本澳青年工程師的技術及知識，讓他們有機會參與內地及外地的大型工程項目。";
                TextBox6.Text = "參加會見的包括行政長官辦公室主任譚俊榮、工程師學會理事長胡祖杰、副理事長黃承發及陳桂舜。";

            }
        }

        protected void CHToPT_Click(object sender, EventArgs e)
        {
            string type = Session["Type"].ToString();
            if (type == "Z0")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT6");
                TextBox TextBox7 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT7");
                TextBox TextBox8 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT8");
                TextBox TextBox9 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT9");

                InputTitlePT.Text = "“Angariação de patrocínio para a Taça GT Macau da 64.ª Edição do Grande Prémio de Macau”";

                TextBox1.Text = "Nos termos previstos nos artigos 165.º, n.º 2, 170.º, n.º 1 e 176.º do Código do Procedimento Administrativo, no 13.º do Decreto-Lei n.º 63/85/M, de 6 de Julho, e em conformidade com o Despacho do Ex.mo Senhor Secretário para os Assuntos Sociais e Cultura, de 24 de Março de 2017, o Instituto do Desporto vem proceder, em representação do adjudicante, à abertura do Concurso Público de Angariação de patrocínio para a Taça GT Macau da 64.ª Edição do Grande Prémio de Macau";
                TextBox2.Text = "A partir da data da publicação do presente anúncio, os interessados podem dirigir-se ao balcão de atendimento da sede do Instituto do Desporto, sito na Avenida do Dr. Rodrigo Rodrigues, n.o 818, em Macau, no horário de expediente, das 9.00 às 13.00 e das 14.30 às 17.30 horas, para consulta do Processo do Concurso ou para obtenção da cópia do processo. Podem ainda ser feita a transferência gratuita de ficheiros pela internet na área de Download da página electrónica do Instituto do Desporto: www.sport.gov.mo.";
                TextBox3.Text = "Os interessados devem comparecer na sede do Instituto do Desporto até à data limite para a apresentação das propostas para tomarem conhecimento sobre eventuais esclarecimentos adicionais. ";
                TextBox4.Text = "A sessão de esclarecimento deste Concurso Público terá lugar no dia 11 de Abril de 2017, (terça-feira), pelas 10.00 horas, na sala de reuniões do Edifício do Grande Prémio sito na Avenida da Amizade n.º  207, em Macau. Em caso de encerramento do Instituto do Desporto na data e hora da sessão de esclarecimento acima mencionadas, por motivos de tufão ou por motivos de força maior, a data e hora estabelecidas para a sessão de esclarecimento serão adiadas para a mesma hora do primeiro dia útil seguinte. ";
                TextBox5.Text = "O prazo para a apresentação das propostas termina às 12.00 horas do dia 24 de Abril de 2017 (segunda-feira), não sendo admitidas propostas fora do prazo. Em caso de encerramento do Instituto do Desporto na data e hora limites para a apresentação das propostas acima mencionadas, por motivos de tufão ou por motivos de força maior, a data e a hora limites estabelecidas para a apresentação das propostas serão adiadas para a mesma hora do primeiro dia útil seguinte. Os concorrentes devem apresentar a sua proposta dentro do prazo estabelecido na sede do Instituto do Desporto, no endereço acima referido.";
                TextBox6.Text = "O acto público de abertura das propostas do concurso terá lugar no dia 25 de Abril de 2017 (terça-feira), pelas 9.30 horas, no Auditório da sede do Instituto do Desporto, sito na Avenida do Dr. Rodrigo Rodrigues, n.º 818, em Macau. Em caso de encerramento do Instituto do Desporto na data e hora para o acto público de abertura das propostas, por motivos de tufão ou por motivos de força maior, ou em caso de adiamento da data e hora para a apresentação das propostas, por motivos de tufão ou por motivos de força maior, a data e a hora estabelecidas para o acto público de abertura das propostas serão adiadas para a mesma hora do primeiro dia útil seguinte.";
                TextBox7.Text = "As propostas são válidas durante 90 dias a contar da data da sua abertura. ";
                TextBox8.Text = "Instituto do Desporto, 5 de Abril de 2017. ";
                TextBox9.Text = "O Presidente Substituto, Lau Cho Un. ";

            }
            else if (type == "Z1")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT6");
                TextBox TextBox7 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT7");
                TextBox TextBox8 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT8");
                TextBox TextBox9 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT9");
                TextBox TextBox10 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT10");
                TextBox TextBox11 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT11");
                TextBox TextBox12 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT12");
                TextBox TextBox13 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT13");
                TextBox TextBox14 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT14");
                TextBox TextBox15 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT15");
                TextBox TextBox16 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT16");
                TextBox TextBox17 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT17");
                TextBox TextBox18 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT18");
                DropDownList DropDownList1 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT1");
                DropDownList DropDownList2 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT2");
                DropDownList DropDownList3 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT3");
                DropDownList DropDownList4 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT4");
                DropDownList DropDownList5 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT5");
                DropDownList DropDownList6 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT6");
                DropDownList DropDownList7 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT7");
                DropDownList DropDownList8 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT8");
                DropDownList DropDownList9 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT9");
                DropDownList DropDownList10 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT10");
                DropDownList DropDownList11 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT11");
                DropDownList DropDownList12 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT12");
                DropDownList DropDownList13 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT13");
                DropDownList DropDownList14 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT14");
                DropDownList DropDownList15 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT15");
                DropDownList DropDownList16 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT16");
                DropDownList DropDownList17 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT17");
                DropDownList DropDownList18 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT18");
                DropDownList DropDownList19 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT19");
                DropDownList DropDownList20 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT20");
                DropDownList DropDownList21 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT21");
                DropDownList DropDownList22 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT22");
                DropDownList DropDownList23 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT23");
                DropDownList DropDownList24 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT24");
                DropDownList DropDownList25 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT25");

                DropDownList1.SelectedValue = "1\t";
                DropDownList2.SelectedValue = "\t";
                DropDownList3.SelectedValue = "2\t";
                DropDownList4.SelectedValue = "2.1\t";
                DropDownList5.SelectedValue = "2.2\t";
                DropDownList6.SelectedValue = "2.3\t";
                DropDownList7.SelectedValue = "2.4\t";
                DropDownList8.SelectedValue = "2.5\t";
                DropDownList9.SelectedValue = "3\t";
                DropDownList10.SelectedValue = "3.1\t";
                DropDownList11.SelectedValue = "3.2\t";
                DropDownList12.SelectedValue = "3.3\t";
                DropDownList13.SelectedValue = "4\t";
                DropDownList14.SelectedValue = "4.1\t";
                DropDownList15.SelectedValue = "4.2\t";
                DropDownList16.SelectedValue = "4.3\t";
                DropDownList17.SelectedValue = "5\t";
                DropDownList18.SelectedValue = "5.1\t";
                DropDownList19.SelectedValue = "5.2\t";
                DropDownList20.SelectedValue = "6\t";
                DropDownList21.SelectedValue = "6.1\t";
                DropDownList22.SelectedValue = "6.2\t";
                DropDownList23.SelectedValue = "7\t";
                DropDownList24.SelectedValue = "7.1\t";
                DropDownList25.SelectedValue = "7.2\t";

                InputTitlePT.Text = "“Angariação de patrocínio para a Taça GT Macau da 64.ª Edição do Grande Prémio de Macau”";

                TextBox1.Text = "Angariação de patrocínio para a Taça GT Macau da 64.ª Edição do Grande Prémio de Macau.";
                TextBox2.Text = "Entidade que autoriza a abertura do concurso: Secretário para os Assuntos Sociais e Cultura Adjudicante: Secretário para os Assuntos Sociais e Cultura Outorgante do acordo: Presidente do Instituto do Desporto Entidade que preside ao concurso: Instituto do Desporto ";
                TextBox3.Text = "O processo pode ser consultado na sede do Instituto do Desporto, sito na Avenida do Dr. Rodrigo n.º 818, em Macau, durante as horas de expediente, desde a data da publicação do respectivo anúncio no Boletim Oficial da Região Administrativa e Especial de Macau (RAEM) até ao dia e hora do acto público do concurso. ";
                TextBox4.Text = "Os documentos que instruem o processo são os indicados no Índice Geral.";
                TextBox5.Text = "Os interessados podem dirigir-se à sede do Instituto do Desporto indicada no artigo 2.2 para obterem cópia do processo ou podem obter gratuitamente o ficheiro pela internet na área de Download da página electrónica do Instituto do Desporto: www.sport.gov.mo. ";
                TextBox6.Text = "É da responsabilidade dos concorrentes a verificação e comparação das cópias com os elementos do processo patenteado, sem prejuízo do estipulado no Decreto-Lei n.º 63/85/M, de 6 de Julho. ";
                TextBox7.Text = "Os pedidos de esclarecimento de quaisquer dúvidas relativas à interpretação das peças processuais devem ser apresentados, por escrito, à sede do Instituto do Desporto mencionada no artigo 2.2 do Programa do Concurso, até às 12.00 horas do dia 19 de Abril de 2017, quarta-feira; o Instituto do Desporto não aceita os documentos enviados por correio. ";
                TextBox8.Text = "Os esclarecimentos a que se refere o artigo 3.1 serão prestados pelo Instituto do Desporto, por escrito, no prazo de 5 dias úteis. ";
                TextBox9.Text = "Dos esclarecimentos prestados ou das rectificações procedidas pelo Instituto do Desporto, juntar-se-á cópia aos documentos do Processo de Concurso, procedendo-se à afixação dos mesmos em forma de anúncio na sede do Instituto do Desporto, no Edifício do Grande Prémio e na página electrónica do Instituto do Desporto www.sport.gov.mo para o descarregamento gratuito na área de Download, devendo os concorrentes acederem ou dirigirem-se pessoalmente aos locais supramencionados para efeitos de consulta. ";
                TextBox10.Text = "As propostas devem ser entregues até às 12.00 horas do dia 24 de Abril de 2017 (segunda-feira), pelos concorrentes ou seus representantes legais, na sede do Instituto do Desporto, não sendo aceites os documentos enviados por correio. Não são aceites as propostas apresentadas fora do prazo estipulado. ";
                TextBox11.Text = "Não são aceites as propostas que violam as disposições ou os articulados do Programa do Concurso ou de onde se constem cláusulas restritivas, estimativas imprecisas e falsas. ";
                TextBox12.Text = "O montante de patrocínio deve ser apresentado em numeração árabe, o que significa que não pode ser apresentado por outras formas ou meios, caso contrário, a proposta não será aceite. ";
                TextBox13.Text = "O acto público do concurso realizar-se-á pelas 9.30 horas do dia 25 de Abril de 2017 (terça-feira), no Auditório da sede do Instituto do Desporto. ";
                TextBox14.Text = "No acto público, proceder-se-á à deliberação de admissão ou rejeição das propostas apresentadas. As propostas que satisfazem todos os requisitos exigidos são admitidas às fases subsequentes, as admitidas condicionalmente devem sanar as irregularidades no prazo de 24 horas e as não admitidas por apresentarem erros e omissões que não podem ser supridos nos termos da lei. ";
                TextBox15.Text = "Os concorrentes devem estar inscritos na Direcção dos Serviços de Finanças e/ou Conservatória dos Registos Comercial e Bens Móveis da RAEM para a exploração da sua actividade. ";
                TextBox16.Text = "Os concorrentes não podem concorrer em consórcio ao presente concurso de angariação de patrocínio. ";
                TextBox17.Text = "Todos os documentos mencionados no artigo 9 do Programa do Concurso têm de ser redigidos numa das línguas oficiais da RAEM, dactilografados ou impressos em computador ou escritos com esferográfica ou caneta de mesma cor, de forma clara e legível, em papel timbrado da empresa ou em papel comum (tamanho A4), sem quaisquer rasuras, entrelinhas ou palavras riscadas, sempre com o mesmo tipo de máquina, quando dactilografada, ou com a mesma caligrafia e tinta, se for manuscrita. ";
                TextBox18.Text = "Todos os documentos mencionados no artigo 9 do Programa do Concurso têm de ser constituídos por um conjunto de textos, gráficos e dados que proporcionem uma compreensão fácil. ";
            }
            else if (type == "Z2")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT6");
                TextBox TextBox7 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT7");
                TextBox TextBox8 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT8");
                TextBox TextBox9 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT9");
                TextBox TextBox10 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT10");
                TextBox TextBox11 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT11");
                DropDownList DropDownList1 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT1");
                DropDownList DropDownList2 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT2");
                DropDownList DropDownList3 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT3");
                DropDownList DropDownList4 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT4");
                DropDownList DropDownList5 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT5");
                DropDownList DropDownList6 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT6");
                DropDownList DropDownList7 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT7");
                DropDownList DropDownList8 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT8");
                DropDownList DropDownList9 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT9");
                DropDownList DropDownList10 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT10");
                DropDownList DropDownList11 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT11");
                DropDownList DropDownList12 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT12");
                DropDownList DropDownList13 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT13");
                DropDownList DropDownList14 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT14");
                DropDownList1.SelectedValue = "1\t";
                DropDownList2.SelectedValue = "1.1\t";
                DropDownList3.SelectedValue = "1.2\t";
                DropDownList4.SelectedValue = "1.3\t";
                DropDownList5.SelectedValue = "1.4\t";
                DropDownList6.SelectedValue = "2\t";
                DropDownList7.SelectedValue = "2.1\t";
                DropDownList8.SelectedValue = "2.2\t";
                DropDownList9.SelectedValue = "2.3\t";
                DropDownList10.SelectedValue = "2.4\t";
                DropDownList11.SelectedValue = "3\t";
                DropDownList12.SelectedValue = "3.1\t";
                DropDownList13.SelectedValue = "3.2\t";
                DropDownList14.SelectedValue = "3.3\t";

                InputTitlePT.Text = "“Angariação de patrocínio para a Taça GT Macau da 64.ª Edição do Grande Prémio de Macau”";

                TextBox1.Text = "O adjudicatário deve cumprir o estipulado no Processo de Concurso e no acordo. ";
                TextBox2.Text = "Em tudo o que estiver omisso nos documentos referidos no número anterior, observar-se-á o disposto no Decreto-Lei n.º 63/85/M, de 6 de Julho, e na restante legislação aplicável da RAEM. ";
                TextBox3.Text = "O adjudicatário deve cumprir também outra legislação em vigor relacionada com os serviços prestados. ";
                TextBox4.Text = "O adjudicatário deve ainda respeitar outras disposições aplicáveis aos serviços prestados, respeitar os formatos e formalidades documentais exigidos pelas entidades públicas e instruções de eventuais fabricantes de bens ou de entidades de titulares dos direitos patenteados, sem prejuízo das disposições do acordo. ";
                TextBox5.Text = "O Instituto do Desporto pode rescindir unilateralmente o acordo devido a quaisquer das seguintes situações, desde que notifique, por escrito, com antecedência o adjudicatário: " + Environment.NewLine +
                    "a) O adjudicatário deixe de cumprir com qualquer uma das obrigações a que ficou vinculado ou deixe de as cumprir atempadamente; " + Environment.NewLine +
                    "b) O adjudicatário transfira para um terceiro, sem prévio consentimento escrito do Instituto do Desporto, a totalidade ou parte dos deveres e obrigações contratuais a que está obrigado; " + Environment.NewLine +
                    "c) O adjudicatário cumpra de forma defeituosa as obrigações a que está vinculado. ";
                TextBox6.Text = "Perante casos de rescisão unilateral por parte do Instituto do Desporto devido à violação dos artigos anteriores pelo adjudicatário, este não tem o direito de exigir ao Instituto do Desporto o reembolso das despesas entretanto efectuadas.";
                TextBox7.Text = "Caso o adjudicatário não cumpra quaisquer disposições contratuais, o Instituto do Desporto pode rescindir unilateralmente o contrato, confiscar a caução definitiva, sem prejuízo das acções que a entidade adjudicante entenda dever instaurar-lhe por perdas e danos.";
                TextBox8.Text = "Rescisão do acordo por mútuo consentimento" + Environment.NewLine +
                    "As partes podem, por mútuo consentimento e em qualquer momento, resolver o acordo, devendo os efeitos de tal resolução ser fixados no mesmo acordo. A parte que toma a iniciativa deve informar a outra parte por escrita com uma antecedência mínima de 30 dias úteis sobre a data para a produção de efeitos da resolução.";
                TextBox9.Text = "Dever de comunicação: " + Environment.NewLine +
                    "O adjudicatário deve informar por escrito o Instituto do Desporto, caso não for possível cumprir o plano adicional para a promoção da Corrida ou necessita de introduzir alterações sobre o plano, no prazo de 5 dias a partir da data da ocorrência, para que o Instituto do Desporto adopte as medidas necessárias;";
                TextBox10.Text = "Implementação das Normas Técnicas: " + Environment.NewLine +
                    "a) Assim que tiver conhecimento ou for informado, o adjudicatário deve comunicar ao Instituto do Desporto quaisquer erros ou omissões que julgue existirem nas Normas Técnicas, bem como nas ordens, avisos e nas notificações do Instituto do Desporto; " + Environment.NewLine +
                    "b) A falta de cumprimento da obrigação estabelecida na alínea anterior torna o adjudicatário responsável pelas consequências do erro ou da omissão, se se provar que agiu com dolo ou negligência incompatível com o normal conhecimento das regras de arte; " + Environment.NewLine +
                    "c) O adjudicatário é o único responsável pelos erros e omissões na execução das Normas Técnicas.";
                TextBox11.Text = "Cumprimento do acordo do patrocínio " + Environment.NewLine +
                    "O adjudicatário necessita de efectuar o valor total de patrocínio dentro do prazo acordado pelas ambas as partes. ";
            }
            else if (type == "Z3")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT6");
                TextBox TextBox7 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT7");
                TextBox TextBox8 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT8");
                TextBox TextBox9 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT9");
                TextBox TextBox10 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT10");
                TextBox TextBox11 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT11");
                TextBox TextBox12 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT12");
                TextBox TextBox13 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT13");
                TextBox TextBox14 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT14");
                TextBox TextBox15 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT15");
                TextBox TextBox16 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT16");
                TextBox TextBox17 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT17");
                TextBox TextBox18 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT18");
                TextBox TextBox19 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT19");
                TextBox TextBox20 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT20");
                TextBox TextBox21 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT21");
                DropDownList DropDownList1 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT1");
                DropDownList DropDownList2 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT2");
                DropDownList DropDownList3 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT3");
                DropDownList DropDownList4 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT4");
                DropDownList DropDownList5 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT5");
                DropDownList DropDownList6 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT6");
                DropDownList DropDownList7 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT7");
                DropDownList DropDownList8 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT8");
                DropDownList DropDownList9 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT9");
                DropDownList DropDownList10 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT10");
                DropDownList DropDownList11 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT11");
                DropDownList DropDownList12 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT12");
                DropDownList DropDownList13 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT13");
                DropDownList DropDownList14 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT14");
                DropDownList DropDownList15 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT15");
                DropDownList DropDownList16 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT16");
                DropDownList DropDownList17 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT17");
                DropDownList DropDownList18 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT18");
                DropDownList DropDownList19 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT19");
                DropDownList DropDownList20 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT20");
                DropDownList DropDownList21 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT21");
                DropDownList DropDownList22 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT22");
                DropDownList DropDownList23 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT23");
                DropDownList DropDownList24 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT24");
                DropDownList DropDownList25 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT25");
                DropDownList DropDownList26 = (DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT26");
                DropDownList1.SelectedValue = "1\t";
                DropDownList2.SelectedValue = "\t";
                DropDownList3.SelectedValue = "2\t";
                DropDownList4.SelectedValue = "2.1\t";
                DropDownList5.SelectedValue = "2.2\t";
                DropDownList6.SelectedValue = "2.3\t";
                DropDownList7.SelectedValue = "3\t";
                DropDownList8.SelectedValue = "3.1\t";
                DropDownList9.SelectedValue = "3.2\t";
                DropDownList10.SelectedValue = "3.3\t";
                DropDownList11.SelectedValue = "3.4\t";
                DropDownList12.SelectedValue = "4\t";
                DropDownList13.SelectedValue = "4.1\t";
                DropDownList14.SelectedValue = "4.2\t";
                DropDownList15.SelectedValue = "4.3\t";
                DropDownList16.SelectedValue = "4.4\t";
                DropDownList17.SelectedValue = "4.5\t";
                DropDownList18.SelectedValue = "4.6\t";
                DropDownList19.SelectedValue = "4.7\t";
                DropDownList20.SelectedValue = "5\t";
                DropDownList21.SelectedValue = "5.1\t";
                DropDownList22.SelectedValue = "5.2\t";
                DropDownList23.SelectedValue = "5.3\t";
                DropDownList24.SelectedValue = "5.4\t";
                DropDownList25.SelectedValue = "5.5\t";
                DropDownList26.SelectedValue = "5.6\t";

                InputTitlePT.Text = "“Angariação de patrocínio para a Taça GT Macau da 64.ª Edição do Grande Prémio de Macau”";

                TextBox1.Text = "Angariação de patrocínio para a Taça GT Macau da 64.ª Edição do Grande Prémio de Macau, a realizar nos dias 16 a 19 de Novembro de 2017. ";
                TextBox2.Text = "O patrocinador pode obter o Título de patrocínio da Taça GT Macau. ";
                TextBox3.Text = "A corrida é identificada com “Nome do Patrocinador” + “Nome da corrida”. ";
                TextBox4.Text = "O Instituto do Desporto tem o direito da decisão final sobre a designação do Título de patrocínio da corrida. ";
                TextBox5.Text = "O logótipo do patrocinador é identificado em materiais impressos e anúncios do evento, na página electrónica oficial, no pano de fundo do palco de entrevistas no Centro de Imprensa e no pano de fundo do pódio da corrida patrocinada. ";
                TextBox6.Text = "O logótipo do patrocinador é identificado nas placas exibidas por “PR Girls”, na grelha de partida.";
                TextBox7.Text = "O logótipo do patrocinador é identificado nos autocolantes colados nos veículos de corrida da Taça GT Macau.";
                TextBox8.Text = "O Instituto do Desporto tem o direito da decisão final sobre o local de colocação dos autocolantes colados nos veículos das Corridas.";
                TextBox9.Text = "O patrocinador e 2 das suas “PR Girls” podem participar na entrega dos prémios no pódio da corrida da Taça GT Macau:  Entrega de prémios aos 1.º, 2.º e 3.º classificados da Corrida. ";
                TextBox10.Text = "O patrocinador e 2 das suas “PR Girls” podem integrar a foto de grupo com os pilotos da corrida da Taça GT Macau mencionado no artigo 4.1. ";
                TextBox11.Text = "O patrocinador pode anunciar o patrocínio da Taça GT Macau durante a conferência de imprensa oficial do Grande Prémio de Macau do Evento, podendo utilizar o título de patrocinador nas actividades e anúncios promocionais do evento. ";
                TextBox12.Text = "O patrocinador pode participar nos eventos oficiais do Grande Prémio de Macau. ";
                TextBox13.Text = "Dez por cento (10%) do valor do patrocínio será deduzido ao patrocinador através de placas de publicidade, os detalhes constam da lista de Preços de Publicidade, sendo a escolha dos espaços publicitários depende da situação real de venda na altura. Caso o patrocinador renunciar o direito de placas de publicidade, em alternativa, pode o patrocinador obter a bonificação pelo mesmo valor monetário em bilhetes.";
                TextBox14.Text = "O patrocinador tem direito a bilhetes e passes:  Bilhetes: 80 unidades por dia para a Tribuna B, nos dias 18 e 19 de Novembro de 2017;  Passes:  40 unidades para utilização durante o período de 16 a 19 de Novembro de   2017. ";
                TextBox15.Text = "O patrocinador tem direito a 30% de desconto na aquisição de um total de 200 bilhetes para os dias das provas (nos dia 18 e 19 de Novembro de 2017). ";
                TextBox16.Text = "O patrocinador tem direito a publicar anúncio no Programa Oficial do Grande Prémio (1 anúncio com tamanho de 1/4 página). ";
                TextBox17.Text = "O patrocinador pode através da Imprensa local e internacional divulgar as actividades e publicar anúncios promocionais do evento.";
                TextBox18.Text = "O patrocinador tem direito ao uso do logótipo oficial da corrida nas suas próprias actividades de comercialização, com prévia aprovação escrita do Instituto do Desporto. ";
                TextBox19.Text = "É estritamente proibida a inclusão da opinião pública e de mensagens relacionadas ao tabaco, à pornografia ou que encorajam a prática de jogos de fortuna e azar em todas as actividades publicitárias. A utilização de conteúdos publicitários depende de prévia aprovação escrita do Instituto do Desporto.";
                TextBox20.Text = "O adjudicatário não pode alterar o Título de patrocínio da Taça GT Macau nem o Título da Corrida.";
                TextBox21.Text = "O Instituto do Desporto tem o direito da decisão final sobre o Título de Patrocínio da Corrida.";
            }
            else if (type == "P1")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");

                InputTitlePT.Text = "2016 alunos competição Pan-pérola Projeto Delta do Rio";

                TextBox1.Text = "Caro vice-presidente:";
                TextBox2.Text = "Questões relacionadas com o computador da escola cursos de ciências de inscrição no país, está diretamente ligada à chave de ensino médio em Xiamen, Fu Jian, e, assim, alvo de atrair a qualidade de seus alunos, e para acompanhar cursos de ciência da computação nas escolas secundárias no ano passado, efeito de propaganda Fu Jian, e Tribunal vai promover o evento \"2016 finais da competição Pan-Pearl River Projeto Delta Estudantes\", realizada em julho (anexo I), a escola tem a intenção de final de junho de 2016, diretor-assistente nomeado do currículo de ciência da computação Zhang Xiaodi, e Ke Wei Li Ye, um lecturer professor associado , de Fu Jian Província, Xiamen Shuangshi ensino médio e Xiamen em uma admissão direta, apresentações profissionais e atividades promocionais para aumentar intercâmbios e Cho para as instituições domésticas por um período de cerca de 6 dias, data a ser anunciada (anexo II), vice-presidente de petição aprovação de conduta de negócios.";
            }
            else if (type == "P2")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT3");

                InputTitlePT.Text = "Cursos de informática Fujian para recrutar estudantes para propaganda";

                TextBox1.Text = "Conselho opinião pública:";
                TextBox2.Text = "Siga vice-presidente instruções de P018 / ESAP / Recomendação 2017 (Anexo I), os cursos de informática escola de Junho de 2017 24 a 29, Zhang Xiaodi nomeado professor associado em conjunto com o conselheiro admissões e Registry tecnológico Chen Jiaqin, vice-recrutar estudantes para construir publicidade e promoção de trabalho, passagens aéreas e assuntos alojamento em hotel de abastecimento, enviou 5 escrita preço consulta perguntando através Q112 / GRP / 2017 pelo escritório de relações públicas, foram devolvidos 4 citações válidos (Anexo II).";
                TextBox3.Text = "O orçamento é o momento para MOP30,650.00 negócio, é a base legal para o Decreto gastos 30/89 / M Nenhuma modificação das Maio 15 122/84 / M, 2, 3 Artigo 8.º 15 dez e seção de 01REG / CG / 2014 \"do Instituto Politécnico de Macau diem e disposições partida\" artigos 4 e 105, instar o Conselho para aprovação.";
            }
            else if (type == "I1")
            {
                InputTitlePT.Text = "Técnica e Acadêmica Comissão da Assembleia Whole";

                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1")).Text = "Fato: (quarta-feira) 15:00 26 de agosto de 2015 a ser realizada no Comité Instituto de Tecnologia & Academic do Politécnico de Macau";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2")).Text = "Você convocados para participar de Comitê de Tecnologia Institute e Académica do Politécnico de Macau está sendo realizada no primeiro andar do hospital sábio Teatro Palestra na 3:26 em agosto 2015 Quantikuaiyi tarde, a agenda é a seguinte:";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT3")).Text = "Pelo comitê de escola técnica Plenária Meeting Minutes 27 de maio de 2015";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT4")).Text = "membros do Comitê Permanente do Comitê Eleição de Estudos Técnicos";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT5")).Text = "Eleição dos membros do Conselho Académico - em nome de Línguas e Tradução Superior";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT6")).Text = "O relatório de trabalho principal 2015 ano escolar";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT7")).Text = "outros assuntos";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT8")).Text = "A conferência irá proporcionar Português de língua e Inglês serviço de interpretação simultânea, agenda documentado de questões foi colocada secretariado Sede do Instituto e Academic Comitê Técnico B102 quarto e carregado para o \"sistema de gerenciamento eletrônico de documentos\" (https://edms.ipm.edu. MO), um membro da Comissão para a revisão técnica e acadêmica.";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT9")).Text = "convoco";



                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH3")).SelectedValue = "1\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH4")).SelectedValue = "2\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH5")).SelectedValue = "3\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH6")).SelectedValue = "4\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH7")).SelectedValue = "5\t";
            }
            else if (type == "I2")
            {

                InputTitlePT.Text = "As reuniões plenárias do Comitê Técnico e Acadêmico";

                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1")).Text = "Pelo comitê de escola técnica Plenária Meeting Minutes 03 de setembro de 2014";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2")).Text = "Depois de assistir o comitê analisou as atas da sessão plenária Comissão em setembro de aprender habilidades 3 de Maio de 2014 por 75 votos a favor, 0 votos contra.";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT3")).Text = "Nota: uma vez que, enquanto freqüentava a 03 de setembro de 2014 e é membro com direito a voto reunião do partido.";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT4")).Text = "Presidente do Tribunal tem sido relatada (campus de idade, da Universidade de Macau Wudongtailou) a concepção e planejamento de um grupo de trabalho sobre o novo campus. Além disso, o parque infantil exterior do departamento de escola primária também foi reconstruir plano está pronto, o planejamento do novo edifício para atender as necessidades de desenvolvimento do hospital para a ex-título, contará com a nova biblioteca e outras instituições acadêmicas / pesquisa, para garantir maior reforço escolar e hospitalar capacidade de I & D. O Presidente congratulou-se com os membros para fazer recomendações sobre a aplicação de construção e planejamento,";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT5")).Text = "Faculdade diretrizes de ética profissional";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT6")).Text = "Desde as gramas de professores Tang Cuilan devido ao negócio não participar da reunião, assim secretariado-mail Comitê Técnico da divisão sopa de saber as suas opiniões sobre o título das orientações e recomendações do Secretário do Congresso lido em seu nome.";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT7")).Text = "O Presidente disse hoje a comunidade de conduta profissional do clero eram extremamente preocupados departamentos, relevantes de ensino superior em Macau também pretende configurar diferentes comitês para tratar de assuntos relacionados. Presidente Ji professores, funcionários capazes de elevado caráter moral e espírito de dedicação para cumprir a missão educativa do hospital.";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT8")).Text = "professor Chen Zhuohua geralmente reconhecido \"orientação docentes moral\" do conteúdo, mas a primeira ao primeiro ponto, \"a conotação de ética profissional - ensino\" estabelecido na última frase -\"...os professores devem política não pessoais, religião imposta aos estudantes \"no entanto, os professores Professor de valores universais, a inevitável necessidade de compartilhar com os alunos o seu ponto de vista acadêmico. O presidente ressaltou que o foco da frase \"imposta\" deveria, o Tribunal, ao formular o título refere-se ao conjunto de direitos também irá forçar rosa para defender os professores, discussão acadêmica, portanto, sem prejuízo dos termos de igualdade.";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT9")).Text = "Consultor jurídico Presidente solicitou à Secretaria a organizar os membros da comissão escolar das evacuações comitê.";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT10")).Text = "outros assuntos";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT11")).Text = "Secretário de informar o termo eleição para a Assembleia do mandato dos membros do Comité Permanente do mandato em curso Comitê Escola Técnica irá expirar em agosto, o outro, representantes das eleições para o Conselho Acadêmico de Línguas e faculdades de tradução e universidades necessidades, secretariado da comissão escola técnica será submetida através dos novos membros candidatos de Maria Auxiliadora da escola cristãos lista do arranjo proposto.";
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT12")).Text = "Outros membros presentes não propôs a discutir negócios, o Presidente em 4 pm 30 minutos e terminou a reunião.";

                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT1")).SelectedValue = "1\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT5")).SelectedValue = "2\t";
                ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT10")).SelectedValue = "3\t";

            }
            else if (type == "CI1")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");

                InputTitlePT.Text = "Assunto: convidados a participar do \"global Livro IV Português\", \"Proceedings Fórum do ensino Terceira Internacional Português\" e \"dicionário verbo Sino - Português\" cerimônia de lançamento do livro";

                TextBox1.Text = "Tenho a honra está programado para realizar regular \"global Livro IV Português\", \"O Terceiro Fórum Internacional de ensino Português no Instituto Politécnico de Macau sábio no primeiro andar leitura salão No. 1(segunda - feira) 11:00 26 jun 2017 set \"e\" dicionário Sino-Português verbo \"cerimônia de lançamento do livro. Até então, o Dr. Tan Junrong Assuntos Sociais e Cultura em pessoa oficiante. * Convidado vai participar.";
                TextBox2.Text = "※ cerimônia de língua Português será realizada.";


            }
            else if (type == "CI2")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");

                InputTitlePT.Text = "Despacho n. 17/PRE/2016";

                TextBox1.Text = "Faculdades e universidades públicas recomendados pelo diretor administrativo, ouvir os pontos de vista e Comissão Técnica Acadêmica em 15 de abril, 2016, o Instituto Politécnico de Macau e ao Conselho sobre 18 abril de 2016 da resolução, de acordo com sociais existentes Secretário cultura em 20 de setembro de 2001 18 setembro de 2001, no primeiro 61 / PRE / 01Recomendações feitas no número de instruções, e o termo \" Instituto Politécnico de Macau estatuto\" Artigo 10, parágrafo d) até 6 de Dezembro de 469/99 / M Nº Ação aprovado, o artigo 14, inciso alínea i) e n.os 3 e 4 do artigo 24 prevê que a nomeação de Zhang Xiaodi serviu como professor associado de instituições da administração pública de ensino superior cursos de informática Director do programa Adjunto, por um período começando 19 abril de 2016 até 2016 em 15 de agosto somente.";
                TextBox2.Text = "Abril 18, 2016 no Instituto Politécnico de Macau";


            }
            else if (type == "CI3")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");

                InputTitlePT.Text = "";

                TextBox1.Text = "Tendo em vista a necessidade de trabalhar após várias unidades, agora a agosto, de acordo com a secção 29 / SAAEJ no dia 23/99 instruções aprovadas pelas instruções e No. 15/2011 do Chefe do Executivo, Chief Executive Despacho n.º 457/2011 segunda Despacho do chefe do Executivo n.º 12/2014 modificar os \"Estatutos do Instituto Politécnico de Macau de pessoal\" (doravante referido como \"pessoal estatuto\") as disposições do parágrafo 3 do artigo 23.º, n.º 1 e resoluções estabelecidas pelo Conselho e aprovado pelo órgão competente nas unidades relevantes da seguinte cargos executivos que detêm cargo por um período de um ano, a partir de 1 de setembro de 2015 a 31 de Agosto, de 2016 somente.";
                TextBox2.Text = "Além disso, nos termos do artigo 23, n.º 2 e 3 do presente artigo \"pessoal estatuto\", a seguinte resolução aprovada pelo pessoal do Conselho ocupou o cargo de cargos executivos, além do salário original, o custo adicional é equivalente a um salário mensal de 100 pontos mesa cinqüenta por cento da remuneração.";


            }
            else if (type == "G1")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT3");

                InputTitlePT.Text = "Macau convidados a participar da \"tradução Sino-Português World Series jogo\" cerimônia de premiação do concurso e Fórum Tradução";

                TextBox1.Text = "Zhang Chen Lin:";
                TextBox2.Text = "Durante a (esquerda) Obrigado por participar no \"jogo da World Series Sino - Português tradução\"! Como a boa qualidade das obras participando de sua equipe, você está convidado para a 08 agosto de 2017 (chegada) a 11, vir a participar da premiação do concurso do Instituto Politécnico de Macau cerimônia e fórum de tradução para fortalecer o intercâmbio acadêmico. Hu convidados a participar todos os funcionários australianos estavam na cerimônia, Hu emitidos certificados de participação (Wo Wo equipa Award vai enviar outro certificado do prêmio), certificados de reconhecimento. O período acima mencionado, a porta do dormitório de sua estadia (ambos a) e Standard Board no comando do Instituto Politécnico de Macau, Hu pode ter que entrar em contato com o subsídio de viagem de MOP 3.600 yuan (cerca de 3.000 RMB yuan) por pessoa.";
                TextBox3.Text = "Para mais informações, entre em contato com o \"jogo Sino-Português tradução World Series\", o comitê organizador, e-mail: wcptc@ipm.edu.mo.";

            }
            else if (type == "G2")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT6");

                InputTitlePT.Text = "Os membros do Comité Consultivo da indústria são convidados para a escola como programas de graduação de ciência da computação";

                TextBox1.Text = "Cen Jintang Kam Taiwan Diretor:";
                TextBox2.Text = "Su Yang Excelência experiente, de especialistas, dedicado ao trabalho do governo, para promover a modernização dos sistemas de informação e comunicação, portanto, gostaria em nome do Instituto Politécnico de Macau de escolas de administração pública e universidades cordialmente convidado a se tornar Comité Consultivo da indústria sobre os programas de graduação computador ciência da universidade ( membros a seguir denominado \"o Comité\") por um período de três anos lectivos (2017/2018 a 2019/2020).";
                TextBox3.Text = "O principal objetivo do \"Comitê\" set up para o contínuo desenvolvimento do currículo de ciência da computação, e fornecer recomendações valiosas relacionadas com a indústria, melhorar a qualidade do ensino, graduados alimentando atender as necessidades da sociedade e da indústria, tem sido profissionalmente woo conhecimentos e habilidades para aumentar estudante vantagem competitiva no mercado de trabalho, mas em termos de aprendizagem pode lutar por excelentes resultados.";
                TextBox4.Text = "diretor Cen com muitos anos de experiência trabalhando na indústria de computadores e do governo, que são os melhores membros do candidato \"Comité Consultivo Indústria\", você pode olhar para a frente para um currículo profissional do direcionamento estratégico e visão, especialmente no campo da pesquisa, currículo e as idéias-chave de desenvolvimento de emissão proposto, compartilhar uma experiência valiosa.";
                TextBox5.Text = "Obrigado por ter tempo para ler esta carta, e instar a indústria a considerar se tornar um membro da nossa Advisory Board Bachelor de computador cursos de ciências. Fechado Introdução Descrição do curso, disciplinas curriculares e Instituto Politécnico de Macau \"ensino computador e pesquisa\", para sua referência. Os Mongólia dignou, ser grato, tampa Jinghou mostrado.";
                TextBox6.Text = "Consulta e responder por, por favor Lin Cantang, professor associado e diretor do contato universitários cursos de ciência da computação, telefone 8599-3342 ou ctlam@ipm.edu.mo e-mail.";

            }
            else if (type == "N1")
            {
                TextBox TextBox1 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT1");
                TextBox TextBox2 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT2");
                TextBox TextBox3 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT3");
                TextBox TextBox4 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT4");
                TextBox TextBox5 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT5");
                TextBox TextBox6 = (TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT6");

                InputTitlePT.Text = "Chefe do Executivo recebe representantes da Associação dos Engenheiros de Macau(Tradução GCS)";

                TextBox1.Text = " Chefe do Executivo, Chui Sai On, teve, hoje (8 de Abril) na sede do Governo, um encontro com representantes e o presidente da Associação dos Engenheiros de Macau, Tam Lap Mou, para troca de impressões sobre intercâmbio e cooperação do sector de engenharia de Macau com o exterior na generalização da ciência, formação de quadros jovens locais e impulsionar o regime de acreditação de engenheiros locais.";
                TextBox2.Text = "Na ocasião, foi apresentada a situação actual e desenvolvimento da associação e ainda a organização da segunda edição do congresso de engenheiros lusófonos em Macau, no próximo mês de Novembro. Por sua vez, Chui Sai On afirmou que o governo dá importância e apoia o desenvolvimento do sector, bem como à formação de quadros jovens locais nesta área. Manifestou ainda o apoio à organização do referido congresso, que pode promover Macau como plataforma de intercâmbio entre os países de língua portuguesa e o interior da China, reforçar a cooperação no sector e fez votos de sucesso para a realização do referido congresso. ";
                TextBox3.Text = "Entretanto, o presidente da Associação dos Engenheiros de Macau, Tam Lap Mou, referiu que os membros da associação são engenheiros e técnicos de diferentes áreas que trabalham no sector privado ou no público, designadamente na área dos transportes, tráfego, infra-estruturas e ainda docentes em instituições académicas. Adiantou que a Associação criará mais oportunidades de formação contínua para os técnicos locais, prestando atenção ao intercâmbio e cooperação com o exterior na generalização da ciência, referiu a recepção de projectos de investigação nas áreas das ciências entregues pelo governo, acções que ajudam a associação a desenvolver o objectivo de servir Macau e elevar o nível do sector.";
                TextBox4.Text = "Ainda durante o encontro, o presidente da Divisão de Relações Públicas e Comunicação, António Trindade, lembrou que, no próximo mês de Novembro, realiza-se a segunda edição do congresso de engenheiros lusófonos em Macau, para o qual vão ser convidados representantes de engenheiros dos países de língua portuguesa, de Pequim e das zonas do Delta do Rio das Pérolas, bem como ministros da tutela de alguns países. Acrescentou que o congresso vai impulsionar a troca de experiências técnicas do sector e também a cooperação e o desenvolvimento da economia local. Fez votos para que o Chefe do Executivo e o governo apoiem esta iniciativa.";
                TextBox5.Text = "A associação aproveitou ainda para apresentar ao Chefe do Executivo as opiniões sobre a acreditação de profissionais do sector de engenharia e dos trabalhos de formação dos jovens engenheiros para melhorar os conhecimentos e técnicas dos jovens engenheiros de Macau, a fim de lhes dar oportunidade para participarem em grandes projectos quer no país, quer no exterior.";
                TextBox6.Text = "No encontro estiveram ainda presentes o chefe do Gabinete do Chefe do Executivo, Alexis Tam, o presidente da direcção e os vice-presidentes da Associação de Engenheiros de Macau, respectivamente, Wu Chou Kit, Wong Seng Fat e Chan Kuai Song.";

            }
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
            var doc = DocX.Load(Server.MapPath("..\\Template") + "\\" + table + ".docx");


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

            doc.ReplaceText("Table", txtTypeZH);
            doc.ReplaceText("Title", InputTitleZH.Text);

            if (table == "P")
            {
                doc.ReplaceText("Type",CodeToString(type));
                doc.ReplaceText("Time", txtDatePicker.Text);
                doc.ReplaceText("Code", FileName.Text);


                String[] zhData = new String[x];

                int lblzh = 1;
                int txtzh = 1;
                for (int i = 1; i <= 3&&i<=x; i++)
                {
                    String rowZHSQL = "Select Row" + i + " From " + table + "Table Where Language = 'ZH' AND Type = '" + type + "ZH'";
                    System.Data.SqlClient.SqlCommand rowZHCommand = new System.Data.SqlClient.SqlCommand(rowZHSQL, connection);
                    string rowZHString = Convert.ToString(rowZHCommand.ExecuteScalar());

                    if (rowZHString.Equals("TextBox"))
                    {
                        if (ListPointCB.Checked) { doc.ReplaceText("Paragraph"+i, ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        else { doc.ReplaceText("Paragraph"+i,((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        zhData[i - 1] = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh++)).Text;
                    }
                    else
                    {
                        doc.InsertParagraph();
                        if (ListPointCB.Checked)
                        { doc.ReplaceText("Paragraph" + i, ((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text); }
                        else { doc.ReplaceText("Paragraph" + i, ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text); }
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
                        if (ListPointCB.Checked) { doc.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        else { doc.InsertParagraph(((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        zhData[i - 1] = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh++)).Text;
                    }
                    else
                    {
                        doc.InsertParagraph();
                        if (ListPointCB.Checked)
                        { doc.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text).Bold(); }
                        else { doc.InsertParagraph(((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text).Bold(); }
                        zhData[i - 1] = ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh++)).Text;
                    }
                }
            }
            else
            {
                String[] zhData = new String[x];

                int lblzh = 1;
                int txtzh = 1;
                for (int i = 1; i <= x; i++)
                {
                    String rowZHSQL = "Select Row" + i + " From " + table + "Table Where Language = 'ZH' AND Type = '" + type + "ZH'";
                    System.Data.SqlClient.SqlCommand rowZHCommand = new System.Data.SqlClient.SqlCommand(rowZHSQL, connection);
                    string rowZHString = Convert.ToString(rowZHCommand.ExecuteScalar());
                    if (rowZHString.Equals("TextBox"))
                    {
                        if (ListPointCB.Checked) { doc.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        else { doc.InsertParagraph(((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh)).Text); }
                        zhData[i - 1] = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputZH" + txtzh++)).Text;
                    }
                    else
                    {
                        doc.InsertParagraph();
                        if (ListPointCB.Checked)
                        { doc.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropZH" + i)).Text + ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text).Bold(); }
                        else { doc.InsertParagraph(((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh)).Text).Bold(); }
                        zhData[i - 1] = ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelZH" + lblzh++)).Text;
                    }
                }
            }

            //var TitleLineFormat = new Novacode.Formatting();
            //TitleLineFormat.Size = 16D;
            //TitleLineFormat.Bold = true;

            //var LineFormat = new Novacode.Formatting();
            //LineFormat.Position = 12;

            //var HeaderLineFormat = new Novacode.Formatting();
            //HeaderLineFormat.Size = 12D;
            //HeaderLineFormat.FontColor = Color.Black;//NOT WoRK


            //doc.AddHeaders();
            //Novacode.Paragraph evenParagraph1 = doc.Headers.even.InsertParagraph("招標案卷", false, HeaderLineFormat);
            //evenParagraph1.Alignment = Alignment.right;
            //Novacode.Paragraph evenParagraph2 = doc.Headers.even.InsertParagraph(Typelbl.Text, false, HeaderLineFormat);
            //evenParagraph2.Alignment = Alignment.right;

            //doc.AddFooters();

            //Novacode.Paragraph oddFooter1 = doc.Footers.odd.InsertParagraph();
            //oddFooter1.Alignment = Alignment.center;
            //oddFooter1.AppendPageNumber(PageNumberFormat.normal);

            //Novacode.Paragraph evenFooter = doc.Footers.even.InsertParagraph();
            //evenFooter.Alignment = Alignment.center;
            //evenFooter.AppendPageNumber(PageNumberFormat.normal);

            //Novacode.Paragraph oddParagraph1 = doc.Headers.odd.InsertParagraph("招標案卷", false, HeaderLineFormat);
            //oddParagraph1.Alignment = Alignment.right;
            //Novacode.Paragraph oddParagraph2 = doc.Headers.odd.InsertParagraph(Typelbl.Text, false, HeaderLineFormat);
            //oddParagraph2.Alignment = Alignment.right;

            //doc.InsertParagraph(txtTypeZH, false, TitleLineFormat).Alignment = Alignment.center;
            //doc.InsertParagraph(InputTitleZH.Text, false, TitleLineFormat).Alignment = Alignment.center;


            //doc.InsertParagraph(txtDatePicker.Text);
            //doc.InsertSectionPageBreak();

            //evenParagraph1.Remove(false);
            //evenParagraph2.Remove(false);
            //oddParagraph1.Remove(false);
            //oddParagraph2.Remove(false);
            //doc.Headers.even.RemoveParagraph(evenParagraph1);
            //doc.Headers.even.RemoveParagraph(evenParagraph2);
            //doc.Headers.odd.RemoveParagraph(oddParagraph1);
            //doc.Headers.odd.RemoveParagraph(oddParagraph2);

            //doc.Headers.even.InsertParagraph("Processo de Concurso", false, HeaderLineFormat).Alignment = Alignment.right;
            //doc.Headers.even.InsertParagraph("Anexo II –  Programa do Concurso", false, HeaderLineFormat).Alignment = Alignment.right;
            //doc.Headers.odd.InsertParagraph("Processo de Concurso", false, HeaderLineFormat).Alignment = Alignment.right;
            //doc.Headers.odd.InsertParagraph("Anexo II –  Programa do Concurso", false, HeaderLineFormat).Alignment = Alignment.right;


            //doc.InsertParagraph(txtTypePT, false, TitleLineFormat).Alignment = Alignment.center;
            //doc.InsertParagraph(InputTitlePT.Text, false, TitleLineFormat).Alignment = Alignment.center;
            //String[] ptData = new String[x];

            //int txtpt = 1;
            //int lblpt = 1;
            //for (int i = 1; i <= x; i++)
            //{
            //    String rowPTSQL = "Select Row" + i + " From " + table + "Table Where Language = 'PT' AND Type = '" + type + "PT'";
            //    System.Data.SqlClient.SqlCommand rowPTCommand = new System.Data.SqlClient.SqlCommand(rowPTSQL, connection);
            //    string rowPTString = Convert.ToString(rowPTCommand.ExecuteScalar());


            //    if (rowPTString.Equals("TextBox"))
            //    {
            //        if (CheckBox1.Checked) { doc.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT" + i)).Text + ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt)).Text, false, LineFormat); }
            //        else { doc.InsertParagraph(((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt)).Text, false, LineFormat); }
            //        ptData[i - 1] = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("InputPT" + txtpt++)).Text;
            //    }
            //    else
            //    {
            //        doc.InsertParagraph();
            //        if (CheckBox1.Checked) { doc.InsertParagraph(((DropDownList)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("DropPT" + i)).Text + ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt)).Text, false, LineFormat).Bold(); }
            //        else { doc.InsertParagraph(((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt)).Text, false, LineFormat).Bold(); }
            //        ptData[i - 1] = ((Label)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl("LabelPT" + lblpt++)).Text;
            //    }
            //}
            //doc.InsertParagraph(txtDatePicker.Text);

            doc.SaveAs(fileName);

            //for (int i = 0; i < x; i++)
            //{
            //    String insertSQL = "Insert into Data (ZH, PT) Values (@zh, @pt);";
            //    System.Data.SqlClient.SqlCommand insertCommand = new System.Data.SqlClient.SqlCommand(insertSQL, connection);
            //    insertCommand.Parameters.AddWithValue("@zh", zhData[i]);
            //    insertCommand.Parameters.AddWithValue("@pt", ptData[i]);
            //    insertCommand.ExecuteNonQuery();
            //}

            System.IO.FileInfo file2 = new System.IO.FileInfo(fileName);
            if (file2.Exists)
            {
                getID(getCode());
                String strSQL = "Insert into DataTable (FileName,Date,Type,Status,Title,UserId) Values (@fileName,@date,@type,@status,@title,@userid)";
                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.Parameters.AddWithValue("@fileName", fn);
                myCommand.Parameters.AddWithValue("@date", txtDatePicker.Text);
                myCommand.Parameters.AddWithValue("@type", type);
                myCommand.Parameters.AddWithValue("@status",0);
                myCommand.Parameters.AddWithValue("@title",InputTitleZH.Text);
                myCommand.Parameters.AddWithValue("@userid",User.Identity.GetUserId().ToString());
                myCommand.ExecuteNonQuery();

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

        protected void Button7_Click(object sender, EventArgs e)
        {
            int txtzh = int.Parse(Session["txtzh"].ToString());
            InputTitlePT.Text = BaiduTranslateTitle(InputTitleZH.Text);
            for (int i = 1; i < txtzh; i++)
            {
                BaiduTranslate("InputZH" + i);
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            int txtzh = int.Parse(Session["txtzh"].ToString());
            InputTitlePT.Text = YudouTranslateTitle(InputTitleZH.Text);
            for (int i = 1; i < txtzh; i++)
            {
                YudouTranslate("InputZH" + i);
            }
        }

        private void BaiduTranslate(string control1, string control2)
        {
            Encoding myEncoding = Encoding.GetEncoding("utf-8");
            string word = Dictionary(((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl(control1)).Text);
            string cl = "20170719000065298" + word + "1435660288" + "FmKYse7PEGeeLzSqq4RW";

            string pwd = "";
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                pwd = String.Concat(md5.ComputeHash(Encoding.UTF8.GetBytes(cl)).Select(x => x.ToString("x2")));
            }

            string address = "https://fanyi-api.baidu.com/api/trans/vip/translate?"
                + HttpUtility.UrlEncode("q", myEncoding) + "=" + HttpUtility.UrlEncode(word, myEncoding) + "&"
                + HttpUtility.UrlEncode("from", myEncoding) + "=" + HttpUtility.UrlEncode("zh", myEncoding) + "&"
                + HttpUtility.UrlEncode("to", myEncoding) + "=" + HttpUtility.UrlEncode("pt", myEncoding) + "&"
                + HttpUtility.UrlEncode("appid", myEncoding) + "=" + HttpUtility.UrlEncode("20170719000065298", myEncoding) + "&"
                + HttpUtility.UrlEncode("salt", myEncoding) + "=" + HttpUtility.UrlEncode("1435660288", myEncoding) + "&"
                + HttpUtility.UrlEncode("sign", myEncoding) + "=" + HttpUtility.UrlEncode(pwd, myEncoding);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(address);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                Stream dataStream = wr.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                //string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine(responseFromServer);
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, object> values = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                var x = values["trans_result"];
                Dictionary<string, object> g = (Dictionary<string, object>)((ArrayList)x)[0];
                string rt = (string)g["dst"];
                ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl(control2)).Text = rt;
            }
        }

        private void YudouTranslate(string control1, string control2)
        {
            Encoding myEncoding = Encoding.GetEncoding("utf-8");
            string word = ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl(control1)).Text;
            string cl = "182fb72f604119a9" + word + "1435660288" + "KtsQ75Ob59cGMiAfGxqXSJTf3vvmRiqx";

            string pwd = "";
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                pwd = String.Concat(md5.ComputeHash(Encoding.UTF8.GetBytes(cl)).Select(x => x.ToString("x2")));
            }

            string address = "https://openapi.youdao.com/api?"
                + HttpUtility.UrlEncode("q", myEncoding) + "=" + HttpUtility.UrlEncode(word, myEncoding) + "&"
                + HttpUtility.UrlEncode("from", myEncoding) + "=" + HttpUtility.UrlEncode("zh-CHS", myEncoding) + "&"
                + HttpUtility.UrlEncode("to", myEncoding) + "=" + HttpUtility.UrlEncode("pt", myEncoding) + "&"
                + HttpUtility.UrlEncode("appKey", myEncoding) + "=" + HttpUtility.UrlEncode("182fb72f604119a9", myEncoding) + "&"
                + HttpUtility.UrlEncode("salt", myEncoding) + "=" + HttpUtility.UrlEncode("1435660288", myEncoding) + "&"
                + HttpUtility.UrlEncode("sign", myEncoding) + "=" + HttpUtility.UrlEncode(pwd, myEncoding);
            try
            {


                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(address);
                req.Method = "GET";
                using (WebResponse wr = req.GetResponse())
                {
                    Stream dataStream = wr.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    //string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    //Console.WriteLine(responseFromServer);
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, object> values = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                    var x = values["translation"];
                    string g = (string)((ArrayList)x)[0];
                    string rt = g;
                    ((TextBox)((ContentPlaceHolder)this.Master.FindControl("MainContent")).FindControl(control2)).Text = rt;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string BaiduTranslateTitle(string text)
        {
            Encoding myEncoding = Encoding.GetEncoding("utf-8");
            string word = Dictionary(text);
            string cl = "20170719000065298" + word + "1435660288" + "FmKYse7PEGeeLzSqq4RW";

            string pwd = "";
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                pwd = String.Concat(md5.ComputeHash(Encoding.UTF8.GetBytes(cl)).Select(x => x.ToString("x2")));
            }

            string address = "https://fanyi-api.baidu.com/api/trans/vip/translate?"
                + HttpUtility.UrlEncode("q", myEncoding) + "=" + HttpUtility.UrlEncode(word, myEncoding) + "&"
                + HttpUtility.UrlEncode("from", myEncoding) + "=" + HttpUtility.UrlEncode("zh", myEncoding) + "&"
                + HttpUtility.UrlEncode("to", myEncoding) + "=" + HttpUtility.UrlEncode("pt", myEncoding) + "&"
                + HttpUtility.UrlEncode("appid", myEncoding) + "=" + HttpUtility.UrlEncode("20170719000065298", myEncoding) + "&"
                + HttpUtility.UrlEncode("salt", myEncoding) + "=" + HttpUtility.UrlEncode("1435660288", myEncoding) + "&"
                + HttpUtility.UrlEncode("sign", myEncoding) + "=" + HttpUtility.UrlEncode(pwd, myEncoding);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(address);
            req.Method = "GET";
            try
            {
                using (WebResponse wr = req.GetResponse())
                {
                    Stream dataStream = wr.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    //string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    //Console.WriteLine(responseFromServer);
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, object> values = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                    var x = values["trans_result"];
                    Dictionary<string, object> g = (Dictionary<string, object>)((ArrayList)x)[0];
                    string rt = (string)g["dst"];
                    return rt;
                }
            }
            catch
            {
                return null;
            }

        }

        private string YudouTranslateTitle(string text)
        {
            Encoding myEncoding = Encoding.GetEncoding("utf-8");
            string word = text;
            string cl = "182fb72f604119a9" + word + "1435660288" + "KtsQ75Ob59cGMiAfGxqXSJTf3vvmRiqx";

            string pwd = "";
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                pwd = String.Concat(md5.ComputeHash(Encoding.UTF8.GetBytes(cl)).Select(x => x.ToString("x2")));
            }

            string address = "https://openapi.youdao.com/api?"
                + HttpUtility.UrlEncode("q", myEncoding) + "=" + HttpUtility.UrlEncode(word, myEncoding) + "&"
                + HttpUtility.UrlEncode("from", myEncoding) + "=" + HttpUtility.UrlEncode("zh-CHS", myEncoding) + "&"
                + HttpUtility.UrlEncode("to", myEncoding) + "=" + HttpUtility.UrlEncode("pt", myEncoding) + "&"
                + HttpUtility.UrlEncode("appKey", myEncoding) + "=" + HttpUtility.UrlEncode("182fb72f604119a9", myEncoding) + "&"
                + HttpUtility.UrlEncode("salt", myEncoding) + "=" + HttpUtility.UrlEncode("1435660288", myEncoding) + "&"
                + HttpUtility.UrlEncode("sign", myEncoding) + "=" + HttpUtility.UrlEncode(pwd, myEncoding);
            try
            {


                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(address);
                req.Method = "GET";
                using (WebResponse wr = req.GetResponse())
                {
                    Stream dataStream = wr.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    //string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    //Console.WriteLine(responseFromServer);
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, object> values = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                    var x = values["translation"];
                    string g = (string)((ArrayList)x)[0];
                    string rt = g;
                    return rt;
                }
            }
            catch
            {
                return null;
            }
        }

        private void BaiduTranslate(string control1)
        {
            string s = control1.Replace("ZH", "PT");
            BaiduTranslate(control1, s);
        }

        private void YudouTranslate(string control1)
        {
            string s = control1.Replace("ZH", "PT");
            YudouTranslate(control1, s);
        }

        private string Dictionary(string s, string s1)
        {
            //Like
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            connection.Open();
            String selectSQL1 = @"SELECT Id FROM Dictionary WHERE ZH Like N'%" + s1 + "%'";
            System.Data.SqlClient.SqlCommand myCommand1 = new System.Data.SqlClient.SqlCommand(selectSQL1, connection);
            int x1 = -1;
            x1 = Convert.ToInt32(myCommand1.ExecuteScalar());
            connection.Close();
            if (x1 > 0)
            {
                connection.Open();
                String selectSQL2 = "SELECT * FROM Dictionary WHERE ZH = N'" + s1 + "'";
                System.Data.SqlClient.SqlCommand myCommand2 = new System.Data.SqlClient.SqlCommand(selectSQL2, connection);
                int x2 = -1;
                x2 = Convert.ToInt32(myCommand2.ExecuteScalar());
                connection.Close();
                if (x2 > 0)
                {
                    string s2 = "";
                    if (s.Length > 0)
                    {
                        s2 = Dictionary(s.Length > 1 ? s.Substring(1, s.Length - 1) : "", s1 + s.Substring(0, 1));
                    }
                    if (s2 == "")
                    {
                        return s1;
                    }
                    return s2;
                }
                else
                {
                    if (s.Length > 0)
                    {
                        s1 += s.Substring(0, 1);
                        s1 = Dictionary(s.Substring(1, s.Length - 1), s1);
                    }
                }

            }
            else
            {
                return "";
            }

            return s1;
        }

        private string Dictionary(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                string x = Dictionary(s.Substring(i + 1, s.Length - i - 1), s.Substring(i, 1));
                if (!String.IsNullOrEmpty(x) && !String.IsNullOrWhiteSpace(x))
                {
                    System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
                    connection.Open();
                    String selectSQL1 = @"SELECT PT FROM Dictionary WHERE ZH = N'" + x + "'";
                    System.Data.SqlClient.SqlCommand myCommand1 = new System.Data.SqlClient.SqlCommand(selectSQL1, connection);
                    string replaceword = " " + myCommand1.ExecuteScalar().ToString() + " ";
                    string a = s.Substring(0, i);
                    string b = s.Substring(i + x.Length);
                    s = a + replaceword + b;
                    i = a.Length + replaceword.Length - 1;
                    connection.Close();
                }
            }

            return s;
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
                Session["School"] = "ESCE";
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
    }
}