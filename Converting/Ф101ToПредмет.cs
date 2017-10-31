using System;
using System.Runtime.Remoting.Messaging;
using Entities;

namespace Converting
{
    // TODO: расчеты
    public class Ф101ToПредмет
    {
        private const int SubgroupStrigth = 15;
        private const float ZachMultiplayer = 0.25f;
        private const float ExamMultiplayer = 0.35f;


        private Предмет Convert(Ф101 form)
        {
            return new Предмет
            {
                Название = form.Дисциплина,
                Кафедра = form.Кафедра,
                Специальность = GetSpecialty(form.ИмяПотока),
                ФормаОбучения = GetLearningForm(form.ИмяПотока),
                Курс = form.Курс,
                Семестр = form.Семестр,
                НедельВСем = form.НедельТо,
                Поток = form.НомерПотока,
                ЧислоГрупп = form.ЧислоГрупп,
                ЧислоПодгрупп = GetSubgroupCount(form.Численность, form.ЧислоГрупп),
                ГруппВПотоке = form.ЧислоГрупп, 
                Численность = form.Численность, 
                Трудоемкость = form.Трудоемкость,
                ТрудоемкостьГода = form.ТрудоемкостьГода,
                ЛекцииВНеделю = form.ЛекцииВНеделю,
                ЛабораторныеВНеделю = form.ЛабораторныеВНеделю,
                ПрактическиеВНеделю = form.ПрактическиеВНеделю,
                Экзамен = form.Экзамен,
                Зачет = form.Зачет,
                КурсовоеПроектирование = GetCourseDesign(form.Кр, form.Кп),
                ПлановаяНагрузка = CalcLoad(form)
            };
        }

        private static string GetSpecialty(string streamName)
        {
            switch (streamName.Substring(0, 3))
            {
                case "ИКБ":
                    return "09.03.04";
                case "ИВБ":
                    return "09.03.01";
                case "ИСБ":
                    return "09.03.02";
                case "ИНБ":
                    return "09.03.03";
                case "ИАБ":
                    return "15.03.04";
                case "ИКМ":
                    return "09.04.04";
                case "ИВМ":
                    return "09.04.01";
                case "ИСМ":
                    return "09.04.02";
                case "ИАМ":
                    return "15.04.04";
                case "ИРБ":
                    return "15.03.06";
                case "ИУБ":
                    return "27.03.04";
                case "ИНМ":
                    return "09.04.03";
                case "ВТБ":
                    return "0"; // TODO: узнать направление 
                case "ИхА":
                    return "0"; // TODO: узнать направление 
                default:
                    return "0";
            }
        }

        private static ФормаОбучения GetLearningForm(string streamName)
        {
            switch (streamName.Substring(3,1))
            {
                case "О":
                    return ФормаОбучения.Очная;
                case "В":
                    return ФормаОбучения.Вечерняя;
                case "З":
                    return ФормаОбучения.Заочная;
                default:
                    return ФормаОбучения.Ошибка; // TODO: узнать какие еще варианты
            }
        }

        private static int GetSubgroupCount(string strength, int groupCount)
        {
            if (int.Parse(strength.Split()[0]) / groupCount <= SubgroupStrigth)
                return groupCount;
            return groupCount*2;
        }

        private static КурсовоеПроектирование GetCourseDesign(bool cw, bool cp)
        {
            if (cw) return КурсовоеПроектирование.КурсоваяРабота;
            if (cp) return КурсовоеПроектирование.КурсовойПроект;
            return КурсовоеПроектирование.Нет;
        }

        private static Нагрузка CalcLoad(Ф101 form)
        {
            return new Нагрузка
            {
                Id = 0, // TODO: придумать как генерить idшник
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
            //G14 - количество недель в семестре
            //O14 - количество занятий в неделю
            //I14 - число групп
            //J14 - число подргупп
            //B14 - кафедра
            if (form.ЛабораторныеВНеделю == 0)
                return 0;

            var result = form.ЛекцииВНеделю * form.ЧислоГрупп * form.НедельТо;
            if (form.НедельТо == "ФВ")  
                result /= form.НедельТо;
            return result;
        }

        private static float CalcPr(Ф101 form)
        {
            throw new NotImplementedException();
        }

        private static float CalcLab(Ф101 form)
        {
            throw new NotImplementedException();
        }

        private static float CalcZach(Ф101 form)
        {
            return form.Зачет ? int.Parse(form.Численность.Split()[0]) * ZachMultiplayer : 0;
        }

        private static float CalcCons(Ф101 form)
        {
            if (!form.Экзамен) return 0;
            throw new NotImplementedException();
        }

        private static float CalcExam(Ф101 form)
        {
            return form.Зачет ? int.Parse(form.Численность.Split()[0]) * ExamMultiplayer : 0;
        }

        private static float CalcCw(Ф101 form)
        {
            throw new NotImplementedException();
        }

        private static float CalcNir(Ф101 form)
        {
            throw new NotImplementedException();
        }

        private static float CalcVkr(Ф101 form)
        {
            throw new NotImplementedException();
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
