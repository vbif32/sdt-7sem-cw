using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Entities;
using OfficeOpenXml;

namespace Services.Converters.Import
{
    internal static class F101Converter
    {
        private const int StartF101Row = 2;
        private const int StartCalculationRow = 14;

        public static List<F101Entry> Convert(string sourcePath)
        {
            var s = $"EXCEL\r\n";
            File.AppendAllText(@"log.txt", s);

            var newFile = new FileInfo(sourcePath);
            var result = new List<F101Entry>();
            using (var package = new ExcelPackage(newFile))
            {
                var worksheet = package.Workbook.Worksheets[1];
                for (var row = StartF101Row;
                    !string.IsNullOrWhiteSpace(worksheet.Cells[row, 5].GetValue<string>());
                    row++)
                    result.Add(LoadEntry(worksheet, row));
            }
            return result;
        }

        private static F101Entry LoadEntry(ExcelWorksheet worksheet, int row)
        {
            var s = $"{worksheet.Cells[row, 5].GetValue<string>()} {worksheet.Cells[row, 7].GetValue<string>()}\r\n";
            File.AppendAllText(@"log.txt", s);

            return new F101Entry
            {
                Курс = worksheet.Cells[row, 1].GetValue<int>(),
                Семестр = worksheet.Cells[row, 2].GetValue<int>(),
                НомерПотока = worksheet.Cells[row, 3].GetValue<int>(),
                ИмяПотока = worksheet.Cells[row, 4].GetValue<string>(),
                Дисциплина = worksheet.Cells[row, 5].GetValue<string>(),
                Кафедра = worksheet.Cells[row, 6].GetValue<int>(),
                Лк = worksheet.Cells[row, 7].GetValue<string>(),
                Лаб = worksheet.Cells[row, 8].GetValue<string>(),
                Пр = worksheet.Cells[row, 9].GetValue<string>(),
                // пропускаем 10 столбец из-за СРС
                Экзамен = !string.IsNullOrWhiteSpace(worksheet.Cells[row, 11].GetValue<string>()),
                Зачет = !string.IsNullOrWhiteSpace(worksheet.Cells[row, 12].GetValue<string>()),
                Кп = !string.IsNullOrWhiteSpace(worksheet.Cells[row, 13].GetValue<string>()),
                Кр = !string.IsNullOrWhiteSpace(worksheet.Cells[row, 14].GetValue<string>()),
                // и еще 15-й из-за пустого столбца
                НедельТо = worksheet.Cells[row, 16].GetValue<string>(),
                Трудоемкость = worksheet.Cells[row, 17].GetValue<int>(),
                ТрудоемкостьГода = worksheet.Cells[row, 18].GetValue<int>(),
                Численность = worksheet.Cells[row, 19].GetValue<string>(),
                ЧислоГрупп = worksheet.Cells[row, 20].GetValue<int>(),
            };
        }

        /// <summary>
        ///     Метод для тестирования соответствия моей конвертации расчётам экселя
        /// </summary>
        private static List<Load> LoadCalculation(string path)
        {
            var newFile = new FileInfo(path);
            var result = new List<Load>();
            using (var package = new ExcelPackage(newFile))
            {
                var worksheet = package.Workbook.Worksheets[3];
                var row = StartCalculationRow;
                while (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].GetValue<string>()))
                {
                    var лекции = float.Parse(worksheet.Cells[row, 21].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var лр =  float.Parse(worksheet.Cells[row, 22].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var пр =  float.Parse(worksheet.Cells[row, 23].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var зачеты =  float.Parse(worksheet.Cells[row, 24].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var консультации =  float.Parse(worksheet.Cells[row, 25].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var экзамены =  float.Parse(worksheet.Cells[row, 26].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var практикиИНир =  float.Parse(worksheet.Cells[row, 27].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var крКп =  float.Parse(worksheet.Cells[row, 28].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var вкр =  float.Parse(worksheet.Cells[row, 29].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var гэк =  float.Parse(worksheet.Cells[row, 30].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var гак =  float.Parse(worksheet.Cells[row, 31].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var рма =  float.Parse(worksheet.Cells[row, 32].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    var рмп =  float.Parse(worksheet.Cells[row, 32].GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture);
                    if (worksheet.Cells[row, 32].GetValue<string>().Contains("Руководство программой"))
                        рма = 0;
                    else
                        рмп = 0;
                    var entry = new Load(лекции, лр, пр, зачеты, консультации,
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