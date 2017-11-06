﻿using System;
using Entities;

namespace Converting
{
    // TODO: расчеты
    public class Ф101ToПредмет
    {
        public Предмет Convert(F101Entry form)
        {
            return new Предмет
            {
                Название = form.Дисциплина,
                Кафедра = form.Кафедра,
                Специальность = form.Специальность,
                ФормаОбучения = form.ФормаОбучения,
                Курс = form.Курс,
                Семестр = form.Семестр,
                НедельВСем = form.НедельВСем,
                Поток = form.НомерПотока,
                ЧислоГрупп = form.ЧислоГрупп,
                ЧислоПодгрупп = form.ЧислоПодгрупп,
                ГруппВПотоке = form.ЧислоГрупп,
                Численность = form.Численность,
                Трудоемкость = form.Трудоемкость,
                ТрудоемкостьГода = form.ТрудоемкостьГода,
                Лк = form.Лк,
                Лаб = form.Лаб,
                Пр = form.Пр,
                Экзамен = form.Экзамен,
                Зачет = form.Зачет,
                КурсовоеПроектирование = form.КурсовоеПроектирование,
                ПлановаяНагрузка = CalcLoad(form)
            };
        }

        public static Нагрузка CalcLoad(F101Entry form)
        {
            return new Нагрузка
            (
                CalcLec(form),
                CalcLab(form),
                CalcPr(form),
                CalcZach(form),
                CalcCons(form),
                CalcExam(form),
                CalcNir(form),
                CalcCw(form),
                CalcVkr(form),
                CalGek(form),
                CalcGakDopZash(form),
                CalcRuk(form)
            );
        }

        private static float CalcLec(F101Entry form)
        {
            return Calc(form.НедельВСем, form.ЛекцииВНеделю, 1, form.ФормаОбучения, form.Кафедра);
        }

        private static float CalcPr(F101Entry form)
        {
            return Calc(form.НедельВСем, form.ПрактическиеВНеделю, form.ЧислоГрупп, form.ФормаОбучения, form.Кафедра);
        }

        private static float CalcLab(F101Entry form)
        {
            return Calc(form.НедельВСем, form.ЛабораторныеВНеделю, form.ЧислоПодгрупп, form.ФормаОбучения,
                form.Кафедра);
        }

        private static float Calc(int недельВСем, int занятийВНеделю, int множительГрупп, ФормаОбучения формаОбучения,
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
            return (float) Math.Round(form.Зачет ? form.ПолнаяЧисленность * F101Entry.ZachMultiplayer : 0,
                2);
        }

        private static float CalcCons(F101Entry form)
        {
            if (!form.Экзамен || form.ФормаОбучения == ФормаОбучения.Ошибка) return 0;
            if (form.ФормаОбучения == ФормаОбучения.Очная) return 2;
            return form.НедельВСем * form.ЛекцииВНеделю * form.ЧислоГрупп * 0.07f / form.НедельВСем + 2;
        }

        private static float CalcExam(F101Entry form)
        {
            return (float) Math.Round(form.Экзамен ? form.ПолнаяЧисленность * F101Entry.ExamMultiplayer : 0,
                2);
        }

        private static float CalcCw(F101Entry form)
        {
            var multiplayer = 0;
            if (form.Кп)
                multiplayer = 3;
            if (form.Кр)
                multiplayer = 2;
            return multiplayer * form.ПолнаяЧисленность;
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
            var lvl = form.ИмяПотока.Substring(3, 1);
            float multiplayer = 0;
            if (form.Дисциплина.Contains("ВКР: Спец") || form.Дисциплина.Equals("ВКР"))
            {
                if (lvl == "Б")
                    multiplayer = 19;
                else if (lvl == "C")
                    multiplayer = 27;
                else if (lvl == "М")
                    multiplayer = 37;
            }
            else if (form.Дисциплина.Contains("ВКР: Экон"))
            {
                if (lvl == "Б")
                    multiplayer = 2;
                else if (lvl == "C")
                    multiplayer = 3.5f;
            }
            else if (form.Дисциплина.Contains("ВКР: Экол"))
            {
                if (lvl == "Б")
                    multiplayer = 1;
                else if (lvl == "C")
                    multiplayer = 1.75f;
            }
            else if (form.Дисциплина.Contains("ГИА"))
            {
                if (lvl == "Б")
                    multiplayer = 19;
                else if (lvl == "C")
                    multiplayer = 27;
                else if (lvl == "М")
                    multiplayer = 37;
            }
            return multiplayer * form.ПолнаяЧисленность;
        }

        private static float CalGek(F101Entry form)
        {
            float multiplayer = 0;
            if (form.Дисциплина.Contains("Государственный экзамен"))
                multiplayer = 2.1f;
            if (form.Дисциплина.Contains("Итоговый междисциплинарный экзамен"))
                multiplayer = 2.5f;
            if (form.Дисциплина.Contains("ВКР: С"))
                multiplayer = 3;
            if (form.Дисциплина.Contains("ГИА"))
                multiplayer = 5.5f;
            return multiplayer * form.ПолнаяЧисленность;
        }

        private static float CalcGakDopZash(F101Entry form)
        {
            var lvl = form.ИмяПотока.Substring(3, 1);
            float multiplayer = 0;
            if (form.Дисциплина.Contains("ВКР: Спец") || form.Дисциплина.Equals("ВКР"))
            {
                if (lvl == "Б")
                    multiplayer = 4;
                else if (lvl == "C")
                    multiplayer = 27;
                else if (lvl == "М")
                    multiplayer = 8;
            }
            else if (form.Дисциплина.Contains("ВКР: Экон"))
            {
                if (lvl == "Б")
                    multiplayer = 2;
                else if (lvl == "C")
                    multiplayer = 3.5f;
            }
            else if (form.Дисциплина.Contains("ВКР: Экол"))
            {
                if (lvl == "Б")
                    multiplayer = 1;
                else if (lvl == "C")
                    multiplayer = 1.75f;
            }
            else if (form.Дисциплина.Contains("ГИА"))
            {
                if (lvl == "Б")
                    multiplayer = 19;
                else if (lvl == "C")
                    multiplayer = 27;
                else if (lvl == "М")
                    multiplayer = 37;
            }
            return multiplayer * form.ПолнаяЧисленность;
        }

        private static float CalcRuk(F101Entry form)
        {
            var multiplayer = 0;
            if (form.Дисциплина.Contains("Руководство магистрами"))
            {
                multiplayer = 6;
            }
            else if (form.Дисциплина.Contains("Руководство аспирантами"))
            {
                if (form.ФормаОбучения == ФормаОбучения.Очная)
                    multiplayer = 50;
                if (form.ФормаОбучения == ФормаОбучения.Заочная)
                    multiplayer = 25;
            }
            else if (form.Дисциплина.Contains("Руководство программой"))
            {
                return 10;
            }
            return multiplayer * form.ПолнаяЧисленность;
        }
    }
}