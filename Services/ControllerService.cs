using System.Linq;
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
            var subjects = F101EntryToSubject.Convert(f101Entries);
            if (!subjects.Any())
                return "Во время конвертации произошла ошибка";

            Context.EntitiesVmRegistry.Subjects.Select(s => s.IsActive = false);
            foreach (var subject in subjects)
            {
                var subjectVms = Context.EntitiesVmRegistry.Subjects.Where(s => s.IsSame(subject));
                if (subjectVms.Count() == 1)
                    subjectVms.First().Update(subject);
                else
                    Context.EntitiesVmRegistry.Subjects.Add(new SubjectVM(subject));
            }
            Context.EntitiesVmRegistry.SaveChanges();
            var mes = $"Импортировано {subjects.Count()} предметов.";
            var inactive = Context.EntitiesVmRegistry.Subjects.Count(s => s.IsActive == false);
            if (inactive > 0)
                mes += $"\nВнимание! {inactive} предметов осталось неативными";
            return mes;
        }

        public static void ResetSubjects()
        {
            ResetEntries();
            Context.EntitiesVmRegistry.Subjects.Clear();
        }

        public static void ResetEntries()
        {
            ContextSingleton.Instance.EntitiesVmRegistry.Entries.Clear();
        }

        public static bool ExportToF106(string path)
        {
            return F106Converter.Convert(path);
        }

        public static bool ExportToF115(string path)
        {
            return F115Converter.Convert(path);
        }

        public static void ExportToF16(string path)
        {
            F16Converter.Convert(Context.EntitiesVmRegistry, path);
        }

        //public static void ExportEnties(TeacherVM teacher) => F16Converter.Convert(Context.EntitiesVmRegistry, path);
        //public static void ExportEnties(SubjectVM subject) => F16Converter.Convert(Context.EntitiesVmRegistry, path);
    }
}