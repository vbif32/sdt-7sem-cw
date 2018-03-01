using System;
using System.IO;
using System.Linq;
using Entities;
using OfficeOpenXml;
using Services.EntitiesViewModels;

namespace Services.Export
{
    internal class F16Converter
    {
        private const int StartRow = 14;
        private static readonly string TemplatePath = @"Templates\F13Template.xlsx";

        public static void Convert(EntitiesVMRegistry registry, string targetPath)
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
                    worksheet.Cells[10, 1].Value = $"КАФЕДРЫ_{ContextSingleton.Instance.EntitiesVmRegistry.Settings[(int)Settings.FullDepartmentName]}_ НА {ContextSingleton.Instance.EntitiesVmRegistry.Settings[(int)Settings.StartYear]}/{ContextSingleton.Instance.EntitiesVmRegistry.Settings[(int)Settings.EndYear]} УЧЕБНЫЙ ГОД";
                    var row = StartRow;
                    var counter = 1;
                    foreach (var subject in subjects)
                    {
                        if (subject.Entries.Count == 0)
                            continue;
                        worksheet.Cells[row, 1].Value = counter++;
                        worksheet.Cells[row, 2].Value = subject.Название;
                        worksheet.Cells[row, 3].Value = subject.Курс + ", ИТ";
                        worksheet.Cells[row, 4].Value = subject.Семестр / 2 == 0 ? "в" : "о";
                        worksheet.Cells[row, 5].Value = subject.Поток;
                        worksheet.Cells[row, 6].Value = subject.ModelObject.Lectures;
                        worksheet.Cells[row, 7].Value = subject.ModelObject.Practical;
                        worksheet.Cells[row, 8].Value = subject.ModelObject.Laboratory;
                        worksheet.Cells[row, 9].Value = subject.ModelObject.Численность;
                        worksheet.Cells[row, 10].Value = subject.ActualLoad.Lectures;
                        worksheet.Cells[row, 11].Value = subject.ActualLoad.Practical;
                        worksheet.Cells[row, 12].Value = subject.ActualLoad.Laboratory;
                        worksheet.Cells[row, 13].Value = subject.ActualLoad.Test;
                        worksheet.Cells[row, 14].Value = subject.ActualLoad.Consultations;
                        worksheet.Cells[row, 15].Value = subject.ActualLoad.Exams;
                        worksheet.Cells[row, 16].Value = subject.ActualLoad.Nir;
                        worksheet.Cells[row, 17].Value = subject.ActualLoad.CourseDesigning;
                        worksheet.Cells[row, 18].Value = subject.ActualLoad.Vkr;
                        worksheet.Cells[row, 19].Value = subject.ActualLoad.Gek;
                        worksheet.Cells[row, 20].Value = subject.ActualLoad.Rma;
                        worksheet.Cells[row, 21].Value = subject.ActualLoad.Rmp;
                        worksheet.Cells[row, 22].Value = subject.ActualLoadSum;
                        worksheet.Cells[row, 23].Value = subject.Entries.Select(x => x.Teacher.Surname_N_P)
                            .Aggregate((s, i) => s + "\n" + i);
                        row++;
                        worksheet.InsertRow(row, 1, row - 1);
                    }
                    worksheet.DeleteRow(row);
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