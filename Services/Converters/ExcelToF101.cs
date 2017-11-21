using System.Collections.Generic;
using System.IO;
using Entities;
using OfficeOpenXml;

// ReSharper disable PossibleInvalidCastException

namespace Services.Converters
{
    internal static class ExcelToF101
    {
        private const int StartF101Row = 2;
        private const int StartCalculationRow = 14;

        public static List<F101Entry> LoadF101(string path)
        {
            var newFile = new FileInfo(path);
            var result = new List<F101Entry>();
            using (var package = new ExcelPackage(newFile))
            {
                var worksheet = package.Workbook.Worksheets[1];
                for (var row = StartF101Row; !string.IsNullOrWhiteSpace(worksheet.Cells[row, 5].GetValue<string>()); row++)
                    result.Add(LoadEntry(worksheet, row));
            }
            return result;
        }

        public static F101Entry LoadEntry(ExcelWorksheet worksheet, int row)
        {
            var курс = worksheet.Cells[row, 1].GetValue<int>();
            var семестр = worksheet.Cells[row, 2].GetValue<int>();
            var номерПотока = worksheet.Cells[row, 3].GetValue<int>();
            var имяПотока = worksheet.Cells[row, 4].GetValue<string>();
            var дисциплина = worksheet.Cells[row, 5].GetValue<string>();
            var кафедра = worksheet.Cells[row, 6].GetValue<int>();
            var лк = worksheet.Cells[row, 7].GetValue<string>();
            var лаб = worksheet.Cells[row, 8].GetValue<string>();
            var пр = worksheet.Cells[row, 9].GetValue<string>();
            // пропускаем 10 столбец из-за СРС
            var экзамен = !string.IsNullOrWhiteSpace(worksheet.Cells[row, 11].GetValue<string>());
            var зачет = !string.IsNullOrWhiteSpace(worksheet.Cells[row, 12].GetValue<string>());
            var кп = !string.IsNullOrWhiteSpace(worksheet.Cells[row, 13].GetValue<string>());
            var кр = !string.IsNullOrWhiteSpace(worksheet.Cells[row, 14].GetValue<string>());
            // и еще 15-й из-за пустого столбца
            var недельТо = worksheet.Cells[row, 16].GetValue<string>();
            var трудоемкость = worksheet.Cells[row, 17].GetValue<int>();
            var трудоемкостьГода = worksheet.Cells[row, 18].GetValue<int>();
            var численность = worksheet.Cells[row, 19].GetValue<string>();
            var числоГрупп = worksheet.Cells[row, 20].GetValue<int>();
            return new F101Entry(курс, семестр, номерПотока, имяПотока, дисциплина, кафедра,
                лк, лаб, пр, экзамен, зачет, кп,
                кр, недельТо, трудоемкость, трудоемкостьГода, численность, числоГрупп
            );
        }

        public static List<Нагрузка> LoadCalculation(string path)
        {
            var newFile = new FileInfo(path);
            var result = new List<Нагрузка>();
            using (var package = new ExcelPackage(newFile))
            {
                var worksheet = package.Workbook.Worksheets[3];
                var row = StartCalculationRow;
                while (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].GetValue<string>()))
                {
                    var лекции = worksheet.Cells[row, 21].GetValue<float>();
                    var лр = worksheet.Cells[row, 22].GetValue<float>();
                    var пр = worksheet.Cells[row, 23].GetValue<float>();
                    var зачеты = worksheet.Cells[row, 24].GetValue<float>();
                    var консультации = worksheet.Cells[row, 25].GetValue<float>();
                    var экзамены = worksheet.Cells[row, 26].GetValue<float>();
                    var практикиИНир = worksheet.Cells[row, 27].GetValue<float>();
                    var крКп = (worksheet.Cells[row, 28].GetValue<float>());
                    var вкр = (worksheet.Cells[row, 29].GetValue<float>());
                    var гэк = (worksheet.Cells[row, 30].GetValue<float>());
                    var гак = (worksheet.Cells[row, 31].GetValue<float>());
                    var рма = worksheet.Cells[row, 32].GetValue<float>();
                    var рмп = worksheet.Cells[row, 32].GetValue<float>();
                    if(worksheet.Cells[row, 32].GetValue<string>().Contains("Руководство программой"))
                        рма = 0;
                    else
                        рмп = 0;
                    var entry = new Нагрузка(лекции, лр, пр, зачеты, консультации,
                        экзамены, практикиИНир, крКп, вкр, гэк,
                        гак, рма, рмп);
                    result.Add(entry);
                    row++;
                }
            }
            return result;
        }
    }
}