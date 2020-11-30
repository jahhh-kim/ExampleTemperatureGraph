using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTemperatureGraph
{
    class CsvRead
    {
        /// <summary> 
        /// CSV 파일을 읽기 
        /// </summary> 
        /// <param name="filePath">CSV 파일 경로</param> 
        /// <param name="isFirstHeader">첫번째 행의 헤더여부</param> 
        /// <param name="commentTokens">주석처리 문자열</param> 
        /// <param name="delimiter">구분자 문자열</param> 
        /// <param name="hasEnclosedInQutes"></param> 
        /// <returns></returns> 

        public static Dictionary<String, List<String[]>> readCSV(String filePath, bool isFirstHeader, String commentTokens, String delimiter, bool hasEnclosedInQutes)
        {
            Dictionary<String, List<String[]>> returnvalue = new Dictionary<String, List<String[]>>();
            List<String[]> headerList = new List<String[]>();
            List<String[]> dataList = new List<String[]>();
            returnvalue.Add("Header", headerList);
            returnvalue.Add("Data", dataList);
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(filePath))
                {
                    csvReader.CommentTokens = new string[] { commentTokens };
                    csvReader.SetDelimiters(new string[] { delimiter });
                    csvReader.HasFieldsEnclosedInQuotes = hasEnclosedInQutes;
                    int count = 0;

                    while (!csvReader.EndOfData)
                    { // Read current line fields, pointer moves to the next line. 
                        String[] fields = csvReader.ReadFields();
                        if (isFirstHeader && count == 0) // 첫번째 줄이 Header 일 경우 
                        { headerList.Add(fields); }
                        else
                        {
                            dataList.Add(fields);
                        }
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return returnvalue;
        }

        /// <summary> 
        /// Header를 가지고 있는 CSV를 읽어서 Dictionary의 List로 반환 
        /// </summary> 
        /// <param name="filePath">CSV 파일 경로</param> 
        /// <param name="commentTokens">주석처리 문자열</param> 
        /// <param name="delimiter">구분자 문자열</param> 
        /// <param name="hasEnclosedInQutes"></param> 
        /// <returns></returns> 
        /// 

        public static List<Dictionary<String, String>> readCSVWithHeader(String filePath, bool v, String commentTokens, String delimiter, bool hasEnclosedInQutes)
        {
            List<Dictionary<String, String>> returnvalue = new List<Dictionary<String, String>>();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(filePath))
                {
                    csvReader.CommentTokens = new string[] { commentTokens };
                    csvReader.SetDelimiters(new string[] { delimiter });
                    csvReader.HasFieldsEnclosedInQuotes = hasEnclosedInQutes;
                    String[] headers = null; bool isFirst = true; while (!csvReader.EndOfData)
                    {
                        // Read current line fields, pointer moves to the next line. 
                        String[] fields = csvReader.ReadFields();
                        if (isFirst) // 첫번째 줄이 Header 
                        { headers = fields; isFirst = false; }
                        else
                        {
                            Dictionary<String, String> dataDic = new Dictionary<String, String>();
                            returnvalue.Add(dataDic);
                            for (int i = 0; i < fields.Length; i++)
                            { dataDic.Add(headers[i], fields[i]); }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return returnvalue;
        }

    }
}
