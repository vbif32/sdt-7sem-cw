using System;
using System.Runtime.Remoting.Messaging;
using Entities;

namespace Converting
{
    // TODO: расчеты
    public class Ф101ToПредмет
    {
        public Предмет Convert(Ф101 form)
        {
            return new Предмет
            {
                Название = form.Дисциплина,
                Кафедра = form.Кафедра,
                Специальность = form.GetSpecialty(),
                ФормаОбучения = form.GetLearningForm(),
                Курс = form.Курс,
                Семестр = form.Семестр,
                НедельВСем = form.НедельТо,
                Поток = form.НомерПотока,
                ЧислоГрупп = form.ЧислоГрупп,
                ЧислоПодгрупп = form.GetSubgroupCount(),
                ГруппВПотоке = form.ЧислоГрупп, 
                Численность = form.Численность, 
                Трудоемкость = form.Трудоемкость,
                ТрудоемкостьГода = form.ТрудоемкостьГода,
                ЛекцииВНеделю = form.ЛекцииВНеделю,
                ЛабораторныеВНеделю = form.ЛабораторныеВНеделю,
                ПрактическиеВНеделю = form.ПрактическиеВНеделю,
                Экзамен = form.Экзамен,
                Зачет = form.Зачет,
                КурсовоеПроектирование = form.GetCourseDesign(),
                ПлановаяНагрузка = CalcLoad(form)
            };
        }

        public static Нагрузка CalcLoad(Ф101 form)
        {
            return new Нагрузка
            {
                Лекции = CalcLec(form),
                Лабораторные = CalcPr(form),
                Практические = CalcLab(form),
                Зачеты = CalcZach(form),
                Консультации = CalcCons(form),
                Экзамены = CalcExam(form),
                КурсовоеПроектирование = CalcCw(form),
                ПрактикиНир = CalcNir(form),
                Вкр = CalcVkr(form),
                ГэкГак = CalGekGak(form),
                ДопЗащ = CalcDopZash(form),
                Рма = CalcRma(form),
                Рмп = CalcRmp(form),
            };
        }

        private static float CalcLec(Ф101 form)
        {
            if (form.НедельТо is string)
                return 0;

            var res = form.НедельТо * form.ЛекцииВНеделю;

            if (form.GetLearningForm() == ФормаОбучения.Ошибка)
                res /= form.НедельТо;
            if (form.Кафедра < 0)
                res /= form.НедельТо;
            return res;
        }

        private static float CalcPr(Ф101 form)
        {
            if (form.НедельТо is string)
                return 0;

            var res = form.НедельТо * form.ПрактическиеВНеделю * form.ЧислоГрупп;

            if (form.GetLearningForm() == ФормаОбучения.Ошибка)
                res /= form.НедельТо;
            if (form.Кафедра < 0)
                res /= form.НедельТо;
            return res;
        }

        private static float CalcLab(Ф101 form)
        {
            if (form.НедельТо is string)
                return 0;

            var res = form.НедельТо * form.ПрактическиеВНеделю * form.GetSubgroupCount();

            if (form.GetLearningForm() == ФормаОбучения.Ошибка)
                res /= form.НедельТо;
            if (form.Кафедра < 0)
                res /= form.НедельТо;
            return res;
        }

        private static float CalcZach(Ф101 form) => (float)Math.Round(form.Зачет ? int.Parse(form.Численность.Split()[0]) * Ф101.ZachMultiplayer : 0,2);

        private static float CalcCons(Ф101 form)
        {
            if (!form.Экзамен || form.GetLearningForm() == ФормаОбучения.Ошибка) return 0;
            if (form.GetLearningForm() == ФормаОбучения.Очная) return 2;
            return form.НедельТо * form.ЛекцииВНеделю * form.ЧислоГрупп * 0.07 / form.НедельТо + 2;
        }

        private static float CalcExam(Ф101 form) => (float) Math.Round(form.Экзамен ? int.Parse(form.Численность.Split()[0]) * Ф101.ExamMultiplayer : 0,2);

        private static float CalcCw(Ф101 form)
        {
            var a = int.Parse(form.Численность);
            if (form.Кп)
                return 3 * a;
            if (form.Кр)
                return 2 * a;
            return 0;
        }

        private static float CalcNir(Ф101 form)
        {
            if (!(form.НедельТо is string) || form.НедельТо != "П1" && form.НедельТо != "П2") return 0;
            string nirCount = form.ПрактическиеВНеделю;
            nirCount = nirCount.Replace("н", "");
            nirCount = nirCount.Replace(",", ".");
            return 3 * form.ЧислоГрупп * 6 * float.Parse(nirCount);
        }

        private static float CalcVkr(Ф101 form)
        {
            var strength = form.GetStrength();
            if (form.Дисциплина.Contains("Государственный экзамен"))
                return (float) (2.1 * strength);
            if (form.Дисциплина.Contains("Итоговый междисциплинарный экзамен"))
                return (float)(2.5 * strength);
            if (form.Дисциплина.Contains("ВКР"))
                return 3 * strength;
            if (form.Дисциплина.Contains("ГИА"))
                return (float) (5.5 * strength);
            return 0;
        }

        private static float CalGekGak(Ф101 form)
        {
            throw new NotImplementedException();
        }

        private static float CalcDopZash(Ф101 form)
        {
            throw new NotImplementedException();
        }

        private static float CalcRma(Ф101 form)
        {
            throw new NotImplementedException();
        }

        private static float CalcRmp(Ф101 form)
        {
            throw new NotImplementedException();
        }
    }
}
