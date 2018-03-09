using System;
using System.IO;
using System.Linq;
using Entities;
using OfficeOpenXml;
using Services.EntitiesViewModels;

namespace Services.Export
{
    internal class F106Converter
    {
        private const int StartRow = 14;
        private static readonly string TemplatePath = @"Templates\F106Template.xlsx";

        public static void Convert(EntitiesVMRegistry registry, string targetPath)
        {
            //TemplatePath = Path.GetFullPath(TemplatePath);
            try
            {
                File.Copy(TemplatePath, targetPath, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            var newFile = new FileInfo(targetPath);
            try
            {
                using (var package = new ExcelPackage(newFile))
                {
                    var teachers = registry.Teachers;
                    var worksheet = package.Workbook.Worksheets[1];
                    worksheet.Cells[8, 1].RichText[1].Text = ContextSingleton.Instance.EntitiesVmRegistry.Settings[(int)Settings.FullDepartmentName].Value;
                    worksheet.Cells[8, 1].RichText[3].Text = ContextSingleton.Instance.EntitiesVmRegistry.Settings[(int)Settings.StartYear].Value;
                    worksheet.Cells[8, 1].RichText[5].Text = ContextSingleton.Instance.EntitiesVmRegistry.Settings[(int)Settings.EndYear].Value;

                    var row = StartRow;
                    var counter = 1;
                    foreach (var teacher in teachers)
                    {
                        worksheet.Cells[row, 1].Value = counter++;
                        worksheet.Cells[row, 2].Value = teacher.Surname_N_P;
                        worksheet.Cells[row, 3].Value = teacher.Rate;
                        worksheet.Cells[row, 4].Value = teacher.Post.LongName;
                        worksheet.Cells[row, 5].Value = teacher.AcademicDegree;
                        worksheet.Cells[row, 6].Value = teacher.ActualLoad.Lectures;
                        worksheet.Cells[row, 7].Value = teacher.ActualLoad.Practical;
                        worksheet.Cells[row, 8].Value = teacher.ActualLoad.Laboratory;
                        worksheet.Cells[row, 9].Value = teacher.ActualLoad.Test;
                        worksheet.Cells[row, 10].Value = teacher.ActualLoad.Consultations;
                        worksheet.Cells[row, 11].Value = teacher.ActualLoad.Exams;
                        worksheet.Cells[row, 12].Value = teacher.ActualLoad.Nir;
                        worksheet.Cells[row, 13].Value = teacher.ActualLoad.CourseDesigning;
                        worksheet.Cells[row, 14].Value = teacher.ActualLoad.Vkr;
                        worksheet.Cells[row, 15].Value = teacher.ActualLoad.Gek + teacher.ActualLoad.Gak;
                        worksheet.Cells[row, 16].Value = teacher.ActualLoad.Rma;
                        worksheet.Cells[row, 17].Value = teacher.ActualLoad.Rmp;
                        worksheet.Cells[row, 20].Value = Math.Round(teacher.ActualLoadSum, 2);
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