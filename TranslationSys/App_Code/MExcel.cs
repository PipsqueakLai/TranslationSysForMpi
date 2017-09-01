using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.SqlClient;

/// <summary>
/// Summary description for LoadExcel
/// </summary>
public class MExcel
{
    XSSFWorkbook hssfworkbook;
    public MExcel()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public void LoadExcelToDataBase(string filePath)
    {
        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
        connection.Open();
        String strSQL = "Insert into Dictionary (ZH,PT) Values (@ZH,@PT)";
        

        try
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new XSSFWorkbook(file);
            }
        }
        catch (Exception e)
        {
            throw e;
        }

        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
        System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

        while (rows.MoveNext())
        {
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            XSSFRow row = (XSSFRow)rows.Current;
            for (int i = 0; i < row.LastCellNum; i++)
            {

                NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                if (cell == null)
                {
                }
                else
                {
                    if (i == 0)
                    {
                        myCommand.Parameters.AddWithValue("@ZH", cell.ToString());
                    }else if(i == 1)
                    {
                        myCommand.Parameters.AddWithValue("@PT", cell.ToString());
                        myCommand.ExecuteNonQuery();
                    }

                }
            }

        }
        connection.Close();

    }
}