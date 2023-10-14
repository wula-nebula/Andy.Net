using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;

using System.Threading.Tasks;
using Template.Static;

namespace Nebula.OMSV2.Client.Portal.Core
{
    public class FileHelper
    {
        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <returns></returns>
        public static async Task<Stream> HttpDownload(string url)
        {
            using (var client = new WebClient())
            {
                return await client.OpenReadTaskAsync(url);
            }
        }

        /// <summary>
        /// 文件转流
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static Stream FileToStream(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // 读取文件的 byte[]
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
                // 把 byte[] 转换成 Stream
                Stream stream = new MemoryStream(bytes);
                File.Delete(fileName);//删除临时文件
                return stream;
            }
        }

        #region GetStreamByte

        /// <summary>
        /// URL转字节流
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static byte[] GetStreamByte(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            byte[] bytes;
            using (Stream stream = req.GetResponse().GetResponseStream())
            {
                using (MemoryStream mstream = new MemoryStream())
                {
                    int count = 0;
                    byte[] buffer = new byte[1024];
                    int readNum = 0;
                    while ((readNum = stream.Read(buffer, 0, 1024)) > 0)
                    {
                        count = count + readNum;
                        mstream.Write(buffer, 0, readNum);
                    }
                    mstream.Position = 0;
                    using (BinaryReader br = new BinaryReader(mstream))
                    {
                        bytes = br.ReadBytes(count);
                    }
                }
            }

            return bytes;
        }

        #endregion

        #region FileDownload

        /// <summary>
        /// 下载文件指定url到本地临时文件
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static Stream FileDownload(string url)
        {
            using (var client = new WebClient())
            {
                string temp = Path.GetTempFileName();
                client.DownloadFile(url, temp);
                return FileToStream(temp);
            }
        }

        #endregion

        #region ExcelToTable
        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中(xls)
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="iIndex"></param>
        /// <param name="typ"></param>
        /// <returns></returns>
        public static DataTable ExcelToTable(byte[] bytes, int iIndex, ExcelIdeType typ = ExcelIdeType.Excel2007)
        {
            DataTable dtResult = new DataTable();
            if (typ.Equals(ExcelIdeType.Excel2003))
            {
                dtResult = ExcelToTableForXLS(new MemoryStream(bytes), iIndex);
            }
            else if (typ.Equals(ExcelIdeType.Excel2007))
            {
                dtResult = ExcelToTableForXLSX(new MemoryStream(bytes), iIndex);
            }
            else if (typ.Equals(ExcelIdeType.Other))
            {
                dtResult = ExcelToTableForXLS(new MemoryStream(bytes), iIndex);
                if (dtResult == null || dtResult?.Rows?.Count < 1)
                {
                    dtResult = ExcelToTableForXLSX(new MemoryStream(bytes), iIndex);
                }
            }
            return dtResult;
        }

        #endregion

        #region ExcelToTableFromUrl

        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中(xls)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream">文件流</param>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableFromUrl(string url,Stream stream, int iIndex)
        {
            ExcelIdeType objExcelIdeType = GetExtFileType(url);
            DataTable dtResult = new DataTable();
            if (objExcelIdeType.Equals(ExcelIdeType.Excel2003))
            {
                dtResult = ExcelToTableForXLS(stream, iIndex);
            }
            else if (objExcelIdeType.Equals(ExcelIdeType.Excel2007))
            {
                dtResult = ExcelToTableForXLSX(stream, iIndex);
            }
            else if (objExcelIdeType.Equals(ExcelIdeType.Other))
            {
                dtResult = ExcelToTableForXLS(stream, iIndex);
                if (dtResult == null || dtResult?.Rows?.Count < 1)
                {
                    dtResult = ExcelToTableForXLSX(stream, iIndex);
                }
            }
            return dtResult;
        }

        #endregion

        #region ExcelIdeType

        /// <summary>
        /// Excel类型
        /// </summary>
        public enum ExcelIdeType
        {
            /// <summary>
            /// Excel2003
            /// </summary>
            Excel2003,
            /// <summary>
            /// Excel2007
            /// </summary>
            Excel2007,
            /// <summary>
            /// CSV
            /// </summary>
            ExcelCSV,
            /// <summary>
            /// 
            /// </summary>
            Other
        }

        #endregion

        #region GetExtFileTypeName
        /// <summary>
        /// 取文件的扩展名
        /// </summary>
        /// <param name="strFileName">文件路径</param>
        /// <returns>string</returns>
        public static string GetExtFileTypeName(string strFileName)
        {
            string sFile = strFileName;
            if (!string.IsNullOrWhiteSpace(sFile))
            {
                sFile = sFile.Substring(sFile.LastIndexOf("\\") + 1);
                sFile = sFile.Substring(sFile.LastIndexOf(".")).ToLower();
            }
            return sFile;
        }

        #endregion

        #region GetMimeType

        /// <summary>
        /// 获取文件的ContentType
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetMimeType(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))//extension == null
            {
                return string.Empty;
            }

            if (extension != null && !extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime = string.Empty;

            return FileContentType._mappings.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }

        #endregion

        /// <summary>
        /// 获取Excel文件类型
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static ExcelIdeType GetExcelType(string extension)
        {
            ExcelIdeType file_type = ExcelIdeType.Excel2003;
            if (string.IsNullOrWhiteSpace(extension))
            {
                return file_type;
            }

            switch (extension)
            {
                case "xls":
                    file_type = ExcelIdeType.Excel2003;
                    break;
                case "xlsx":
                    file_type = ExcelIdeType.Excel2007;
                    break;
                case "csv":
                    file_type = ExcelIdeType.ExcelCSV;
                    break;
                default:
                    file_type = ExcelIdeType.Other;
                    break;
            }

            return file_type;
        }


        //////////////////////////////////////private method/////////////////////////////////////////////////

        #region GetExtFileType

        /// <summary>
        /// 获取文件的版本类型
        /// </summary>
        /// <param name="strFileName">文件路径</param>
        /// <returns></returns>
        private static ExcelIdeType GetExtFileType(string strFileName)
        {
            ExcelIdeType objExcelIdeType = ExcelIdeType.Excel2003;
            switch (GetExtFileTypeName(strFileName))
            {
                case ".xls":
                    objExcelIdeType = ExcelIdeType.Excel2003;
                    break;
                case ".xlsx":
                    objExcelIdeType = ExcelIdeType.Excel2007;
                    break;
                case ".csv":
                    objExcelIdeType = ExcelIdeType.ExcelCSV;
                    break;
                default:
                    objExcelIdeType = ExcelIdeType.Other;
                    break;
            }
            return objExcelIdeType;
        }

        #endregion
        
        #region ExcelToTableForXLS

        public static DataTable ExcelToTableForXLS(Stream stream, int iIndex)
        {
            DataTable dt = new DataTable();
            try
            {
                HSSFWorkbook xssfworkbook = new HSSFWorkbook(stream);
                ISheet sheet = xssfworkbook.GetSheetAt(iIndex);
                dt.TableName = xssfworkbook.GetSheetName(iIndex);
                //表头
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();

                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLS(header.GetCell(i) as HSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("列" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }

                int iStart = sheet.FirstRowNum;
                int iLast = sheet.LastRowNum;
                if (iIndex.Equals(0))
                {
                    iStart += 1;
                }
                //数据
                for (int i = iStart; i <= iLast; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        if (sheet.GetRow(i) == null)
                        {
                            continue;
                        }
                        dr[j] = GetValueTypeForXLS(sheet.GetRow(i).GetCell(j) as HSSFCell); //sheet.GetRow(i).GetCell(j) == null ? string.Empty : sheet.GetRow(i).GetCell(j).ToString();
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        #endregion

        #region GetValueTypeForXLS
        /// <summary>
        /// 获取单元格类型(xls)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueTypeForXLS(HSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank:    //BLANK:
                    return null;
                case CellType.Boolean:  //BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.Numeric:  //NUMERIC:
                    if (NPOI.SS.UserModel.DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue;
                    }
                    else
                    {
                        return cell.NumericCellValue;
                    }
                case CellType.String:   //STRING:
                    return cell.StringCellValue;
                case CellType.Error:    //ERROR:
                    return cell.ErrorCellValue;
                case CellType.Formula:  //FORMULA:
                    return cell.NumericCellValue;
                default:
                    return "=" + cell.CellFormula;
            }
        }

        #endregion

        #region ExcelToTableForXLSX
        private static DataTable ExcelToTableForXLSX(Stream stream, int iIndex)
        {
            DataTable dt = new DataTable();
            try
            {
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(stream);
                ISheet sheet = xssfworkbook.GetSheetAt(iIndex);
                dt.TableName = xssfworkbook.GetSheetName(iIndex);
                //表头
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();

                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("列" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }

                int iStart = sheet.FirstRowNum;
                int iLast = sheet.LastRowNum;
                if (iIndex.Equals(0))
                {
                    iStart += 1;
                }
                //数据
                for (int i = iStart; i <= iLast; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        if (sheet.GetRow(i) == null)
                        {
                            continue;
                        }
                        dr[j] = GetValueTypeForXLSX(sheet.GetRow(i).GetCell(j) as XSSFCell); //sheet.GetRow(i).GetCell(j) == null ? string.Empty : sheet.GetRow(i).GetCell(j).ToString();
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception)
            {
            }
            
            return dt;
        }
        #endregion

        #region GetValueTypeForXLSX
        /// <summary>
        /// 获取单元格类型(xls)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueTypeForXLSX(XSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank:    //BLANK:
                    return null;
                case CellType.Boolean:  //BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.Numeric:  //NUMERIC:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue;
                    }
                    else
                    {
                        return cell.NumericCellValue;
                    }
                case CellType.String:   //STRING:
                    return cell.StringCellValue;
                case CellType.Error:    //ERROR:
                    return cell.ErrorCellValue;
                case CellType.Formula:  //FORMULA:
                    return cell.NumericCellValue;
                default:
                    return "=" + cell.CellFormula;
            }
        }
        #endregion

       


    }
}
