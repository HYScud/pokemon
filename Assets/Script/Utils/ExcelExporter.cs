using System.IO;
using System.Data;
using ExcelDataReader;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class ReadExcel : MonoBehaviour
{

    private string FilePath;


    private void Start()
    {
        FilePath = Application.dataPath + "/Resources/DataTables/LevelList.xlsx";
        //ReadExcelStream();
        ReadNatureExcelStream();
        Debug.Log(FilePath);
    }

    /*void ReadExcelStream()
    {
        FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelDataReader.AsDataSet();

        int columns = result.Tables[0].Columns.Count;
        int rows = result.Tables[0].Rows.Count;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                string nvalue = result.Tables[0].Rows[i][j].ToString();
                Debug.Log(nvalue);
            }
        }

        excelDataReader.Close();
    }*/
    void ReadNatureExcelStream()
    {
        string FilePath = Application.dataPath + "/Resources/DataTables/NatrueCharts.xlsx";
        FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
        if (stream != null)
        {
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelDataReader.AsDataSet();
            if (result != null)
            {
                int columns = result.Tables[0].Columns.Count;
                int rows = result.Tables[0].Rows.Count;
                Debug.Log("打印*列*" + columns + "行" + rows);
                PokemonTable.NatureChart = new float[rows - 1, columns - 1];
                PokemonTable.typeChart1 = new float[columns - 1][];
                for (int i = 1; i < rows; i++)
                {
                    for (int j = 1; j < columns; j++)
                    {
                        Debug.Log("打印*i:" + i + "**j:" + j + "**result:" + result.Tables[0].Rows[i][j]);
                        PokemonTable.NatureChart[i-1, j-1] = (float)Convert.ToSingle(result.Tables[0].Rows[i][j]);
                    }
                }
                /*for (int i = 1; i < columns; i++)
                {
                    for (int j = 1; j < rows; j++)
                    {
                        Debug.Log("打印**" + result.Tables[0].Rows[i][j]);
                        var tem = PokemonTable.typeChart1[i];
                        if (tem == null)
                        {
                            tem = new float[rows];
                        }
                        tem[j] = (float)Convert.ToSingle(result.Tables[0].Rows[i][j]);
                    }
                }*/
            }
            else
            {
                Debug.Log("读取性格表失败");
            }
            excelDataReader.Close();
        }
    }
}