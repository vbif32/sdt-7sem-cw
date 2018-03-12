using System.Linq;
using Services.Converters.Export;
using Services.Converters.Import;
using Services.EntitiesViewModels;
using Services.Export;

namespace Services
{
    public static class ControllerService
    {
        private static readonly ContextSingleton Context = ContextSingleton.Instance;

        public static string ImportF101(string path)
        {
            var f101Entries = F101Converter.Convert(path);
            if (!f101Entries.Any())
                return "Во время импорта произошла ошибка";
            var newSubjects = F101EntryToSubject.Convert(f101Entries);
            if (!newSubjects.Any())
                return "Во время конвертации произошла ошибка";

            Context.EntitiesVmRegistry.Subjects.Select(s =>
            {
                s.IsActive = false;
                return s;
            });

            if (Context.EntitiesVmRegistry.Subjects.Count == 0)
                foreach (var newSubject in newSubjects)
                    Context.EntitiesVmRegistry.Subjects.Add(new SubjectVM(newSubject));
            else
            {
                foreach (var newSubject in newSubjects)
                {
                    var subjectVms = Context.EntitiesVmRegistry.Subjects.Where(s => s.IsSame(newSubject));
                    if (subjectVms.Count() == 1)
                        subjectVms.First().Update(newSubject);
                    else
                        Context.EntitiesVmRegistry.Subjects.Add(new SubjectVM(newSubject));
                }
            }

            Context.EntitiesVmRegistry.SaveChanges();
            var mes = $"Импортировано {newSubjects.Count()} предметов. Из них {Context.EntitiesVmRegistry.NewSubjects.Count} новые.";
            var inactive = Context.EntitiesVmRegistry.Subjects.Count(s => s.IsActive == false);
            if (inactive > 0)
                mes += $"\nВнимание! {inactive} предметов осталось неативными";
            return mes;
        }

        public static void ResetSubjects()
        {
            ResetEntries();
            Context.EntitiesVmRegistry.Subjects.Clear();
            Context.EntitiesVmRegistry.SaveSubjects();
        }

        public static void ResetEntries()
        {
            Context.EntitiesVmRegistry.Entries.Clear();
            Context.EntitiesVmRegistry.SaveEntries();
        }

        public static void ExportToF106(string path)
        {
            F106Converter.Convert(Context.EntitiesVmRegistry,path);
        }

        public static bool ExportToF115(string path)
        {
            return F115Converter.Convert(path);
        }

        public static void ExportToF13(string path)
        {
            F16Converter.Convert(Context.EntitiesVmRegistry, path);
        }

        public static void ExportToIP(string path, TeacherVM teacher)
        {
            IPConverter.Convert(Context.EntitiesVmRegistry, path, teacher);
        }



        //public static void ExportEnties(TeacherVM teacher) => F16Converter.Convert(Context.EntitiesVmRegistry, path);
        //public static void ExportEnties(SubjectVM subject) => F16Converter.Convert(Context.EntitiesVmRegistry, path);
    }
}