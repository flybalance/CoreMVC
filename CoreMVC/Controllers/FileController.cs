using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Collections.Generic;
using System.IO;

namespace CoreMVC.Controllers
{
    public class FileController : Controller
    {
        private readonly IConfiguration _configuration;

        public FileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.MySqlConn = _configuration.GetValue<string>("Ado.Net.MySqlConn");

            return View();
        }

        [HttpPost]
        public IActionResult Upload()
        {
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                return null;
            }

            var file = files[0];
            Stream stream = file.OpenReadStream();

            return null;
        }
    }

    public static class ExcelUtil
    {
        public static List<Dictionary<string, object>> ImportExcel(Stream stream, int titleRownum = 0)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            IWorkbook workbook = null;
            ISheet sheet = null;

            try
            {
                workbook = WorkbookFactory.Create(stream);
                sheet = workbook.GetSheetAt(0);
            }
            catch (System.Exception)
            {
                return null;
            }

            if (titleRownum < 0)
            {
                return null;
            }

            try
            {
                int sheetCount = sheet.LastRowNum;
                int sheetMergeCount = sheet.NumMergedRegions;
                IRow row = null;
                Dictionary<string, int> titleMap = new Dictionary<string, int>();

                for (int rowIndex = titleRownum + 1; rowIndex <= sheetCount; rowIndex++)
                {
                    row = sheet.GetRow(rowIndex);
                    if (null == row || row.RowNum < titleRownum - 1)
                    {
                        continue;
                    }

                    #region 解析标题行
                    if (row.RowNum == titleRownum - 1)
                    {
                        List<ICell> cellList = row.Cells;
                        int index = 0;
                        object cellValue = null;

                        foreach (ICell cellItem in cellList)
                        {
                            if (sheetMergeCount > 0)
                            {
                                for (int i = 0; i < sheetMergeCount; i++)
                                {
                                    CellRangeAddress range = sheet.GetMergedRegion(i);
                                    int firstColumn = range.FirstColumn;
                                    int lastColumn = range.LastColumn;
                                    int firstRow = range.FirstRow;
                                    int lastRow = range.LastRow;

                                    if (cellItem.RowIndex == firstRow
                                        && cellItem.RowIndex == lastRow
                                        && cellItem.ColumnIndex >= firstColumn
                                        && cellItem.ColumnIndex >= lastColumn)
                                    {
                                        for (int j = firstColumn; j <= lastColumn; j++)
                                        {
                                            index = j;

                                            cellValue = GetCellValue(sheet.GetRow(firstRow + 1).GetCell(j));

                                            titleMap.Add(null == cellValue ? "" : cellValue.ToString(), index);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                cellValue = GetCellValue(cellItem);

                                titleMap.Add(null == cellValue ? "" : cellValue.ToString(), index);
                            }

                            index++;
                        }
                    }
                    #endregion

                    if (sheetMergeCount > 0
                        && row.RowNum == titleRownum - 1)
                    {
                        continue;
                    }

                    Dictionary<string, object> map = new Dictionary<string, object>();
                    foreach (string key in titleMap.Keys)
                    {
                        int index = titleMap[key];

                        object contentCellValue = GetCellValue(row.GetCell(index));
                        map.Add(key, null == contentCellValue ? "" : contentCellValue.ToString());
                    }

                    list.Add(map);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                stream.Close();
            }

            return list;

        }

        private static object GetCellValue(ICell cell)
        {
            if (cell == null || (cell.CellType == CellType.String
                    && string.IsNullOrWhiteSpace(cell.StringCellValue)))
            {
                return null;
            }

            CellType cellType = cell.CellType;

            switch (cellType)
            {
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:
                    return cell.NumericCellValue;
                case CellType.Numeric:
                    cell.SetCellType(CellType.String);
                    return cell.StringCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                default:
                    return null;
            }
        }
    }
}