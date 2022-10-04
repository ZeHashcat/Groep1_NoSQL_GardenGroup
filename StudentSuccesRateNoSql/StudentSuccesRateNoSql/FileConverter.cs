using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace StudentSuccesRateNoSql
{
    public class FileConverter
    {
        public void ConvertXLSL(string fileNameRead, string fileNameWrite)
        {
            string pathRead = new FileInfo($"{fileNameRead}.xlsx").FullName;
            string pathWrite = new FileInfo($"{fileNameWrite}.CSV").FullName;

            if (File.Exists(pathRead))
            {
                using (ExcelPackage package = new ExcelPackage(pathRead))
                {
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            var currentWorksheet = workBook.Worksheets.First();
                             foreach (ExcelRangeBase cells in currentWorksheet.Cells)
                            {
                                Console.WriteLine(cells.Value);
                            }
                        }
                    }
                }
            }
        }
    }
}
