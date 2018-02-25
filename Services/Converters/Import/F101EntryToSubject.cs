using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Services.Converters.Import
{
    internal static class F101EntryToSubject
    {
        private const string Bac = "Б";
        private const string Mag = "М";
        private const string Spec = "С";

        private static readonly ContextSingleton Context = ContextSingleton.Instance;

        public static IEnumerable<Subject> Convert(IEnumerable<F101Entry> form)
        {
            return form.Select(Convert).ToList();
        }

        public static Subject Convert(F101Entry f101Entry)
        {
            return new Subject
            {
                IsActive = true,
                Name = f101Entry.Дисциплина,
                Department = f101Entry.Кафедра,
                Specialty = f101Entry.Специальность,
                EducationForm = f101Entry.ФормаОбучения,
                Course = f101Entry.Курс,
                Semester = f101Entry.Семестр,
                НедельВСем = f101Entry.НедельВСем,
                Flow = f101Entry.ИмяПотока,
                GroupsCount = f101Entry.ЧислоГрупп,
                SubgroupsCount = f101Entry.ЧислоПодгрупп,
                ГруппВПотоке = f101Entry.ЧислоГрупп,
                Численность = f101Entry.Численность,
                Трудоемкость = f101Entry.Трудоемкость,
                ТрудоемкостьГода = f101Entry.ТрудоемкостьГода,
                Lectures = f101Entry.Лк,
                Laboratory = f101Entry.Лаб,
                Practical = f101Entry.Пр,
                Exam = f101Entry.Экзамен,
                Test = f101Entry.Зачет,
                CourseDesigning = f101Entry.КурсовоеПроектирование,
                PlannedLoad = CalcLoad(f101Entry)
            };
        }

        public static Load CalcLoad(F101Entry entry)
        {
            return new Load
            {
                Lectures = CalcLec(entry),
                Laboratory = CalcLab(entry),
                Practical = CalcPr(entry),
                Test = CalcZach(entry),
                Consultations = CalcCons(entry),
                Exams = CalcExam(entry),
                Nir = CalcNir(entry),
                CourseDesigning = CalcCD(entry),
                Vkr = CalcVkr(entry),
                Gek = CalGek(entry),
                Gak = CalcGak(entry),
                Rma = CalcRma(entry),
                Rmp = CalcRmp(entry)
            };
        }

        private static float CalcLec(F101Entry entry)
        {
            return CalcAuditoryLessons(entry.НедельВСем, entry.ЛекцииВНеделю, 1, entry.ФормаОбучения, entry.Кафедра);
        }

        private static float CalcPr(F101Entry entry)
        {
            return CalcAuditoryLessons(entry.НедельВСем, entry.ПрактическиеВНеделю, entry.ЧислоГрупп,
                entry.ФормаОбучения, entry.Кафедра);
        }

        private static float CalcLab(F101Entry form)
        {
            return CalcAuditoryLessons(form.НедельВСем, form.ЛабораторныеВНеделю, form.ЧислоПодгрупп,
                form.ФормаОбучения, form.Кафедра);
        }

        private static float CalcAuditoryLessons(int недельВСем, float занятийВНеделю, int множительГрупп,
            ФормаОбучения формаОбучения,
            int кафедра)
        {
            if (недельВСем == 0)
                return 0;

            var res = недельВСем * занятийВНеделю * множительГрупп;

            if (формаОбучения == ФормаОбучения.Ошибка)
                res /= недельВСем;
            if (кафедра < 0)
                res /= недельВСем;
            return res;
        }

        private static float CalcZach(F101Entry form)
        {
            return (float) Math.Round(form.Зачет ? form.ПолнаяЧисленность * Context.ZachMultiplayer : 0, 2);
        }

        private static float CalcCons(F101Entry form)
        {
            if (!form.Экзамен || form.ФормаОбучения == ФормаОбучения.Ошибка)
                return 0;
            if (form.ФормаОбучения == ФормаОбучения.Очная)
                return 2;
            return form.НедельВСем * form.ЛекцииВНеделю * form.ЧислоГрупп * Context.ConsMultiplayer /
                   form.НедельВСем + 2;
        }

        private static float CalcExam(F101Entry form)
        {
            return (float) Math.Round(form.Экзамен ? form.ПолнаяЧисленность * Context.ExamMultiplayer : 0, 2);
        }

        private static float CalcCD(F101Entry form)
        {
            if (form.Кр)
                return Context.CourseWorkMultiplayer * form.ПолнаяЧисленность;
            if (form.Кп)
                return Context.CourseProjectMultiplayer * form.ПолнаяЧисленность;
            return 0;
        }

        private static float CalcNir(F101Entry form)
        {
            if (form.НедельВСем != 0 || form.НедельТо != "П1" && form.НедельТо != "П2") return 0;
            var nirCount = form.Пр;
            nirCount = nirCount.Replace("н", "");
            nirCount = nirCount.Replace(",", ".");
            return 3 * form.ЧислоГрупп * 6 * float.Parse(nirCount);
        }

        private static float CalcVkr(F101Entry form)
        {
            var lvl = form.ИмяПотока.Substring(2, 1);
            float multiplayer = 0;
            if (form.Дисциплина.Contains("ВКР: Спец") || form.Дисциплина.Equals("ВКР"))
                switch (lvl)
                {
                    case Bac:
                        multiplayer = 19;
                        break;
                    case Spec:
                        multiplayer = 27;
                        break;
                    case Mag:
                        multiplayer = 37;
                        break;
                }
            else if (form.Дисциплина.Contains("ВКР: Экон"))
                switch (lvl)
                {
                    case Bac:
                        multiplayer = 2;
                        break;
                    case Spec:
                        multiplayer = 3.5f;
                        break;
                }
            else if (form.Дисциплина.Contains("ВКР: Экол"))
                switch (lvl)
                {
                    case Bac:
                        multiplayer = 1;
                        break;
                    case Spec:
                        multiplayer = 1.75f;
                        break;
                }
            else if (form.Дисциплина.Contains("ГИА"))
                switch (lvl)
                {
                    case Bac:
                        multiplayer = 19;
                        break;
                    case Spec:
                        multiplayer = 27;
                        break;
                    case Mag:
                        multiplayer = 37;
                        break;
                }
            return multiplayer * form.ПолнаяЧисленность;
        }

        private static float CalGek(F101Entry form)
        {
            float multiplayer = 0;
            if (form.Дисциплина.Contains("Государственный экзамен"))
                multiplayer = 2.1f;
            else if (form.Дисциплина.Contains("Итоговый междисциплинарный экзамен"))
                multiplayer = 2.5f;
            else if (form.Дисциплина.Contains("ВКР: С"))
                multiplayer = 3;
            else if (form.Дисциплина.Contains("ГИА"))
                multiplayer = 5.5f;
            return multiplayer * form.ПолнаяЧисленность;
        }

        private static float CalcGak(F101Entry form)
        {
            var lvl = form.ИмяПотока.Substring(3, 1);
            float multiplayer = 0;
            if (form.Дисциплина.Contains("ВКР: Спец") || form.Дисциплина.Equals("ВКР"))
                switch (lvl)
                {
                    case Bac:
                        multiplayer = 4;
                        break;
                    case Spec:
                        multiplayer = 27;
                        break;
                    case Mag:
                        multiplayer = 8;
                        break;
                }
            else if (form.Дисциплина.Contains("ВКР: Экон"))
                switch (lvl)
                {
                    case Bac:
                        multiplayer = 2;
                        break;
                    case Spec:
                        multiplayer = 3.5f;
                        break;
                }
            else if (form.Дисциплина.Contains("ВКР: Экол"))
                switch (lvl)
                {
                    case Bac:
                        multiplayer = 1;
                        break;
                    case Spec:
                        multiplayer = 1.75f;
                        break;
                }
            else if (form.Дисциплина.Contains("ГИА"))
                switch (lvl)
                {
                    case Bac:
                        multiplayer = 19;
                        break;
                    case Spec:
                        multiplayer = 27;
                        break;
                    case Mag:
                        multiplayer = 37;
                        break;
                }
            return multiplayer * form.ПолнаяЧисленность;
        }

        private static float CalcRma(F101Entry form)
        {
            var multiplayer = 0;
            if (form.Дисциплина.Contains("Руководство магистрами"))
                multiplayer = 6;
            else if (form.Дисциплина.Contains("Руководство аспирантами"))
                switch (form.ФормаОбучения)
                {
                    case ФормаОбучения.Очная:
                        multiplayer = 50;
                        break;
                    case ФормаОбучения.Заочная:
                        multiplayer = 25;
                        break;
                }
            return multiplayer * form.ПолнаяЧисленность;
        }

        private static float CalcRmp(F101Entry form)
        {
            return form.Дисциплина.Contains("Руководство программой") ? 10 : 0;
        }
    }
}