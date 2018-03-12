using System;
using System.IO;
using System.Linq;
using Entities;
using OfficeOpenXml;
using Services.EntitiesViewModels;

namespace Services.Converters.Export
{
    class IPConverter
    {
        private static readonly string TemplatePath = @"Templates\IPTemplate.xlsx";
        private static readonly EntitiesVMRegistry EntitiesVmRegistry = ContextSingleton.Instance.EntitiesVmRegistry;

        public static void Convert(EntitiesVMRegistry registry, string targetPath, TeacherVM teacher)
        {
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
                    FillTitle(package.Workbook.Worksheets[1], teacher);
                    FillSem(package.Workbook.Worksheets[2],1, teacher);
                    FillSem(package.Workbook.Worksheets[3],2, teacher);
                    FillTotal(package.Workbook.Worksheets[5]);
                    package.Save();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void FillTitle(ExcelWorksheet worksheet, TeacherVM teacher)
        {
            worksheet.Cells["AT18"].Value = EntitiesVmRegistry.Settings[(int)Settings.StartYear].Value.Substring(2);
            worksheet.Cells["EE18"].Value = EntitiesVmRegistry.Settings[(int)Settings.StartYear].Value.Substring(2);
            worksheet.Cells["AZ18"].Value = EntitiesVmRegistry.Settings[(int)Settings.EndYear].Value.Substring(2);
            worksheet.Cells["EK18"].Value = EntitiesVmRegistry.Settings[(int)Settings.EndYear].Value.Substring(2);
            worksheet.Cells["CO27"].Value = EntitiesVmRegistry.Settings[(int)Settings.FullDepartmentName].Value;

            worksheet.Cells["BM28"].Value = teacher.Post.LongName;
            worksheet.Cells["DC28"].Value = teacher.Rate;
            worksheet.Cells["DY28"].Value = teacher.WorkPlace;
            worksheet.Cells["BM29"].Value = teacher.AcademicDegreeFull;
            worksheet.Cells["DY29"].Value = teacher.AcademicDegreeFull.Length > 0 ? teacher.Post.LongName : null;
            worksheet.Cells["A30"].Value = teacher.Surname_Name_Patronymic;
        }

        private static void FillSem(ExcelWorksheet worksheet, int sem, TeacherVM teacher)
        {
            var row = 8;
            foreach (var entry in teacher.Entries.Where(e => e.Subject.Семестр == sem))
            {
                worksheet.Cells[row, 1].Value = entry.Subject.Название;
                worksheet.Cells[row, 2].Value = entry.Subject.Курс + ", ИТ";
                worksheet.Cells[row, 3].Value = entry.Subject.Поток;
                worksheet.Cells[row, 4].Value = entry.Subject.Численность;
                worksheet.Cells[row, 5].Value = "ПЛ";
                worksheet.Cells[row+1, 5].Value = "ВЫП";
                worksheet.Cells[row, 6].Value = entry.Lectures;
                worksheet.Cells[row, 7].Value = entry.Laboratory;
                worksheet.Cells[row, 8].Value = entry.Practical;
                worksheet.Cells[row, 9].Value = entry.Consultations;
                worksheet.Cells[row, 10].Value = entry.Test;
                worksheet.Cells[row, 11].Value = entry.Exams;
                worksheet.Cells[row, 12].Value = entry.Nir;
                worksheet.Cells[row, 14].Value = entry.CourseDesigning;
                worksheet.Cells[row, 15].Value = entry.Vkr;
                worksheet.Cells[row, 16].Value = entry.Gek + entry.Gak;
                worksheet.Cells[row, 17].Value = entry.Rma;
                worksheet.Cells[row, 20].Value = Math.Round(entry.Amount, 2);
                row+=2;
                worksheet.InsertRow(row, 2, row - 1);
            }
        }

        private static void FillTotal(ExcelWorksheet worksheet)
        {
            worksheet.Cells["AQ25"].Value = EntitiesVmRegistry.Settings[(int)Settings.StartYear].Value.Substring(2);
            worksheet.Cells["FD25"].Value = EntitiesVmRegistry.Settings[(int)Settings.EndYear].Value.Substring(2);
        }
    }
}
