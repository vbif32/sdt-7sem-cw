using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Services.EntitiesViewModels;

namespace Services.Converters.Export
{
    class IPConverter
    {
        private static readonly string TemplatePath = @"Templates\F13Template.xlsx";
        private static readonly EntitiesVMRegistry EntitiesVmRegistry = ContextSingleton.Instance.EntitiesVmRegistry;
        private static readonly TeacherVM Teacher;


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
                    FillTitle(package.Workbook.Worksheets[1]);
                    package.Save();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void FillTitle(ExcelWorksheet worksheet)
        {
            worksheet.Cells["AT18"].Value = EntitiesVmRegistry.Settings[(int)Settings.StartYear].Value.Substring(2);
            worksheet.Cells["EE18"].Value = EntitiesVmRegistry.Settings[(int)Settings.StartYear].Value.Substring(2);
            worksheet.Cells["AZ18"].Value = EntitiesVmRegistry.Settings[(int)Settings.EndYear].Value.Substring(2);
            worksheet.Cells["EK18"].Value = EntitiesVmRegistry.Settings[(int)Settings.EndYear].Value.Substring(2);
            worksheet.Cells["CO27"].Value = EntitiesVmRegistry.Settings[(int)Settings.FullDepartmentName].Value;

            worksheet.Cells["BM28"].Value = Teacher.Post.LongName;
            worksheet.Cells["DC28"].Value = Teacher.Rate;
            worksheet.Cells["DY28"].Value = Teacher.WorkPlace;
            worksheet.Cells["BM29"].Value = Teacher.AcademicDegreeFull;
            worksheet.Cells["DY29"].Value = Teacher.AcademicDegreeFull.Length > 0 ? Teacher.Post.LongName : null;
            worksheet.Cells["A30"].Value = Teacher.Surname_Name_Patronymic;
        }

        private static void FillSem(ExcelWorksheet worksheet, int sem)
        {
            var row = 8;
            var counter = 1;
            foreach (var entry in Teacher.Entries.Where(e => e.Subject.Семестр == sem))
            {
                worksheet.Cells[row, 1].Value = entry.Subject.Название;
                worksheet.Cells[row, 2].Value = entry.Subject.Курс + ", ИТ";
                worksheet.Cells[row, 3].Value = entry.Subject.Поток;
                worksheet.Cells[row, 4].Value = entry.Subject.Численность;
                worksheet.Cells[row, 10].Value = entry.Lectures;
                worksheet.Cells[row, 11].Value = entry.Practical;
                worksheet.Cells[row, 12].Value = entry.Laboratory;
                worksheet.Cells[row, 13].Value = entry.Test;
                worksheet.Cells[row, 14].Value = entry.Consultations;
                worksheet.Cells[row, 15].Value = entry.Exams;
                worksheet.Cells[row, 16].Value = entry.Nir;
                worksheet.Cells[row, 17].Value = entry.CourseDesigning;
                worksheet.Cells[row, 18].Value = entry.Vkr;
                worksheet.Cells[row, 19].Value = entry.Gek;
                worksheet.Cells[row, 20].Value = entry.Rma;
                worksheet.Cells[row, 21].Value = entry.Rmp;
                worksheet.Cells[row, 22].Value = Math.Round(entry.Amount, 2);
                row+=2;
                worksheet.InsertRow(row, 1, row - 1);
            }
        }
    }
}
