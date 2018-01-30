using System;
using System.IO;
using System.Linq;
using EntitiesViewModels;
using OfficeOpenXml;

namespace Services.Export
{
    internal class ExportToF16
    {
        private const int StartRow = 14;
        private static readonly string TemplatePath = @"Templates\F13Template.xlsx";

        public static void Export(EntitiesVMRegistry registry, string targetPath)
        {
            //TemplatePath = Path.GetFullPath(TemplatePath);
            File.Copy(TemplatePath, targetPath, true);
            var newFile = new FileInfo(targetPath);
            try
            {
                using (var package = new ExcelPackage(newFile))
                {
                    var subjects = registry.Subjects;
                    var worksheet = package.Workbook.Worksheets[1];
                    var row = StartRow;
                    var counter = 1;
                    foreach (var subject in subjects)
                    {
                        if (subject.Entries.Count == 0)
                            break;
                        worksheet.Cells[row, 1].Value = counter++;
                        worksheet.Cells[row, 2].Value = subject.Название;
                        worksheet.Cells[row, 3].Value = subject.Курс + ", ИТ";
                        worksheet.Cells[row, 4].Value = subject.Семестр / 2 == 0 ? "в" : "о";
                        worksheet.Cells[row, 5].Value = subject.Поток;
                        worksheet.Cells[row, 6].Value = subject.Subject.Лк;
                        worksheet.Cells[row, 7].Value = subject.Subject.Пр;
                        worksheet.Cells[row, 8].Value = subject.Subject.Лаб;
                        worksheet.Cells[row, 9].Value = subject.Subject.Численность;
                        worksheet.Cells[row, 10].Value = subject.PlannedLoad.Lectures;
                        worksheet.Cells[row, 11].Value = subject.PlannedLoad.Practical;
                        worksheet.Cells[row, 12].Value = subject.PlannedLoad.Laboratory;
                        worksheet.Cells[row, 13].Value = subject.PlannedLoad.Test;
                        worksheet.Cells[row, 14].Value = subject.PlannedLoad.Consultations;
                        worksheet.Cells[row, 15].Value = subject.PlannedLoad.Exams;
                        worksheet.Cells[row, 16].Value = subject.PlannedLoad.Nir;
                        worksheet.Cells[row, 17].Value = subject.PlannedLoad.CourseDesigning;
                        worksheet.Cells[row, 18].Value = subject.PlannedLoad.Vkr;
                        worksheet.Cells[row, 19].Value = subject.PlannedLoad.Hack;
                        worksheet.Cells[row, 20].Value = subject.PlannedLoad.Rma;
                        worksheet.Cells[row, 21].Value = subject.PlannedLoad.Rmp;
                        worksheet.Cells[row, 22].Value = subject.PlannedLoadSum;
                        worksheet.Cells[row, 23].Value = subject.Entries.Select(x => x.Teacher.Surname_N_P)
                            .Aggregate((s, i) => s + "\n" + i);
                        row++;
                        worksheet.InsertRow(row, 1, row);
                    }
                    package.Save();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}