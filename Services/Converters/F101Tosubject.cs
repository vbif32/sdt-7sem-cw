using System;
using Entities;

namespace Services.Converters
{
    internal static class F101ToSubject
    {
        private const string bac = "Б";
        private const string mag = "М";
        private const string spec = "С";

        public static Load CalcLoad(F101Entry entry)
        {
            return new Load
            (
                CalcLec(entry),
                CalcLab(entry),
                CalcPr(entry),
                CalcZach(entry),
                CalcCons(entry),
                CalcExam(entry),
                CalcNir(entry),
                CalcCw(entry),
                CalcVkr(entry),
                CalGek(entry),
                CalcGak(entry),
                CalcRma(entry),
                CalcRmp(entry)
            );
        }

        private static float CalcLec(F101Entry entry)
        {
            return Calc(entry.НедельВСем, entry.ЛекцииВНеделю, 1, entry.ФормаОбучения, entry.Кафедра);
        }

        private static float CalcPr(F101Entry entry)
        {
            return Calc(entry.НедельВСем, entry.ПрактическиеВНеделю, entry.ЧислоГрупп, entry.ФормаОбучения,
                entry.Кафедра);
        }

        private static float CalcLab(F101Entry form)
        {
            return Calc(form.НедельВСем, form.ЛабораторныеВНеделю, form.ЧислоПодгрупп, form.ФормаОбучения,
                form.Кафедра);
        }

        private static float Calc(int недельВСем, float занятийВНеделю, int множительГрупп, ФормаОбучения формаОбучения,
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
            return (float) Math.Round(
                form.Зачет ? form.ПолнаяЧисленность * ContextSingleton.Instance.ZachMultiplayer : 0, 2);
        }

        private static float CalcCons(F101Entry form)
        {
            if (!form.Экзамен || form.ФормаОбучения == ФормаОбучения.Ошибка)
                return 0;
            if (form.ФормаОбучения == ФормаОбучения.Очная)
                return 2;
            return form.НедельВСем * form.ЛекцииВНеделю * form.ЧислоГрупп * ContextSingleton.Instance.ConsMultiplayer /
                   form.НедельВСем + 2;
        }

        private static float CalcExam(F101Entry form)
        {
            return (float) Math.Round(
                form.Экзамен ? form.ПолнаяЧисленность * ContextSingleton.Instance.ExamMultiplayer : 0, 2);
        }

        private static float CalcCw(F101Entry form)
        {
            if (form.Кр)
                return ContextSingleton.Instance.CourseWorkMultiplayer * form.ПолнаяЧисленность;
            if (form.Кп)
                return ContextSingleton.Instance.CourseProjectMultiplayer * form.ПолнаяЧисленность;
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
                    case bac:
                        multiplayer = 19;
                        break;
                    case spec:
                        multiplayer = 27;
                        break;
                    case mag:
                        multiplayer = 37;
                        break;
                }
            else if (form.Дисциплина.Contains("ВКР: Экон"))
                switch (lvl)
                {
                    case bac:
                        multiplayer = 2;
                        break;
                    case spec:
                        multiplayer = 3.5f;
                        break;
                }
            else if (form.Дисциплина.Contains("ВКР: Экол"))
                switch (lvl)
                {
                    case bac:
                        multiplayer = 1;
                        break;
                    case spec:
                        multiplayer = 1.75f;
                        break;
                }
            else if (form.Дисциплина.Contains("ГИА"))
                switch (lvl)
                {
                    case bac:
                        multiplayer = 19;
                        break;
                    case spec:
                        multiplayer = 27;
                        break;
                    case mag:
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
                    case bac:
                        multiplayer = 4;
                        break;
                    case spec:
                        multiplayer = 27;
                        break;
                    case mag:
                        multiplayer = 8;
                        break;
                }
            else if (form.Дисциплина.Contains("ВКР: Экон"))
                switch (lvl)
                {
                    case bac:
                        multiplayer = 2;
                        break;
                    case spec:
                        multiplayer = 3.5f;
                        break;
                }
            else if (form.Дисциплина.Contains("ВКР: Экол"))
                switch (lvl)
                {
                    case bac:
                        multiplayer = 1;
                        break;
                    case spec:
                        multiplayer = 1.75f;
                        break;
                }
            else if (form.Дисциплина.Contains("ГИА"))
                switch (lvl)
                {
                    case bac:
                        multiplayer = 19;
                        break;
                    case spec:
                        multiplayer = 27;
                        break;
                    case mag:
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